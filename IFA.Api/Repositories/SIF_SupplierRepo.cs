using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public  class SIF_SupplierRepo
    {
        public static List<SIF_Supplier> GetSIFSupplierCbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kode_Supplier as kd_Supplier,Nama_Supplier " +
                    " FROM [SIF].[dbo].[SIF_Supplier] WITH(NOLOCK) WHERE Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Supplier ";

                var res = con.Query<SIF_Supplier>(sql, param);

                return res.ToList();
            }
        }

        public static List<SIF_Supplier> GetAll_Supplier()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * " +
                    " FROM SIF_Supplier WITH(NOLOCK) WHERE Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Supplier ";

                var res = con.Query<SIF_Supplier>(sql, param);

                return res.ToList();
            }
        }

    }
}
