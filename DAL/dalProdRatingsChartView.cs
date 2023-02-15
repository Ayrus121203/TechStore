using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechStore.Domain;
using System.Data;
using Dapper;

namespace TechStore.DAL
{
    public class dalProdRatingsChartView
    {
        public static List<ProdRatingChart> FetchList()
        {
            var sql = "SELECT P.ProdName, ISNULL(AVG(R.rating),0) AverageRating FROM Products P INNER JOIN RATINGS R ON P.ProductID = R.ProductID GROUP BY P.ProdName ORDER BY AverageRating DESC";
            var sql1 = "SELECT * FROM RATINGS";
            using (var con = ConnectionUtil.GetConnection())
            {
                return con.Query<ProdRatingChart>(sql).ToList();
             
            }
        }
    }
}