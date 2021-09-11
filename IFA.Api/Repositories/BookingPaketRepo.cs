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
    public class BookingPaketRepo
    {
        public static async Task<int> Insert(SIF_BOOKING_PAKET data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SIF].[dbo].[SIF_BOOKING_PAKET] WITH(ROWLOCK) ( Kd_Cabang, No_Paket, Tgl_Paket, Nama_Paket, " +
                " Tgl_Akhir_Paket, Harga_Paket, Status_Aktif, Total_qty, Departement, " +
                " Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, Program_Name, Biaya, Status) " +
                    "VALUES(@Kd_Cabang,@No_Paket,@Tgl_Paket,@Nama_Paket,@Tgl_Akhir_Paket,@Harga_Paket,@Status_Aktif,@Total_qty,@Departement,@Last_Create_Date,@Last_Created_By,@Last_Update_Date,@Last_Updated_By,@Program_Name,@Biaya,@Status);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_Paket", data.No_Paket);
            param.Add("@Tgl_Paket", data.Tgl_Paket);
            param.Add("@Nama_Paket", data.Nama_Paket);
            param.Add("@Tgl_Akhir_Paket", data.Tgl_Akhir_Paket);
            param.Add("@Harga_Paket", data.Harga_Paket);
            param.Add("@Status_Aktif", data.Status_Aktif);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Departement", data.Departement);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Biaya", data.Biaya);
            param.Add("@Status", data.Status);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> Update(SIF_BOOKING_PAKET data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "UPDATE [SIF].[dbo].[SIF_BOOKING_PAKET] WITH(ROWLOCK) SET Kd_Cabang=@Kd_Cabang, Tgl_Paket=@Tgl_Paket, Nama_Paket=@Nama_Paket, " +
                " Tgl_Akhir_Paket=@Tgl_Akhir_Paket, Harga_Paket=@Harga_Paket, Status_Aktif=@Status_Aktif, Total_qty=@Total_qty, Departement=@Departement, " +
                " Last_Create_Date=@Last_Create_Date, Last_Created_By=@Last_Created_By, Last_Update_Date=@Last_Update_Date, Last_Updated_By=@Last_Updated_By, Program_Name=@Program_Name, Biaya=@Biaya, Status=@Status" +
                " WHERE No_Paket=@No_Paket ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_Paket", data.No_Paket);
            param.Add("@Tgl_Paket", data.Tgl_Paket);
            param.Add("@Nama_Paket", data.Nama_Paket);
            param.Add("@Tgl_Akhir_Paket", data.Tgl_Akhir_Paket);
            param.Add("@Harga_Paket", data.Harga_Paket);
            param.Add("@Status_Aktif", data.Status_Aktif);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Departement", data.Departement);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Biaya", data.Biaya);
            param.Add("@Status", data.Status);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DeleteDetail(SIF_BOOKING_PAKET data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [SIF].[dbo].[SIF_BOOKING_PAKET_D] WITH(ROWLOCK) " +
                " WHERE No_Paket=@No_Paket;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO_D] set STATUS_DO='BATAL'  " +
            //  " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@No_Paket", data.No_Paket);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(SIF_BOOKING_PAKET_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "INSERT INTO [SIF].[dbo].[SIF_BOOKING_PAKET_D] WITH(ROWLOCK) " +
                " (Kd_Cabang, No_Paket, No_seq, Kd_Stok, Qty, harga, Kd_satuan, Keterangan, potongan, potongan_total, " +
                " Status, departemen, Last_create_date, Last_created_by, Programe_name, " +
                " kd_parent, No, ambil_bonus, Deskripsi) " +
                    "VALUES(@Kd_Cabang,@No_Paket,@No_seq,@Kd_Stok,@Qty,@harga,@Kd_satuan,@Keterangan,@potongan,@potongan_total,@Status,@departemen,@Last_create_date,@Last_created_by,@Programe_name,@kd_parent,@No,@ambil_bonus,@Deskripsi);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_Paket", data.No_Paket);
            param.Add("@No_seq", data.No_seq);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Qty", data.Qty);
            param.Add("@harga", data.harga);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Last_create_date", data.Last_create_date);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@potongan", data.potongan);
            param.Add("@potongan_total", data.potongan_total);
            param.Add("@Status", data.Status);
            param.Add("@departemen", data.departemen);
            param.Add("@potongan_total", data.potongan_total);
            param.Add("@potongan", data.potongan);
            param.Add("@Programe_name", data.Programe_name);
            param.Add("@kd_parent", data.kd_parent);
            param.Add("@No", data.No);
            param.Add("@ambil_bonus", data.ambil_bonus);
            param.Add("@Deskripsi", data.Deskripsi);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<SIF_BOOKING_PAKET> GetPaketByID(string No_Paket = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT  Kd_Cabang, No_Paket, Tgl_Paket, Nama_Paket, Tgl_Akhir_Paket, Harga_Paket, Status_Aktif, " +
                    " Total_qty, Departement, Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, " +
                    " Program_Name, Biaya, Status " +
                    " FROM SIF.dbo.SIF_BOOKING_PAKET WITH(NOLOCK)  ";
                if(No_Paket != null && No_Paket != "") {
                    sql += "WHERE No_Paket=@No_Paket";
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@No_Paket", No_Paket);
               
                var res = con.Query<SIF_BOOKING_PAKET>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public static async Task<IEnumerable<SIF_BOOKING_PAKET>> GetPaket(string No_Paket = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT  Kd_Cabang, No_Paket, Tgl_Paket, Nama_Paket, Tgl_Akhir_Paket, Harga_Paket, Status_Aktif, " +
                    " Total_qty, Departement, Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, " +
                    " Program_Name, Biaya, Status " +
                    " FROM SIF.dbo.SIF_BOOKING_PAKET WITH(NOLOCK) ORDER BY Last_Create_Date DESC";
                DynamicParameters param = new DynamicParameters();
                param.Add("@No_Paket", No_Paket);

                var res = con.Query<SIF_BOOKING_PAKET>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SIF_BOOKING_PAKET_D>> GetDODetailByID(string No_Paket)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT Kd_Cabang, No_Paket, No_seq, Kd_Stok, Qty, harga, Kd_satuan, Keterangan, potongan, " +
                    " potongan_total, Status, departemen, Last_create_date, Last_created_by, Last_update_date, " +
                    " Last_updated_by, Programe_name, kd_parent, No, [set], ambil_bonus, Deskripsi " +
                    " FROM SIF.dbo.SIF_BOOKING_PAKET_D WITH(NOLOCK) " +
                    " WHERE No_Paket=@No_Paket ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@No_Paket", No_Paket);

                var res = con.Query<SIF_BOOKING_PAKET_D>(sql, param, null, true, 36000);

                return res;
            }
        }

    }
}
