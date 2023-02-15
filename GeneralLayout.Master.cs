using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TechStore
{
    public partial class GeneralLayout : System.Web.UI.MasterPage
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckforUserSession();
                BindCartNumber();
                BindRptrBrandFilter();
                BindRptrCatFilter();
            }
        }
        



        private void CheckforUserSession()
        {
            if (Session["UserID"] != null) //Signed In
            {
                userprof.Visible = true;
                btnSignOut.Visible = true;
                btnSignin.Visible = false;
                btnSignup.Visible = false;
            }
            else
            {
                userprof.Visible = false;
                btnSignOut.Visible = false;
                btnSignin.Visible = true;
                btnSignup.Visible = true;
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            string UserID = Session["UserID"].ToString();
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("DELETE FROM Cart WHERE UserID ='" + Convert.ToInt32(UserID) + "' ", con);
            con.Open();
            cmd.ExecuteNonQuery();
            Session["UserID"] = null;
            Response.Redirect("Home");
        }
        protected void btnnewsignin_Click(object sender, EventArgs e)
        {
            string Username = "Username2";
            string password = "Passs";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM Users WHERE Username='" + Username + "' AND Password='" + password + "'", con);
            DataTable dtUsers = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dtUsers);
            if (dtUsers.Rows.Count != 0)
            {
                Session["UserID"] = dtUsers.Rows[0][0].ToString();
            }
            else
            {
                Response.Redirect("Error");
            }
        }
        public void BindCartNumber()
        {

            if (Session["UserID"]!=null)
            {
                int userID = Convert.ToInt32(Session["UserID"].ToString());
                SqlConnection con = new SqlConnection(CS);
                SqlCommand cmd = new SqlCommand("SELECT COUNT(CartID) FROM Cart WHERE UserID=" + userID + "", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dtcartnum = new DataTable();
                sda.Fill(dtcartnum);
                if (dtcartnum.Rows.Count != 0)
                {
                    int cartcount = Convert.ToInt32(dtcartnum.Rows[0][0].ToString());
                    System.Diagnostics.Debug.WriteLine(cartcount);
                    pCount.InnerText = cartcount.ToString();
                }
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            Session["Search"] = txtsearch.Text;
            Response.Redirect("Search");
        }
        private void BindRptrBrandFilter()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT * FROM ProductBrands", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rptrBrands.DataSource = dt;
            rptrBrands.DataBind();
        }
        private void BindRptrCatFilter()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT * FROM ProductCategories", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            rptrCat.DataSource = dt;
            rptrCat.DataBind();
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }

        protected void btnSubmitFeedback_Click(object sender, EventArgs e)
        {
            Int32 UserID;
            if (Session["UserID"] != null)
            {
                UserID = Convert.ToInt32(Session["UserID"].ToString());
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                //insert rating into database
                SqlCommand cmd = new SqlCommand("INSERT INTO WebsiteRating VALUES (@WebsiteRating, @Feedback)", con);
                SqlDataAdapter sda = new SqlDataAdapter();
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@WebsiteRating", UserInputRatings.CurrentRating.ToString());
                cmd.Parameters.AddWithValue("@Feedback", txtFeedback.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                Session["UserID"] = null;
                Response.Redirect("Home");
            }
            else
            {
                Response.Redirect("Login");
            }
        }
    }
}