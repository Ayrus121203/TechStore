
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class ForgotPass_AnswerSecQuestion : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SecurityQuestions();
            }
        }
        private void SecurityQuestions()
        {
            List<string> securityQuestions = new List<string>();
            securityQuestions.Add("--SELECT--");
            securityQuestions.Add("What is your oldest sibling’s middle name?");
            securityQuestions.Add("What city were you born in?");
            securityQuestions.Add("In what city or town did your parents meet?");
            for (int i = 0; i < securityQuestions.Count; i++)
            {
                ddlQuestions.Items.Add(new ListItem(securityQuestions[i], i.ToString()));
            }
            for (int i = 0; i < ddlQuestions.Items.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(ddlQuestions.SelectedItem.Value.GetType());
            }
        }
        private Int32 GetUserID()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM Users WHERE Username=@Username",con);
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
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Incorrect Username/Answer/Question";
            }
            return -1;

        }

        private void VerifySecQuesAndAns()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            if(GetUserID() != -1)
            {
                Int32 UserID = GetUserID();
                string selectedQuesID = ddlQuestions.SelectedItem.Value;
                string UserInputAns = txtInputAns.Text;
                SqlCommand cmd = new SqlCommand("SELECT SecurityAnswer FROM UserSecurityQuestions WHERE UserID=@UserID AND SecurityQuestionID=@SecurityQuestionID", con);
                cmd.Parameters.AddWithValue("UserID", UserID);
                cmd.Parameters.AddWithValue("@SecurityQuestionID", selectedQuesID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    string ActualAnswer = dt.Rows[0][0].ToString();
                    if (ActualAnswer == UserInputAns)
                    {
                        Session["PassReset"] = txtUsername.Text;
                        Response.Redirect("ResetPassword");
                    }
                }
                else
                {
                    lblMsg.CssClass = "text-danger";
                    lblMsg.Text = "Incorrect Username/Answer/Question";
                }
            }
        }
        

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            VerifySecQuesAndAns();
        }
    }
}