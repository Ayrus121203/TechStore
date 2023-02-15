using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Design;

namespace TechStore
{
    public partial class Products : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private void BindProductRepeater()
        {
            string BrandName = Request.QueryString["BrandName"] == null ? "" : Request.QueryString["BrandName"];
            string CatName = Request.QueryString["CatName"] == null ? "" : Request.QueryString["CatName"];
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveAllProd", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (BrandName != "")
                {
                    cmd.Parameters.AddWithValue("@BrandName", BrandName);
                }
                if (CatName != "")
                {
                    cmd.Parameters.AddWithValue("@CatName", CatName);
                }
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
        private void BindProductRepeater_LowToHighPrice()
        {
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveAllProd_LowToHighPrice", con))
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
        private void BindProductRepeater_HighToLowPrice()
        {
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveAllProd_HighToLowPrice", con))
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

        private void BindProductRepeater_BestDiscounts()
        {
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveAllProd_BestDiscounts", con))
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
            BindProductRepeater();
        }

        protected void btnAllProds_Click(object sender, EventArgs e)
        {
            BindProductRepeater();
        }

        protected void btnLowPrice_Click(object sender, EventArgs e)
        {
            BindProductRepeater_LowToHighPrice();
        }

        protected void btnHighPrice_Click(object sender, EventArgs e)
        {
            BindProductRepeater_HighToLowPrice();
        }

        protected void btnDiscount_Click1(object sender, EventArgs e)
        {
            BindProductRepeater_BestDiscounts();
        }
    }
}