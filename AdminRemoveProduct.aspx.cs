using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TechStore
{
    public partial class AdminRemoveProduct : System.Web.UI.Page
    {
        String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            DeleteProd();
        }
        private void DeleteProd()
        {
            Int32 ProductID = Convert.ToInt32(Request.QueryString["ProductID"]);
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd1 = new SqlCommand("DELETE FROM ProductImages WHERE ProductID=" + ProductID + "", con);
            con.Open();
            cmd1.ExecuteNonQuery();
            SqlCommand cmd2 = new SqlCommand("DELETE FROM Products WHERE ProductID=" + ProductID + "", con);
            cmd2.ExecuteNonQuery();
            SqlCommand cmd3 = new SqlCommand("DELETE FROM RATINGS WHERE ProductID=@ProdID", con);
            cmd3.Parameters.AddWithValue("@ProdID", ProductID);
            cmd3.ExecuteNonQuery();
            Response.Redirect("AdminHome");
        }
    }
}