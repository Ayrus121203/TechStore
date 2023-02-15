using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

namespace TechStore
{
    public partial class PaymentSuccess : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                if (Session["DeliveryAddress"] != null && Session["Phonenum"] != null && Session["Name"] !=null && Session["CartSubTotal"] !=null && Session["TotalPayment"]!=null)
                {
                    
                    if (!IsPostBack)
                    {
                        CreateOrder();
                    }
                }
                else
                {
                    Response.Redirect("Cart");
                }
            }
            else
            {
                Response.Redirect("Home");
            }
        }

        public void BindRptrOrderDetail()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT P.ProductID, ProdName, CartProdQuantity, P.ProdSellPrice, CartProdPrice FROM Cart C INNER JOIN Products P ON C.ProductID = P.ProductID", con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrOrderProd.DataSource = dt;
                rptrOrderProd.DataBind();
            }
        }

        private void CreateOrder()
        {
            BindRptrOrderDetail();

            Guid InvoiceID = Guid.NewGuid();

            List<int> ProdIDs = new List<int>();
            List<int> OrderQuan = new List<int>();

            string DateofOrder = DateTime.Now.ToLongDateString();
            string BuyerName = Session["Name"].ToString();
            string BillingAddress = Session["DeliveryAddress"].ToString();
            string PhoneNum = Session["Phonenum"].ToString();
            string OrderStatus = "PAID";
            int UserID = Convert.ToInt32(Session["UserID"].ToString());
            string OrderAmt = Session["TotalPayment"].ToString();


            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT CartProdQuantity, ProductID FROM Cart WHERE UserID = @UserID", con);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    OrderQuan.Add(Convert.ToInt32(dt.Rows[i][0]));
                    ProdIDs.Add(Convert.ToInt32(dt.Rows[i][1]));
                }
                //Insert to OrderPurchase
                SqlCommand cmd2 = new SqlCommand("INSERT INTO OrderPurchase VALUES(@InvoiceID, @DateOfOrder, @BuyerName, @UserBillingAddress, @UserPhoneNum, @OrderStatus, @UserID, @OrderAmt)", con);
                cmd2.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                cmd2.Parameters.AddWithValue("@DateOfOrder", DateofOrder);
                cmd2.Parameters.AddWithValue("@BuyerName", BuyerName);
                cmd2.Parameters.AddWithValue("@UserBillingAddress", BillingAddress);
                cmd2.Parameters.AddWithValue("@UserPhoneNum", PhoneNum);
                cmd2.Parameters.AddWithValue("@OrderStatus", OrderStatus);
                cmd2.Parameters.AddWithValue("@UserID", UserID);
                cmd2.Parameters.AddWithValue("@OrderAmt", OrderAmt);
                cmd2.ExecuteNonQuery();

                for (int i = 0; i < ProdIDs.Count; i++)
                {
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO OrderPurchaseProducts VALUES(@InvoiceID, @ProductID, @OrderedQuantity)", con);
                    cmd3.Parameters.AddWithValue("@InvoiceID", InvoiceID);
                    cmd3.Parameters.AddWithValue("@ProductID", ProdIDs[i]);
                    cmd3.Parameters.AddWithValue("@OrderedQuantity", OrderQuan[i]);
                    cmd3.ExecuteNonQuery();
                }

                SqlCommand cmd4 = new SqlCommand("DELETE FROM Cart WHERE UserID=@UserID", con);
                cmd4.Parameters.AddWithValue("@UserID", UserID);
                cmd4.ExecuteNonQuery();

                string CartSubTotal = Session["CartSubTotal"].ToString();
                string CartTotal = Session["TotalPayment"].ToString();
                //Fill The UI
                name.InnerText = BuyerName;
                billaddress.InnerText = BillingAddress;
                phonenum.InnerText = PhoneNum;
                orderdate.InnerText = DateofOrder;
                subtot.InnerText = "$" + CartSubTotal;
                tot.InnerText = CartTotal;
                invoice.InnerText = InvoiceID.ToString();
                invoice_top.InnerText = InvoiceID.ToString();
            }
        }
    }
}