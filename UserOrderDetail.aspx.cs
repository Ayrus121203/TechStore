using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Twilio.Rest.Media.V1;
using Twilio.TwiML.Messaging;

namespace TechStore
{
    public partial class UserOrderDetail : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"]!=null && Request.QueryString["InvoiceID"] != null)
                {
                    DisplayOrderDetails();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }
       
        private void DisplayOrderDetails()
        {
            Int32 UserID = Convert.ToInt32(Session["UserID"].ToString());
            string InvoiceID = Request.QueryString["InvoiceID"].ToString();
            string Name;
            string DateOfOrder;
            string UserBillingAddress;
            string UserPhoneNum;
            string OrderStatus;
            string CartSubTotal;
            string OrderAmt;

            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("procGetUserOrderDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrOrderProd.DataSource=dt;
                rptrOrderProd.DataBind();
                Name = dt.Rows[0]["Name"].ToString();
                DateOfOrder = dt.Rows[0]["DateOfOrder"].ToString();
                UserBillingAddress = dt.Rows[0]["UserBillingAddress"].ToString();
                UserPhoneNum = dt.Rows[0]["UserPhoneNum"].ToString();
                OrderStatus = dt.Rows[0]["OrderStatus"].ToString();
                double DoubleCartSubTotal = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DoubleCartSubTotal += Convert.ToDouble(dt.Rows[i]["CartSubTotal"].ToString());
                }
                CartSubTotal = DoubleCartSubTotal.ToString();
                OrderAmt = dt.Rows[0]["OrderAmt"].ToString();
                invoice_top.InnerText = InvoiceID;
                name.InnerText = Name;
                billaddress.InnerText = UserBillingAddress;
                phonenum.InnerText = UserPhoneNum;
                invoice.InnerText = InvoiceID;
                orderdate.InnerText = DateOfOrder;
                subtot.InnerText = CartSubTotal;
                tot.InnerText = OrderAmt;

            }
            else
            {
                Response.Redirect("Profile");
            }
        }
    }
}