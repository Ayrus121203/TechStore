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
    public partial class AdminOrders : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminID"] != null)
                {
                    BindRptrOrders();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }
        private void BindRptrOrders()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT InvoiceID, DateOfOrder, OrderAmt, OrderStatus FROM OrderPurchase", con);
            con.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrOrders.DataSource = dt;
                rptrOrders.DataBind();
            }

        }
    }
}