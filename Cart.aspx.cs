using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Xml.Linq;

namespace TechStore
{
    public partial class Cart : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] != null)
                {
                    BindCartProds();
                    GetCartTotal();
                }
                else
                {
                    Response.Redirect("Login");
                }
            }
        }
        private void BindCartProds()
        {
            //procRetrieveCartProds
            SqlConnection con = new SqlConnection(CS);

            //SqlCommand cmd = new SqlCommand("SELECT TOP 8 PNAME FROM tblProducts ORDER BY PNAME DESC;", con);
            using (SqlCommand cmd = new SqlCommand("procRetrieveCartProds", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dtCartProds = new DataTable();
                    sda.Fill(dtCartProds);
                    //procBindAllProducts
                    rptrCartProds.DataSource = dtCartProds;
                    rptrCartProds.DataBind();
                }
            }
        }
        private void GetCartTotal()
        {
            double CartSubTotal = 0;
            double CartTotal = 0;
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("procRetrieveCartProds", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dtCart = new DataTable();
            sda.Fill(dtCart);
            if (dtCart.Rows.Count != 0)
            {
                for(int index = 0; index < dtCart.Rows.Count; index++)
                {
                    double ProdSellPrice = Convert.ToDouble(dtCart.Rows[index]["ProdSellPrice"].ToString());
                    int ProdQuan = Convert.ToInt32(dtCart.Rows[index]["CartProdQuantity"].ToString());
                    CartSubTotal += (ProdSellPrice * ProdQuan);
                }
            }
            CartTotal = CartSubTotal + 4;
            Session["TotalPayment"] = CartTotal;
            Session["CartSubTotal"] = CartSubTotal;
            subtot.InnerText = "$ "+ CartSubTotal.ToString();
            tot.InnerText = "$ " + CartTotal.ToString();
            if (CartTotal <= 4)
            {
                flashes.Visible = true;
                cartsec.Visible = false;
                infosec.Visible = false;
                btn_ProceedToCheckout.Enabled = false;
            }
            else
            {
                flashes.Visible = false;
                cartsec.Visible = true;
                infosec.Visible = true;
                btn_ProceedToCheckout.Enabled = true;
            }
        }

        protected void btn_ProceedToCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryDetails");
        }
    }
}