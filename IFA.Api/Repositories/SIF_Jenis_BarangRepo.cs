using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class SIF_Jenis_BarangRepo
    {
        public static List<SIF_Jenis_Barang> GetSIFJenisBarangCbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kd_Jns_Brg,Nama_Jenis " +
                    " FROM SIF_Jenis_Barang   WHERE Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();

                sql += " ORDER BY Nama_Jenis ";

                var res = con.Query<SIF_Jenis_Barang>(sql, param);

                return res.ToList();
            }
        }

    }
}
