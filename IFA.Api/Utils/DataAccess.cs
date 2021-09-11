using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Utils
{
    public class DataAccess
    {
        public static string GetConnectionString()
        {
            return Startup.ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            var cn = new SqlConnection(GetConnectionString());
            cn.Open();
            return cn;
        }

        public static SqlTransaction OpenTransaction(SqlConnection conn)
        {
            var trans = conn.BeginTransaction();
            return trans;
        }

        public static void CloseTransaction(SqlConnection conn, SqlTransaction trans, bool isCommit)
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                if (isCommit)
                {
                    trans.Commit();
                }
                else
                {
                    trans.Rollback();
                }
            }
        }

        public static void DisposeConnectionAndTransaction(SqlConnection conn, SqlTransaction trans)
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                trans.Dispose();
                conn.Close();
            }
        }
    }
}
