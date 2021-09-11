using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class SIF_SatuanRepo
    {
        public static List<SIF_Satuan> GetSIFSatuanCbo(string kode_barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT S.Kode_Satuan,S.Nama_Satuan " +
                    " FROM SIF_Satuan S WITH(NOLOCK) " +
                    " INNER JOIN SIF_Barang B WITH(NOLOCK) ON B.Kd_Satuan=S.Kode_Satuan " +
                    " WHERE S.Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();
                if (kode_barang != null)
                {
                    param.Add("@kode_barang", kode_barang);
                    sql += " AND B.Kode_Barang=@kode_barang ";
                }
                sql += " GROUP BY S.Kode_Satuan,S.Nama_Satuan ORDER BY Nama_Satuan ";

                var res = con.Query<SIF_Satuan>(sql, param);

                return res.ToList();
            }
        }

        public static List<SIF_Satuan> GetSatuanALL()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kode_Satuan,Nama_Satuan FROM SIF_Satuan S WITH(NOLOCK) WHERE Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();
         
                

                var res = con.Query<SIF_Satuan>(sql, param);

                return res.ToList();
            }
        }

    }
}
