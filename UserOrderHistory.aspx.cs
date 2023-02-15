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
    public partial class UserOrderHistory : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                if (!IsPostBack)
                {
                    BindRptrOrders();
                }
            }
            else
            {
                Response.Redirect("Login");
            }
        }

        private void BindRptrOrders()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT InvoiceID, DateOfOrder, OrderAmt, OrderStatus FROM OrderPurchase WHERE UserID=@UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                rptrOrders.DataSource = dt;
                rptrOrders.DataBind();
            }

        }
    }
}