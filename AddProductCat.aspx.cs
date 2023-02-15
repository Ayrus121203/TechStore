using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TechStore
{
    public partial class AddProductCat : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            BindCategoryRptr();
        }
        private void BindCategoryRptr()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductCategories", con))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        DataTable dtCategories = new DataTable();
                        sda.Fill(dtCategories);
                        rptrCategories.DataSource = dtCategories;
                        rptrCategories.DataBind();
                    }

                }
            }
        }
        protected void btnAddCat_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text != "")
            {
                SqlConnection con = new SqlConnection(CS);
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ProductCategories VALUES(@CategoryName)", con);
                cmd.Parameters.AddWithValue("@CategoryName", txtCatName.Text);
                cmd.ExecuteNonQuery();
                Response.Redirect(Request.RawUrl);
            }
        }


    }
}