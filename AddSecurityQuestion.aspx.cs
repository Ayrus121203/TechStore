using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class AddSecurityQuestion : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null || Session["AdminID"]!=null)
            {
                if(Session["UserID"] != null)
                {
                    Session["AccountID"] = Session["UserID"];
                }
                else if(Session["AdminID"] != null)
                {
                    Session["AccountID"] = Session["AdminID"];
                }
                else
                {
                    Response.Redirect("Login");
                    Session["AccountID"] = null;
                }
                if (!IsPostBack)
                {
                    SecurityQuestions();
                }
            }
            else
            {
                Response.Redirect("Login");
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
        private bool IsQuestionIndexNOT0()
        {
            string selectedquestionID = ddlQuestions.SelectedItem.Value;
            if(selectedquestionID != "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Add_LinkAnsToQues_ToDB()
        {
            Int32 UserID = Convert.ToInt32(Session["AccountID"].ToString());
            string answer = txtAns.Text;
            string selectedquestionID = ddlQuestions.SelectedItem.Value;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO UserSecurityQuestions VALUES(@SecurityQuestionID, @SecurityAnswer, @UserID)", con);
            cmd.Parameters.AddWithValue("@SecurityQuestionID", selectedquestionID);
            cmd.Parameters.AddWithValue("SecurityAnswer", answer);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.ExecuteNonQuery();
        }
        private bool CheckExistingAnsQuesSetInDB() //Check if user already have a ans to the selected question. Display modal to check if want update
        {
            Int32 UserID = Convert.ToInt32(Session["AccountID"].ToString());
            string selectedquestionID = ddlQuestions.SelectedItem.Value;
            System.Diagnostics.Debug.WriteLine("Index: "+ddlQuestions.SelectedItem.Value);
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT SecurityAnswer FROM UserSecurityQuestions WHERE UserID=@UserID AND SecurityQuestionID=@SecurityQuestionID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@SecurityQuestionID", selectedquestionID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            System.Diagnostics.Debug.WriteLine(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateAns() //Updates the answer to selected ques if CheckExistingAnsQuesSetInDB() == true
        {
            Int32 UserID = Convert.ToInt32(Session["AccountID"].ToString());
            string answer = txtAns.Text;
            string selectedquestionID = ddlQuestions.SelectedItem.Value;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE UserSecurityQuestions SET SecurityAnswer=@SecurityAnswer WHERE UserID=@UserID AND SecurityQuestionID=@SecurityQuestionID", con);
            cmd.Parameters.AddWithValue("@SecurityAnswer", answer);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@SecurityQuestionID", selectedquestionID);
            cmd.ExecuteNonQuery();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsQuestionIndexNOT0())
            {
                if (CheckExistingAnsQuesSetInDB())
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append(@"<script language='javascript'>");
                    sb.Append(@"$('#myModal').modal('show');");
                    sb.Append(@"</script>");
                    ClientScript.RegisterStartupScript(this.GetType(), "JSScript", sb.ToString());
                }
                else
                {
                    Add_LinkAnsToQues_ToDB();
                    Response.Redirect("AdminProfile");
                }
            }
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Please choose a question";
            }
        }

        protected void btnUpdateAns_Click(object sender, EventArgs e)
        {
            UpdateAns();
            Response.Redirect("AdminProfile");
        }
    }
}