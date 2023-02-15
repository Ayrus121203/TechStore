using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class AdminOrderDetails : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminID"] != null && Request.QueryString["InvoiceID"] != null)
                {
                    DisplayOrderDetails();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }
        private Int32 GetUserIDOnInvoiceID()
        {
            Int32 UserID;
            string InvoiceID = Request.QueryString["InvoiceID"].ToString();
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT UserID FROM OrderPurchase WHERE InvoiceID=@InvoiceID", con);
            cmd.Parameters.AddWithValue("@InvoiceID", InvoiceID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                UserID = Convert.ToInt32(dt.Rows[0][0].ToString());
                return UserID;
            }
            return -1;
        }
        private void DisplayOrderDetails()
        {
            if (GetUserIDOnInvoiceID() != -1)
            {
                Int32 UserID = GetUserIDOnInvoiceID();
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
                    rptrOrderProd.DataSource = dt;
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
                    Response.Redirect("AdminProfile");
                }
            }
        }
    }
}