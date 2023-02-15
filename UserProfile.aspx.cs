using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Web.UI.DataVisualization.Charting;

namespace TechStore
{
    public partial class UserProfile : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                if (!IsPostBack)
                {
                    BindRptrUserProfilePic();
                    BindRptrUserDetails();
                    DisplaySecurityQuestionSection();
                    DisplayDeviceProxDetails();
                }
            }
            else
            {
                Response.Redirect("Login");
            }
        }
        private void DisplaySecurityQuestionSection()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"].ToString());
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
        private void BindRptrUserProfilePic()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserID, ProfilePicName, ProfilePicExtension FROM Users WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrUserProfPic.DataSource = dt;
                rptrUserProfPic.DataBind();
            }
        }
        private void BindRptrUserDetails()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
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
        private void DisplayDeviceProxDetails()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
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

        protected void btnUpdateImg_Click(object sender, EventArgs e)
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT Username FROM Users WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("UserID",UserID);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            string Username = dt.Rows[0][0].ToString();
            //Insert and upload Images
            foreach (RepeaterItem item in rptrUserProfPic.Items)
            {
                var fuImg01 = item.FindControl("fuimg01") as FileUpload;
                if (fuImg01.HasFile)
                {
                    string SavePath = Server.MapPath("Images/UserProfilePics/") + UserID;
                    if (!Directory.Exists(SavePath))
                    {
                        Directory.CreateDirectory(SavePath);
                    }
                    string Extention = Path.GetExtension(fuImg01.PostedFile.FileName);
                    fuImg01.SaveAs(SavePath + "\\" + Username.Trim() + Extention);
                    SqlCommand cmd3 = new SqlCommand("UPDATE Users SET ProfilePicName = @ProfilePicName, ProfilePicExtension = @ProfilePicExtension WHERE UserID=@UserID", con);
                    cmd3.Parameters.AddWithValue("@ProfilePicName", Username.Trim());
                    cmd3.Parameters.AddWithValue("@ProfilePicExtension", Extention);
                    cmd3.Parameters.AddWithValue("@UserID", UserID);
                    cmd3.ExecuteNonQuery();
                }
            }
        }

        protected void btnUpdateUser_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptrUserDetails.Items)
            {
                var txtUsername = item.FindControl("txtUsername") as TextBox;
                var txtName = item.FindControl("txtName") as TextBox;
                var txtEmail = item.FindControl("txtEmail") as TextBox;
                var txtPhoneNumber = item.FindControl("txtPhoneNumber") as TextBox;

                Int32 UserID = Convert.ToInt32(Session["UserID"]);
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Username = @Username, Email=@Email, Name=@Name, PhoneNumber=@PhoneNumber WHERE UserID=@UserID", con);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("PhoneNumber", txtPhoneNumber.Text);
                cmd.Parameters.AddWithValue("UserID", UserID);
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnDeletAcc_Click(object sender, EventArgs e)
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmdDeleteUserOrders = new SqlCommand("DELETE FROM OrderPurchase WHERE UserID=@UserID", con);
            SqlCommand cmdDeleteUserRatings = new SqlCommand("DELETE FROM RATINGS WHERE UserID=@UserID", con);
            SqlCommand cmdDeleteUsers = new SqlCommand("DELETE FROM Users WHERE UserID=@UserID", con);

            cmdDeleteUserOrders.Parameters.AddWithValue("@UserID", UserID);
            cmdDeleteUserRatings.Parameters.AddWithValue("@UserID", UserID);
            cmdDeleteUsers.Parameters.AddWithValue("@UserID", UserID);

            cmdDeleteUserOrders.ExecuteNonQuery();
            cmdDeleteUserRatings.ExecuteNonQuery();
            cmdDeleteUsers.ExecuteNonQuery();
            Session["UserID"] = null;
            Response.Redirect("Home");
        }

        protected void btnEnableSecurityQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnableSecurityQuestions");
        }

        protected void btnRedirectToSecQues_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnableSecurityQuestions");
        }

        protected void btnDeviceProxSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("EnableDeviceProximityAuth");
        }
    }
}