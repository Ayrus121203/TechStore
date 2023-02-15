using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http.Controllers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace TechStore
{
    public partial class Login : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        string AccountSID = "ACd8092879c3f9fae29614d31c01da62ad";
        string Auth_Token = "4b0883e784eed280cc6fa455ce9f1e33";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Request.Cookies["UNAME"] != null && Request.Cookies["PWD"] != null) //Check for Remeber Me In btnLogin_Click()
                {
                    txtUsername.Text = Request.Cookies["UNAME"].Value; //Auto fills the Username txtbx when page is loaded
                    txtPassword.Attributes["value"] = Request.Cookies["PWD"].Value; //Auto fills the Password txtbx when page is loaded
                    chckbxRemMe.Checked = true; //Auto check chckbx when page is loaded
                }
                else
                {
                    chckbxRemMe.Checked = false;
                }
            }
        }
        private Int32 GetUserID()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM Users WHERE Username=@Username", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Int32 UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                return UserID;
            }
            return -1;

        }
        private bool CheckSecurityQuesEnabled()
        {
            if (GetUserID() != -1)
            {
                Int32 UserID = GetUserID();
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT UserID FROM UserSecurityQuestions WHERE UserID=@UserID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@UserID", UserID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        private void DisplaySecQuesModal()
        {
            if (CheckSecurityQuesEnabled())
            {
                displaymodal.Visible = true;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script language='javascript'>");
                sb.Append(@"$('#myModal').modal('show');");
                sb.Append(@"</script>");
                ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                
            }
            else
            {
                displaymodal.Visible = false;
            }
        }
        private void AccountCompromiseAlertUsersEmail_SMS()
        {
            //Get All Required Sending Parameters
            Guid linkuid = Guid.NewGuid();
            string Name = "";
            string Email = "";
            int UserID;
            string PhoneNumber = "";
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmdGetUserDetails = new SqlCommand("SELECT Name, Email, PhoneNumber, UserID FROM Users WHERE Username=@Username",con);
            cmdGetUserDetails.Parameters.AddWithValue("@Username",txtUsername.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetUserDetails);
            sda.Fill(dt);
            if (dt.Rows.Count != 0)
            {
                Name = dt.Rows[0][0].ToString();
                Email = dt.Rows[0][1].ToString();
                PhoneNumber = dt.Rows[0][2].ToString();
                PhoneNumber = "+65" + PhoneNumber;
                //Alert Users using Email for Account Recovery
                //Send email using System.Net.Mail
                try
                {
                    String EmailBody = "Hi " + Name + ",<br/><br/> Click the link below to unlock your account <br/><br/> https://localhost:44337/AccountRecoveryPasswordReset.aspx?Uid=" + linkuid;
                    MailMessage AddUserEmail = new MailMessage("TheToyStoreit2666@outlook.com", Email);
                    AddUserEmail.Body = EmailBody;
                    AddUserEmail.IsBodyHtml = true;
                    //SMTPClient Configuration *Note that google no longer works due to Restrictions on 3rd party apps (Less secure apps)
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Timeout = 5000; //Give a timeout of 60sec(1 min)
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("TheToyStoreit2666@outlook.com", "Windows10");
                    client.EnableSsl = true;
                    client.Credentials = credential;
                    client.Send(AddUserEmail);
                    
                    //Alert Users to Log In Immediately Via SMS
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    TwilioClient.Init(AccountSID, Auth_Token);
                    var message = MessageResource.Create(
                        body: "From TechStore: Dear " + Name + ". We have suspended your account due to multiple failed login attempts. Please check your registered email to unlock your account. ",
                        from: new Twilio.Types.PhoneNumber("+17262247257"), //Only change this number if it expires
                        to: new Twilio.Types.PhoneNumber(PhoneNumber)
                          );
                    Console.Write(message.Sid);
                }
                catch
                {
                    lblMsg.CssClass = "text-danger text-warning";
                    lblMsg.Text = "There was an error sending an email";
                }
                UserID = Convert.ToInt32(dt.Rows[0][3].ToString());
                SqlCommand cmd2 = new SqlCommand("INSERT INTO AccountRecovery VALUES(@GUID,@UserID)", con);
                cmd2.Parameters.AddWithValue("@GUID", linkuid);
                cmd2.Parameters.AddWithValue("@UserID", UserID);
                cmd2.ExecuteNonQuery();
                if (CheckSecurityQuesEnabled())
                {
                    DisplaySecQuesModal();
                }
                else
                {
                    Session["UserAccEmailRec"] = UserID.ToString();
                }
            }
        }
        private int AccountLockoutStatus() //Returns the INT count of Login Attempts
        {
            int LoginAttemptCount;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmdGetLoginAttemptFromDB = new SqlCommand("SELECT LoginAttemptCount FROM Users WHERE Username=@Username", con);
            cmdGetLoginAttemptFromDB.Parameters.AddWithValue("@Username", txtUsername.Text); //Gets the Login Attempt from Users Table where the usernames match
            SqlDataAdapter sda = new SqlDataAdapter(cmdGetLoginAttemptFromDB);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count != 0) //If username found
            {
                LoginAttemptCount = Convert.ToInt32(dt.Rows[0][0].ToString()) + 1;
                System.Diagnostics.Debug.WriteLine(LoginAttemptCount);
                //Update the Login Attempt by 1
                SqlCommand cmdUpdateLoginAttempt = new SqlCommand("UPDATE Users SET LoginAttemptCount=@LoginAttemptCount WHERE Username=@Username", con);
                cmdUpdateLoginAttempt.Parameters.AddWithValue("@LoginAttemptCount", LoginAttemptCount);
                cmdUpdateLoginAttempt.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmdUpdateLoginAttempt.ExecuteNonQuery();
                return LoginAttemptCount;
            }
            else
            {
                return 0;
            }
        }
        private bool ValidateUserCredentials()
        {
            if (txtPassword.Text != "" && txtUsername.Text != "") //Ensure both fields are filled
            {
                if (AccountLockoutStatus() < 3)
                {
                    string formUsername = txtUsername.Text;
                    string formPassword = txtPassword.Text;
                    SqlConnection con = new SqlConnection(CS);
                    con.Open();
                    SqlCommand getuserdetailbasedonform = new SqlCommand("SELECT UserID, UserName, Password FROM Users WHERE UserName = '" + formUsername + "'", con);
                    SqlDataAdapter sdauserformhashedpw = new SqlDataAdapter(getuserdetailbasedonform);
                    DataTable dtgetuserhashedpw = new DataTable();
                    sdauserformhashedpw.Fill(dtgetuserhashedpw);
                    if (dtgetuserhashedpw.Rows.Count > 0) //Username is found (Only 1 Username should be found as Username is supposed to be Unique)
                    {
                        string userdbhashedpw = dtgetuserhashedpw.Rows[0][2].ToString();
                        Int32 userIDinDB = Convert.ToInt32(dtgetuserhashedpw.Rows[0][0].ToString());
                        System.Diagnostics.Debug.WriteLine(BCrypt.Net.BCrypt.Verify(formPassword, userdbhashedpw));
                        if (BCrypt.Net.BCrypt.Verify(formPassword, userdbhashedpw)) //Returns true if Entered password == Form Password (Uses Bcrypt Verify Method)
                        {
                            SqlCommand cmdUpdateLoginAttempt = new SqlCommand("UPDATE Users SET LoginAttemptCount=@LoginAttemptCount WHERE Username=@Username", con);
                            cmdUpdateLoginAttempt.Parameters.AddWithValue("@LoginAttemptCount", 0);
                            cmdUpdateLoginAttempt.Parameters.AddWithValue("@Username", txtUsername.Text);
                            cmdUpdateLoginAttempt.ExecuteNonQuery();

                            string UserType = "";
                            SqlCommand cmd = new SqlCommand("SELECT UserType FROM Users WHERE UserID=" + userIDinDB + "", con);
                            DataTable dtusertype = new DataTable();
                            SqlDataAdapter sdausertype = new SqlDataAdapter(cmd);
                            sdausertype.Fill(dtusertype);
                            if (dtusertype.Rows.Count > 0)
                            {
                                Session["LoginID"] = userIDinDB;
                                return true;
                            }
                            else
                            {
                                lblMsg.CssClass = "text-danger text-warning";
                                lblMsg.Text = "There Is An Issue With Your Account. Please Try Again Later";
                                return false;
                            }
                        }
                        else
                        {
                            lblMsg.CssClass = "text-danger text-warning";
                            lblMsg.Text = "Login failed; Invalid user ID or password."; //Wrong Password NOTE: Display Generic MSG As PER Security Policy (1)
                            return false;
                        }
                    }
                    else
                    {
                        lblMsg.CssClass = "text-danger text-warning";
                        lblMsg.Text = "Login failed; Invalid user ID or password."; //Wrong Username NOTE: Display Generic MSG As PER Security Policy (1)
                        return false;
                    }

                }
                else
                {
                    AccountCompromiseAlertUsersEmail_SMS();
                    lblMsg.CssClass = "text-danger text-warning";
                    lblMsg.Text = "Your Account Has Been Locked. Please Unlock Your Account Via Your Email Address";
                    return false;
                }
            }
            else
            {
                lblMsg.CssClass = "text-danger text-warning";
                lblMsg.Text = "Please Enter Your Username And Password";
                return false;

            }
        }
        protected void btnEmail2FA_Click(object sender, EventArgs e)
        {
            if (ValidateUserCredentials())
            {
                Response.Redirect("Login_2FA");

                if (chckbxRemMe.Checked) //Enable Remeber Me Function
                {
                    Response.Cookies["UNAME"].Value = txtUsername.Text;
                    Response.Cookies["PWD"].Value = txtUsername.Text;

                    Response.Cookies["UNAME"].Expires = DateTime.Now.AddMinutes(60);
                    Response.Cookies["PWD"].Expires = DateTime.Now.AddMinutes(60);
                }
                else 
                {
                    Response.Cookies["UNAME"].Value = "";
                    Response.Cookies["PWD"].Value = "";
                    Response.Cookies["UNAME"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["PWD"].Expires = DateTime.Now.AddDays(-1);
                }
            }
        }

        protected void btnRedirectAnswerSecurityQues_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Username: " + txtUsername.Text);
            Session["AccRecUserID"] = GetUserID();
            System.Diagnostics.Debug.WriteLine("UserID: " + Session["AccRecUserID"]);
            Response.Redirect("AccountRecoverySecurityQuestionVerification");
        }

        protected void btnPasswordlessLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginDeviceProxAuth");
        }
    }
}