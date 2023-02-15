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
    public partial class AddProductBrand : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindBrandsRptr();
        }
        private void BindBrandsRptr()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductBrands", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtBrands = new DataTable();
                        sda.Fill(dtBrands);
                        rptrBrands.DataSource = dtBrands;
                        rptrBrands.DataBind();
                    }

                }
            }
        }
        protected void btnAddBrand_Click(object sender, EventArgs e)
        {
            if (txtBrand.Text != "")
            {
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductBrands VALUES(@BrandName)", con);
                cmd.Parameters.AddWithValue("@BrandName", txtBrand.Text);
                cmd.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}