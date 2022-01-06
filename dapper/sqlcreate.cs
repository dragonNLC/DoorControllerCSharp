using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Dapper
{
    public class sqlcreate
    {
        static string constr = "";
        static sqlcreate()
        {
            constr = System.Configuration.ConfigurationManager.ConnectionStrings["datastr"].ConnectionString;
        }
        public static SqlConnection getcon()
        {
            var connection = new SqlConnection(constr);
            connection.Open();
            return connection;

        }
    }
}
