using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TechStore.Domain;
using Dapper;
namespace TechStore.DAL
{
    public class dalWebsiteRatingChartView
    {
        public static List<clsWebisteRating> FetchList()
        {
            var sql = "SELECT COUNT(WebsiteRating) Ratings FROM WebsiteRating WR Where WebsiteRating=1 UNION ALL SELECT COUNT(WebsiteRating) Ratings FROM WebsiteRating WR Where WebsiteRating=2 UNION ALL SELECT COUNT(WebsiteRating) Ratings FROM WebsiteRating WR Where WebsiteRating=3 UNION ALL SELECT COUNT(WebsiteRating) Ratings FROM WebsiteRating WR Where WebsiteRating=4 UNION ALL SELECT COUNT(WebsiteRating) Ratings FROM WebsiteRating WR Where WebsiteRating=5";
            using (var con = ConnectionUtil.GetConnection())
            {
                return con.Query<clsWebisteRating>(sql).ToList();

            }
        }
    }
}