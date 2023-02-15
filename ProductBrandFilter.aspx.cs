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
    public partial class ProductBrandFilter : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BrandName"] != null)
                {
                    BindProductRepeater();
                }
                else
                {
                    Response.Redirect("~/Products.aspx");
                }
            }
        }
        private void BindProductRepeater()
        {
            string Brand = Request.QueryString["BrandName"].ToString();
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("procRetrieveFilterBrandsProducts", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SELECT");
                cmd.Parameters.AddWithValue("@ProdBrand", Brand);
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
    }
}


