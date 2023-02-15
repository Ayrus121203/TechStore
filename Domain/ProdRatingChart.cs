using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechStore.Domain
{
    public class ProdRatingChart
    {
        public int RatingID { get; set; }

        public int AverageRating { get; set; }

        public string review { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public string ProdName { get; set;}
    }
}