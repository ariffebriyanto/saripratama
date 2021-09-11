using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class SIF_PegawaiRepo
    {
        public static List<SIF_Pegawai> GetSIFPegawaiCbo(string kode_pegawai = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF_Pegawai p WITH (NOLOCK) inner join SIF.dbo.SIF_cabang c WITH (NOLOCK) on  c.kd_cabang=p.Kd_Cabang  WHERE p.Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_pegawai", kode_pegawai);

                if (kode_pegawai != string.Empty && kode_pegawai != null)
                {
                    sql += " AND p.Kode_Pegawai=@kode_pegawai ";
                }

                sql += "  ORDER BY p.Last_Create_Date DESC, CASE WHEN p.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<SIF_Pegawai>(sql, param);

                return res.ToList();
            }
        }

        public static List<SIF_Pegawai> GetSopir(string kode_pegawai = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT * FROM SIF_Pegawai p WITH (NOLOCK)  WHERE p.Rec_Stat='Y'  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kode_pegawai", kode_pegawai);

                if (kode_pegawai != string.Empty && kode_pegawai != null)
                {
                    sql += " AND p.Kode_Pegawai=@kode_pegawai ";
                }

                sql += "  ORDER BY p.Nama_Pegawai DESC ";

                var res = con.Query<SIF_Pegawai>(sql, param);

                return res.ToList();
            }
        }

        public static List<MROLE> GetRole()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select IDROLE,NAMA from SIF.dbo.MROLE where IDROLE in ('PENJUALAN','LOGISTIK','SPV') ";


                DynamicParameters param = new DynamicParameters();
                // param.Add("@kode_pegawai", kode_pegawai);

                //if (kode_pegawai != string.Empty && kode_pegawai != null)
                //{
                //    sql += " AND p.Kode_Pegawai=@kode_pegawai ";
                //}

                //sql += "  ORDER BY p.Nama_Pegawai DESC ";

                var res = con.Query<MROLE>(sql, param);

                return res.ToList();
            }
        }

        public static async Task<int> SavePegawai(SIF_Pegawai data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.SIF_Pegawai (Kd_Cabang,Kode_Pegawai,NIP,NABS,no_ktp,Nama_Pegawai,Alamat_1,Alamat_2,Kode_Kota,No_Telepon1," +
                "No_telepon2,Tmp_lahir,Tgl_Lahir,Jns_Kelamin,Kode_Status,tgl_kerja,Rec_Stat,Last_Create_Date,Last_Created_By,Program_Name,sts_kb,userlogin) " +
                    "VALUES(@Kd_Cabang,@Kode_Pegawai,@NIP,@NABS,@no_ktp,@Nama_Pegawai,@Alamat_1,@Alamat_2,@Kode_Kota,@No_Telepon1," +
                    "@No_telepon2,@Tmp_lahir,@Tgl_Lahir,@Jns_Kelamin,@Kode_Status,@tgl_kerja,@Rec_Stat,GETDATE(),@Last_Created_By,@Program_Name,@sts_kb,@userlogin);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kode_Pegawai", data.Kode_Pegawai);
            param.Add("@Nama_Pegawai", data.Nama_Pegawai);
            param.Add("@NIP", data.NIP);
            param.Add("@no_ktp", data.no_ktp);
            param.Add("@NABS", data.NABS);
            param.Add("@Alamat_1", data.Alamat_1);
            param.Add("@Alamat_2", data.Alamat_2);
            param.Add("@Kode_Kota", data.Kode_Kota);
            param.Add("@No_Telepon1", data.No_Telepon1);
            param.Add("@No_telepon2", data.No_telepon2);
            param.Add("@Tmp_lahir", data.Tmp_lahir);
            param.Add("@Tgl_Lahir", data.Tgl_Lahir);
            param.Add("@Jns_Kelamin", data.Jns_Kelamin);
            param.Add("@Kode_Status", data.Kode_Status);
            param.Add("@tgl_kerja", data.tgl_kerja);
            param.Add("@Rec_Stat", "Y");
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@sts_kb", data.sts_kb); //userlogin
            param.Add("@userlogin", data.userlogin); //userlogin


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveUserRole(SIF_Pegawai data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.MUSER_ROLE (IDUSER,IDROLE) " +
                    "VALUES(@IDUSER,@IDROLE);";
            param = new DynamicParameters();
            param.Add("@IDUSER", data.userlogin);
            param.Add("@IDROLE", data.idrole);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveMUSER(SIF_Pegawai data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.MUSER (kd_cabang,userid,nama,passwd,rec_stat,akses_penjualan) " +
                    "VALUES(@kd_cabang,@userid,@nama,@passwd,@rec_stat,@akses_penjualan);";
            param = new DynamicParameters();
            param.Add("@kd_cabang", data.Kd_Cabang);
            param.Add("@userid", data.userlogin);
            param.Add("@nama", data.Kode_Pegawai);
            param.Add("@NIP", data.Kode_Pegawai);
            param.Add("@passwd", data.userlogin); //password default sama dgn nama
            param.Add("@rec_stat", "Y");
            param.Add("@akses_penjualan", data.akses_penjualan);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveSales(SIF_Pegawai data, string kd_sales, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO SIF.dbo.SIF_Sales   (Kd_Cabang,Kode_Sales,Kode_Pegawai,Nama_Sales,Rec_Stat) " +
                    "VALUES(@Kd_Cabang,@Kd_Sales,@Kode_Pegawai,@Nama_Sales,@Rec_Stat);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@Kd_Sales", kd_sales);
            param.Add("@Kode_Pegawai", data.Kode_Pegawai);
            param.Add("@Nama_Sales", data.Nama_Pegawai);

            param.Add("@Rec_Stat", "Y");




            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> UpdatePegawai(SIF_Pegawai data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_Pegawai   SET  Nama_Pegawai=@Nama_Pegawai,Alamat_1=@Alamat_1,No_Telepon1=@No_Telepon1,No_telepon2=@No_telepon2,Last_Updated_By=@Last_Updated_By, Last_Update_Date=GETDATE() " +
                    " WHERE Kode_Pegawai=@Kode_Pegawai;";
            param = new DynamicParameters();
            param.Add("@Kode_Pegawai", data.Kode_Pegawai);
            param.Add("@Nama_Pegawai", data.Nama_Pegawai);
            param.Add("@Alamat_1", data.Alamat_1);
            param.Add("@Alamat_2", data.Alamat_2);
            param.Add("@No_Telepon1", data.No_Telepon1);
            param.Add("@No_Telepon2", data.No_telepon2);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static SIF_Pegawai assignData(SIF_Pegawai data)
        {
            SIF_Pegawai SIF_PegawaiCont = new SIF_Pegawai();
            SIF_PegawaiCont.Kode_Pegawai = data.Kode_Pegawai;
            SIF_PegawaiCont.Nama_Pegawai = data.Nama_Pegawai;
            SIF_PegawaiCont.Last_Created_By = "SYSTEM";
            SIF_PegawaiCont.Last_Create_Date = DateTime.Now;

            return SIF_PegawaiCont;
        }



    }
}
