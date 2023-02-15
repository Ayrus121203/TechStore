using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TechStore
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {
                if (!IsPostBack)
                {
                    DisplayDeviceProxDetails();
                    BindRptrUserDetails();
                    DisplaySecurityQuestionSection();
                }
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        protected void btnRedirectToSecQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnableSecurityQuestions");
        }
        private void BindRptrUserDetails()
        {
            Int32 UserID = Convert.ToInt32(Session["AdminID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT Username, Email, Name, PhoneNumber FROM Users WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrUserDetails.DataSource = dt;
                rptrUserDetails.DataBind();
            }
        }
        private void DisplaySecurityQuestionSection()
        {
            Int32 UserID = Convert.ToInt32(Session["AdminID"].ToString());
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM UserSecurityQuestions WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            System.Diagnostics.Debug.WriteLine(dt.Rows.Count);
            if (dt.Rows.Count == 0)
            {
                displaymodal.Visible = true;
            }
            else
            {
                displaymodal.Visible = false;
            }
        }
        private void DisplayDeviceProxDetails()
        {
            Int32 UserID = Convert.ToInt32(Session["AdminID"]);
            string DeviceAddress;
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT DeviceAddress FROM BluetoothDeviceAddress WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                DeviceAddress = dt.Rows[0][0].ToString();
                txtDeviceAddress.Attributes["value"] = DeviceAddress;
            }

        }
        protected void btnEnableSecurityQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnableSecurityQuestions");
        }
    }
}