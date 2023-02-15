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
    public partial class SearchResult : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Search"] != null)
                {
                    BindSearchResult();
                }
                else
                {
                    Response.Redirect("Home");
                }
            }
        }

        private void BindSearchResult()
        {
            string txtsearch = Session["Search"].ToString();
            SqlConnection con = new SqlConnection(CS);
            string strCommandText = "select A.*, B.*, ROUND(A.ProdUnitPrice, 2) UnitPrice, ROUND(A.ProdUsualPrice, 2) UsualPrice, ROUND(A.ProdSellPrice,2) SellPrice, B.Name as ImageName from Products A cross apply(select top 1 * from ProductImages B where B.ProductID = A.ProductID order by B.ProductID desc) B WHERE A.ProdName LIKE @ProdName OR A.ProdBrand LIKE @ProdBrand order by A.ProductID ASC";
            SqlCommand cmd = new SqlCommand(strCommandText, con);
            //declare cmd parameters for title and author to be dispayed
            cmd.Parameters.AddWithValue("@ProdName", "%" + txtsearch + "%");
            cmd.Parameters.AddWithValue("@ProdBrand", "%" + txtsearch + "%");
            //open the connection
            con.Open();
            cmd.ExecuteNonQuery();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                flashes.Visible=false;
                prodresults.Visible = true;
                rptrProdsSearchResult.DataSource=dt;
                rptrProdsSearchResult.DataBind();
            }
            else
            {
                flashes.Visible = true;
                prodresults.Visible = false;
            }
            Session["Search"] = null;
        }
    }
}