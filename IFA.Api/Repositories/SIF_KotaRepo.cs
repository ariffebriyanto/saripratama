using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Api.Repositories
{
    public class SIF_KotaRepo
    {
        public static IEnumerable<SIF_Kota> GetSIFKota(string kode_kota = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT Kd_Cabang,Kode_Kota,Nama_Kota,Keterangan,Rec_Stat,Last_Create_Date,Last_Created_By,Last_Update_Date,Last_Updated_By,Program_Name  " +
                    " FROM SIF_Kota  WHERE Rec_Stat='Y'  ";
                   

                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_kota", kode_kota);
                    
                if (kode_kota != string.Empty && kode_kota != null)
                {
                    sql += " AND kode_kota=@kode_kota ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<SIF_Kota>(sql, param);

                return res;
            }
        }

        public static int SaveKota(SIF_Kota data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF_Kota WITH(ROWLOCK) (Kode_Kota, Kd_Cabang, Nama_Kota, Keterangan, Last_Created_By, Last_Create_Date, Rec_Stat) " +
                    "VALUES(@Kode_Kota, @Kd_Cabang, @Nama_Kota, @Keterangan, @Last_Created_By, @Last_Create_Date, @Rec_Stat);";
            param = new DynamicParameters();
            param.Add("@Kode_Kota", data.Kode_Kota);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Kota", data.Nama_Kota);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Rec_Stat", data.Rec_Stat);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int UpdateKota(SIF_Kota data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF_Kota WITH(ROWLOCK) SET  Kd_Cabang=@Kd_Cabang, Nama_Kota=@Nama_Kota, Keterangan=@Keterangan, Last_Created_By=@Last_Created_By, Last_Create_Date=@Last_Create_Date, Rec_Stat=@Rec_Stat " +
                    " WHERE Kode_Kota=@Kode_Kota;";
            param = new DynamicParameters();
            param.Add("@Kode_Kota", data.Kode_Kota);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Nama_Kota", data.Nama_Kota);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Rec_Stat", data.Rec_Stat);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static SIF_Kota assignData(SIF_Kota data)
        {
            SIF_Kota SIF_KotaCont = new SIF_Kota();
            SIF_KotaCont.Kode_Kota = data.Kode_Kota;
            SIF_KotaCont.Kd_Cabang = data.Kd_Cabang;
            SIF_KotaCont.Nama_Kota = data.Nama_Kota;
            SIF_KotaCont.Keterangan = data.Keterangan;
            SIF_KotaCont.Rec_Stat = data.Rec_Stat;

            SIF_KotaCont.Last_Created_By = "SYSTEM";
            SIF_KotaCont.Last_Create_Date = DateTime.Now;

            return SIF_KotaCont;
        }
    }
}
