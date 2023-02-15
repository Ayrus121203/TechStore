using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class ForgotPasswordVerifyEmail : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            VerifyEmail();
        }
        
        private void VerifyEmail()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            
            
            SqlCommand cmd = new SqlCommand("SELECT Name, Email, UserID FROM Users WHERE Email=@Email", con);
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Int32 UserID= Convert.ToInt32(dt.Rows[0][2].ToString());
                SqlCommand getuseraccstatus = new SqlCommand("SELECT * FROM AccountRecovery WHERE UserID=@UserID", con);
                getuseraccstatus.Parameters.AddWithValue("@UserID", UserID);
                DataTable dtuserstat = new DataTable();
                SqlDataAdapter sdauserstat = new SqlDataAdapter(getuseraccstatus);
                sdauserstat.Fill(dtuserstat);
                System.Diagnostics.Debug.WriteLine(dtuserstat.Rows.Count);
                if (dtuserstat.Rows.Count == 0)
                {
                    //Email Found
                    lblMsg.CssClass = "text-success";
                    lblMsg.Text = "A link to reset your password has been emailed to the address provided.";

                    //Send email using System.Net.Mail
                    String ToEmailAddress = txtEmail.Text;
                    String Username = dt.Rows[0][0].ToString();
                    Guid linkuid = Guid.NewGuid();
                    String EmailBody = "Hi " + Username + ",<br/><br/> Click the link below to reset your password <br/><br/> https://localhost:44337/ResetPassword?Uid=" + linkuid;
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
                        Session["EmailReset"] = txtEmail.Text;
                    }
                    catch
                    {
                        lblMsg.CssClass = "text-danger";
                        lblMsg.Text = "An error has occurred. Please try again later";
                    }
                }
                else
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "This account has been suspended";
                }
            }
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "You have entered an incorrect email address";
            }
        }

        protected void btnRedirectAnswerSecurityQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("ForgotPasswordSecurityQuestionVerification");
        }
    }
}