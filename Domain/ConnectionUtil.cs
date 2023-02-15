using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TechStore.Domain
{
    public class ConnectionUtil
    {
        public static String CS = ConfigurationManager.ConnectionStrings["MyDatabaseConnectionString1"].ConnectionString;
        public static SqlConnection GetConnection()
        {
            var con = new SqlConnection(CS);
            con.Open();
            return con;
        }
    }
}