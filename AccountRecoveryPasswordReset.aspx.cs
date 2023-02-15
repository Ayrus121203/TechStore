using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.Http;

namespace TechStore
{
    public partial class AccountRecoveryPasswordReset : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserAccEmailRec"] != null || Session["UserAccRecoveryUserID"]!=null)
                {
                    if(Session["UserAccEmailRec"] != null)
                    {
                        Session["UserAccRecovery"] = Session["UserAccEmailRec"];
                    }
                    else if(Session["UserAccRecoveryUserID"] != null)
                    {
                        Session["UserAccRecovery"] = Session["UserAccRecoveryUserID"];
                    }
                    else
                    {
                        Response.Redirect("Login");
                    }
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }

        protected void btnResetPas_Click(object sender, EventArgs e)
        {
            Validateform();
        }
        private void Validateform()
        {
            Int32 UserID = Convert.ToInt32(Session["UserAccRecovery"].ToString());

            SqlConnection con = new SqlConnection(CS);
            con.Open();
            if (txtNewPass.Text != "" && txtConPass.Text != "")
            {
                if (txtNewPass.Text == txtConPass.Text)
                {
                    if (ValidatePasswordStrength())
                    {
                        if (VerifyNonReusePass())
                        {
                            string hashedpw = BCrypt.Net.BCrypt.HashPassword(txtNewPass.Text, BCrypt.Net.BCrypt.GenerateSalt());
                            SqlCommand cmd2 = new SqlCommand("UPDATE Users SET Password=@Password WHERE UserID=@UserID", con);
                            cmd2.Parameters.AddWithValue("@Password", hashedpw);
                            cmd2.Parameters.AddWithValue("@UserID", UserID);
                            cmd2.ExecuteNonQuery();
                            SqlCommand cmd3 = new SqlCommand("DELETE FROM AccountRecovery WHERE UserID=@UserID", con);
                            cmd3.Parameters.AddWithValue("@UserID", UserID);
                            cmd3.ExecuteNonQuery();
                            SqlCommand cmd4 = new SqlCommand("UPDATE Users SET LoginAttemptCount = @LoginAttemptCount WHERE UserID=@UserID", con);
                            cmd4.Parameters.AddWithValue("@LoginAttemptCount", 0);
                            cmd4.Parameters.AddWithValue("@UserID", UserID);
                            cmd4.ExecuteNonQuery();
                            Session["UserAccRecovery"] = null;
                            Response.Redirect("Login");
                        }
                        else
                        {
                            lblMsg.CssClass = "text-danger text-warning";
                            lblMsg.Text = "Please choose a new password";
                        }
                    }
                    else
                    {
                        lblMsg.CssClass = "text-danger text-warning";
                        lblMsg.Text = "Please choose a stronger password";
                    }
                }

                else
                {
                    lblMsg.CssClass = "text-danger text-warning";
                    lblMsg.Text = "Password must match";
                }
            }
            else
            {
                lblMsg.CssClass = "text-danger text-warning";
                lblMsg.Text = "Please enter a password";
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
            if (passwordstrengthRegex.IsMatch(txtNewPass.Text))
            {
                lblMsg.Text = "";
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool VerifyNonReusePass()
        {
            Int32 UserID = Convert.ToInt32(Session["UserAccRecovery"].ToString());
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Password FROM Users WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("UserID", UserID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                string hashedpw = dt.Rows[0][0].ToString();
                if (BCrypt.Net.BCrypt.Verify(txtConPass.Text, hashedpw))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}