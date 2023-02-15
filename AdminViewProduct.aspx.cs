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
    public partial class AdminEditProduct : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private void BindProductRepeater()
        {
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveAllProd", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dtAllProds = new DataTable();
                    sda.Fill(dtAllProds);
                    //procBindAllProducts
                    rptrProducts.DataSource = dtAllProds;
                    rptrProducts.DataBind();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProductRepeater();
            }
        }
    }
}