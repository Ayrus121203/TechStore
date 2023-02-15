using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using AjaxControlToolkit;

namespace TechStore
{
    public partial class Default : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        private void BindNewest8Prods()
        {
            SqlConnection con = new SqlConnection(CS);

            //SqlCommand cmd = new SqlCommand("SELECT TOP 8 PNAME FROM tblProducts ORDER BY PNAME DESC;", con);
            using (SqlCommand cmd = new SqlCommand("procRetrieveNewest8ProdDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dtnewest8prod = new DataTable();
                    sda.Fill(dtnewest8prod);
                    //procBindAllProducts
                    rptrNew8Prod.DataSource = dtnewest8prod;
                    rptrNew8Prod.DataBind();
                }
            }
        }
        private void BindBest8Prods()
        {
            SqlConnection con = new SqlConnection(CS);
            using (SqlCommand cmd = new SqlCommand("procRetrieveBest8ProdDetails", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dtbest8prod = new DataTable();
                    sda.Fill(dtbest8prod);
                    //procBindAllProducts
                    rptrFeaturedProd.DataSource = dtbest8prod;
                    rptrFeaturedProd.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewest8Prods();
                BindBest8Prods();
            }
        }
    }
}