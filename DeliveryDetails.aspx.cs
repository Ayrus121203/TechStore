using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace TechStore
{
    public partial class BillingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private bool ValidateDeliveryDetails()
        {
            if(txtBillingAddress.Text!="" && txtName.Text != "" && txtPhoneNum.Text != "")
            {
                lblMsg.Text = "";
                return true;
            }
            else
            {
                lblMsg.CssClass = "text-danger text-warning";
                lblMsg.Text = "All Fields Are Mandatory";
                //All fields mandatory
                return false;
            }
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            if (ValidateDeliveryDetails())
            {
                Session["DeliveryAddress"] = txtBillingAddress.Text;
                Session["Name"] = txtName.Text;
                Session["Phonenum"] = txtPhoneNum.Text;
                Response.Redirect("Checkout");
            }
        }
    }
}