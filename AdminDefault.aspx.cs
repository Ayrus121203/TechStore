using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Twilio.TwiML.Voice;

namespace TechStore
{
    public partial class AdminDefault : System.Web.UI.Page
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDBProductRartingsToChart();
            GetDBWebisteRatingToChart();
            BindRptrUserCount();
            BindRptrTotOrders();
            BindRptrProductSold();
            BindRptrProfits();
        }

        private void BindRptrUserCount()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(UserID) UserCount FROM Users", con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrTotalUsers.DataSource = dt;
                rptrTotalUsers.DataBind();
            }
        }
        private void BindRptrTotOrders()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(InvoiceID) OrderCount FROM OrderPurchase", con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrTotOrders.DataSource = dt;
                rptrTotOrders.DataBind();
            }
        }
        private void BindRptrProductSold()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(OrderedQuantity),0) ProdsSold FROM OrderPurchaseProducts", con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrProductsSold.DataSource = dt;
                rptrProductsSold.DataBind();
            }
        }

        private void BindRptrProfits()
        {
            SqlConnection con = new SqlConnection(CS);
            con.Open();
            SqlCommand cmd = new SqlCommand("procGetProfits", con);
            cmd.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                rptrTtProfits.DataSource = dt;
                rptrTtProfits.DataBind();
            }
        }


        public void GetDBProductRartingsToChart()
        {
            var list = DAL.dalProdRatingsChartView.FetchList();

            if (list.Count > 0)
            {
                var chartData = "";
                var views = "";
                var labels = "";

                chartData += "<script>";

                foreach (var item in list)
                {
                    views += item.AverageRating + ",";
                    labels += "\"" + item.ProdName + "\",";
                }

                views = views.Substring(0, views.Length - 1);
                labels = labels.Substring(0, labels.Length - 1);

                chartData += " chartLabels = [" + labels + "]; chartData = [" + views + "];";
                chartData += "</script>";
                ltChartData.Text = chartData;
            }
        }
        public void GetDBWebisteRatingToChart()
        {
            var list = DAL.dalWebsiteRatingChartView.FetchList();

            if (list.Count > 0)
            {
                var chartData = "";
                var ratings = "";
                var labels = "1,2,3,4,5,";

                chartData += "<script>";

                foreach (var item in list)
                {
                    ratings += item.Ratings + ",";
                }

                ratings = ratings.Substring(0, ratings.Length - 1);

                chartData += " chartLabels = ["+labels+"]; chartData = [" + ratings + "];";
                chartData += "</script>";
                ltWebsiteRating.Text = chartData;
            }
        }
    }
}