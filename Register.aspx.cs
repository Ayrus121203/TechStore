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
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TechStore
{
    public partial class Register : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null) //User Signed In
            {
                Response.Redirect("Home");
            }
        }

        private bool ValidateRegisterForm()
        {
            if(txtUserName.Text != "" && txtName.Text!="" && txtEmail.Text!="" && txtPass.Text!="" && txtConPass.Text != "") //Check to ensure all fields are filled up
            {
                if(txtPass.Text == txtConPass.Text) //Ensure Password and Confirm password are the same
                {
                    if (ValidatePhoneNum())
                    {
                        if (ValidatePasswordStrength())
                        {
                            SqlConnection con = new SqlConnection(CS);
                            SqlCommand getexistinguser = new SqlCommand("SELECT Username, Email FROM Users", con);
                            con.Open();
                            DataTable dtusers = new DataTable();
                            SqlDataAdapter sda = new SqlDataAdapter(getexistinguser);
                            sda.Fill(dtusers);
                            List<string> usernamesindb = new List<string>();
                            List<string> emailsindb = new List<string>();
                            for (int i = 0; i < dtusers.Rows.Count; i++)
                            {
                                usernamesindb.Add(dtusers.Rows[i][0].ToString());
                                emailsindb.Add(dtusers.Rows[i][1].ToString());
                            }
                            if (!usernamesindb.Contains(txtUserName.Text)) //Ensure Username is Unique
                            {
                                if (!emailsindb.Contains(txtEmail.Text))
                                {
                                    if (chkbxRemPass.Checked)
                                    {
                                        lblError.Text = "";
                                        con.Close();
                                        return true;

                                    }
                                    else
                                    {
                                        lblError.CssClass = "text-danger text-warning";
                                        lblError.Text = "Please Accept Our Terms And Conditions";
                                        return false;
                                    }
                                }
                                else
                                {
                                    lblError.CssClass = "text-danger text-warning";
                                    lblError.Text = "Email Already Exists";
                                    return false;
                                }
                            }
                            else
                            {
                                lblError.CssClass = "text-danger text-warning";
                                lblError.Text = "Username Already Exists";
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    lblError.CssClass = "text-danger text-warning";
                    lblError.Text = "Passwords Must Match";
                    return false;
                }
            }
            else
            {
                lblError.CssClass = "text-danger text-warning";
                lblError.Text = "All Fields Are Mandatory";
                return false;
            }
        }

        private bool ValidatePasswordStrength()
        {
            string passwordstrengthRegexExp = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\\$%\\^&\\*])(?=.{8,128})";
            //(?=.*[a-z]) --> Password Must Contain 1 or more LOWERCASE letter
            //(?=.*[A-Z]) --> Password Must Contain 1 or more UPPERCASE letter
            //(?=.*[0-9]) --> Password Must Contain 1 or more Numeric
            //(?=.*[!@#\\$%\\^&\\*]) --> Password Must Contain 1 or more Special Characters (!@#$%^&*)
            //(?=.{8,128}) --> Password Must Be 8 Characters and Less Than 128 Characters (OWASP: Password must be < 128 chars)
            Regex passwordstrengthRegex = new Regex(passwordstrengthRegexExp);
            if (passwordstrengthRegex.IsMatch(txtPass.Text))
            {
                lblError.Text = "";
                return true;
            }
            else
            {
                lblError.CssClass = "text-danger text-warning";
                lblError.Text = "Please Choose A Stronger Password";
                return false;
            }
        }

        private bool ValidatePhoneNum()
        {
            string phonenumRegexExp = "^[0-9]{8}$";
            
            Regex PhoneNumRegex = new Regex(phonenumRegexExp);
            if (PhoneNumRegex.IsMatch(txttelnum.Text))
            {
                lblError.Text = "";
                return true;
            }
            else
            {
                lblError.CssClass = "text-danger text-warning";
                lblError.Text = "Ensure Your Phone Number Is Only 8 Digits";
                return false;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateRegisterForm()) //Calls the ValidateRegisterForm Function to check if function returns true
            {
                lblError.CssClass = "text-success";
                lblError.Text = "A link to activate your account has been emailed to the address provided.";

                //Send email using System.Net.Mail
                String ToEmailAddress = txtEmail.Text;
                String Username = txtUserName.Text;
                Guid linkuid = Guid.NewGuid();
                String EmailBody = "Hi " + Username + ",<br/><br/> Click the link below to register with us <br/><br/> https://localhost:44337/RegisterUserSMS.aspx?Uid=" + linkuid;
                MailMessage AddUserEmail = new MailMessage("TheToyStoreit2666@outlook.com", ToEmailAddress);
                AddUserEmail.Body = EmailBody;
                AddUserEmail.IsBodyHtml = true;
                try
                {
                    //SMTPClient Configuration *Note that google no longer works due to Restrictions on 3rd party apps (Less secure apps)
                    SmtpClient client = new SmtpClient("smtp-mail.outlook.com");
                    client.Port = 587;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    System.Net.NetworkCredential credential = new System.Net.NetworkCredential("TheToyStoreit2666@outlook.com", "Windows10");
                    client.EnableSsl = true;
                    client.Credentials = credential;
                    client.Send(AddUserEmail);
                }
                catch
                {
                    lblError.CssClass = "text-danger text-warning";
                    lblError.Text = "There was an error sending an email";
                    Response.Redirect("Login");
                }
                string Name = txtName.Text;
                string hashedpw = BCrypt.Net.BCrypt.HashPassword(txtPass.Text, BCrypt.Net.BCrypt.GenerateSalt());
                System.Diagnostics.Debug.WriteLine("Hashed pw: " + hashedpw);
                string Email = txtEmail.Text;
                string TelNo = txttelnum.Text;
                Session["Username"] = Username;
                Session["Name"] = Name;
                Session["Password"] = hashedpw;
                Session["Email"] = Email;
                Session["TelNo"] = TelNo;
              
            }
        }
    }
}