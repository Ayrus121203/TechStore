using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechStore
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AdminID"] != null)
            {

            }
            else
            {
                Response.Redirect("Home");
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session["AdminID"] = null;
            Response.Redirect("Home");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}