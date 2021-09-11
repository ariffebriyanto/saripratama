using System;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
namespace IFA.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);

            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }

           // string sql = "EXEC AutoReverse ";

            DynamicParameters param = new DynamicParameters();
          //  var res = con.Query<int>(sql, param).FirstOrDefault();
        }
    }
}
