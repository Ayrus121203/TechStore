using AjaxControlToolkit;
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

namespace TechStore
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["EmailReset"] != null || Session["SecQuesPassReset"]!=null)
                {

                }
                else
                {
                    Response.Redirect("ForgotPasswordEmailVerify");
                }
            }
        }
        private void ChooseValidationType()
        {
            if (Session["EmailReset"] != null)
            {
                ValidateForm_Email();
            }
            else if(Session["SecQuesPassReset"] != null)
            {
                ValidateForm_SecQues();
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        private void ValidateForm_Email()
        {
            string email = Session["EmailReset"].ToString();
            System.Diagnostics.Debug.WriteLine(email);
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            if (txtNewPass.Text != "" && txtConPass.Text != "")
            {
                if (txtNewPass.Text == txtConPass.Text)
                {
                    if (ValidatePasswordStrength())
                    {
                        string hashedpw = BCrypt.Net.BCrypt.HashPassword(txtNewPass.Text, BCrypt.Net.BCrypt.GenerateSalt());
                        SqlCommand cmd2 = new SqlCommand("UPDATE Users SET Password=@Password WHERE Email=@Email", con);
                        cmd2.Parameters.AddWithValue("@Password", hashedpw);
                        cmd2.Parameters.AddWithValue("@Email", email);
                        cmd2.ExecuteNonQuery();
                        Session["EmailReset"] = null;
                        Response.Redirect("Login");
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
        private void ValidateForm_SecQues()
        {
            string username = Session["SecQuesPassReset"].ToString();
            System.Diagnostics.Debug.WriteLine(username);
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            if (txtNewPass.Text != "" && txtConPass.Text != "")
            {
                if (txtNewPass.Text == txtConPass.Text)
                {
                    if (ValidatePasswordStrength())
                    {
                        string hashedpw = BCrypt.Net.BCrypt.HashPassword(txtNewPass.Text, BCrypt.Net.BCrypt.GenerateSalt());
                        SqlCommand cmd2 = new SqlCommand("UPDATE Users SET Password=@Password WHERE Username=@Username", con);
                        cmd2.Parameters.AddWithValue("@Password", hashedpw);
                        cmd2.Parameters.AddWithValue("@Username", username);
                        cmd2.ExecuteNonQuery();
                        Session["SecQuesPassReset"] = null;
                        Response.Redirect("Login");
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
                lblMsg.CssClass = "text-danger text-warning";
                lblMsg.Text = "Please Choose A Stronger Password";
                return false;
            }
        }

        protected void btnResetPas_Click(object sender, EventArgs e)
        {
            ChooseValidationType();
        }
    }
}