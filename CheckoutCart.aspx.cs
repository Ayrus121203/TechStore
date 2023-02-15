using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Stripe;
using Stripe.Checkout;
using static System.Net.WebRequestMethods;

namespace TechStore
{
    public partial class CheckoutCart : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        public string sessionId = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null && Session["CartSubTotal"]!=null && Session["TotalPayment"]!=null && Session["DeliveryAddress"]!=null && Session["Name"]!=null && Session["Phonenum"]!=null)
            {
                GetCartConfirmDetails();
                
                Double Total = Convert.ToDouble(Session["TotalPayment"].ToString());
                Double SubTotal = Convert.ToDouble(Session["CartSubTotal"]);
                subtot.InnerText = "$ " + SubTotal.ToString();
                tot.InnerText = "$ " + Total.ToString();
                deliveryaddress.InnerText= Session["DeliveryAddress"].ToString();
                nameofbuyer.InnerText = Session["Name"].ToString();
                contactnum.InnerText = Session["Phonenum"].ToString();
                //Stripe
                StripeConfiguration.ApiKey = "sk_test_51MJo2iJ3Lmxbx4EBmNDvOMswligAyBy9nlWAVMcyo07Ohda9JVqKAvczh5T4nprXw9sH330xGgaykZ9J6RSjnWxv00XyALko5m"; //Put Secret Key

                var options = new SessionCreateOptions
                {
                    SuccessUrl = "https://localhost:44337/PaymentSuccess",
                    CancelUrl = "https://localhost:44371/Error",
                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Amount=Convert.ToInt32(Total*100),
                        Name = "The Gape",
                        Description="Payment To The Gape",
                        Quantity = 1,
                        Currency="sgd"

                    }
                }
                };
                var service = new SessionService();
                var session = service.Create(options);
                sessionId = session.Id;
                //End Stripe
            }
            else
            {
                Response.Redirect("Cart");
            }
        }

        protected void btnBuyNow_Click(object sender, EventArgs e)
        {
        }
        private void GetCartConfirmDetails()
        {
            //procRetrieveCartProds
            SqlConnection con = new SqlConnection(CS);

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
    }
}
