using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using ERP.Domain.Base;

namespace IFA.Api.Repositories
{
    public class DORepo
    {
        public static async Task<int> Save(SALES_SO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_SO] WITH(ROWLOCK) (Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, " +
                " Kd_Customer, Atas_Nama, Almt_pnrm, Kd_sales, Keterangan, Flag_Ppn, PPn, Total_qty, Departement, " +
                " Status, Last_Create_Date, Last_Created_By, Program_Name, Jenis_sp, JML_RP_TRANS, JML_VALAS_TRANS, " +
                " Tgl_Kirim_Marketing,  SP_REFF, Discount, Potongan, SP_REFF2, pending, STATUS_DO, Jatuh_Tempo, " +
                " Status_Simpan, dp,Biaya, inc_ongkir,jenis_so,no_paket) " +
                    "VALUES(@Kd_Cabang,@No_sp,@Tipe_trans,@Tgl_sp,@Kd_Customer,@Atas_Nama,@Almt_pnrm,@Kd_sales,@Keterangan," +
                    "@Flag_Ppn,@PPn,@Total_qty,@Departement,@Status,@Last_Create_Date,@Last_Created_By,@Program_Name,@Jenis_sp,@JML_RP_TRANS,@JML_VALAS_TRANS," +
                    "@Tgl_Kirim_Marketing,@SP_REFF,@Discount,@Potongan,@SP_REFF2,@pending,@STATUS_DO,@Jatuh_Tempo,@Status_Simpan,@dp,@Biaya, @inc_ongkir,@jenis_so,@no_paket);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_sp", data.No_sp);
            param.Add("@Tipe_trans", data.Tipe_trans);
            param.Add("@Tgl_sp", data.Tgl_sp);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Atas_Nama", data.Atas_Nama);
            param.Add("@Almt_pnrm", data.Almt_pnrm);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@PPn", data.PPn);
            param.Add("@Flag_Ppn", data.Flag_Ppn);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Departement", data.Departement);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Jenis_sp", data.Jenis_sp);
            param.Add("@JML_RP_TRANS", data.JML_RP_TRANS);
            param.Add("@JML_VALAS_TRANS", data.JML_VALAS_TRANS);
            param.Add("@Tgl_Kirim_Marketing", data.Tgl_Kirim_Marketing);
            param.Add("@SP_REFF", data.SP_REFF);
            param.Add("@Discount", data.Discount);
            param.Add("@Potongan", data.Potongan);
            param.Add("@SP_REFF2", data.SP_REFF2);
            param.Add("@pending", "");
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@Jatuh_Tempo", data.Jatuh_Tempo);
            param.Add("@Status_Simpan", data.Status_Simpan);
            param.Add("@Biaya", data.Biaya);
            param.Add("@dp", data.dp);
            param.Add("@inc_ongkir", data.inc_ongkir);
            param.Add("@jenis_so", data.jenis_so);
            param.Add("@no_paket", data.no_paket);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateBO(SALES_SO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
         
            DynamicParameters param = new DynamicParameters();

            if (data.tunda == true )
            {
                Query = "update sales.dbo.sales_so set pending = 'Y', status_do='PENDING DO' where no_sp = @IDSP; " +
                "update sales.dbo.sales_so_d set pending = 'Y', status_do='PENDING DO' where no_sp = @IDSP;";
            }
            else
            {
                Query = "update sales.dbo.sales_so_d set status_do='PERSIAPAN BARANG',Status_Inspeksi = NULL, qty_alocated = qty_alocated + @alokasi  where no_sp = @IDSP and kd_stok = @kdstok and no_seq = @noseq;";
                    
            }

            param = new DynamicParameters();
            param.Add("@IDSP", data.No_sp);
            param.Add("@alokasi", data.alokasi);
            param.Add("@kdstok", data.Kd_Stok);
            param.Add("@noseq", data.No_seq);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveLogDel(SALES_SO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_SO_LOG]  (Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, " +
                " Kd_Customer, Atas_Nama, Almt_pnrm, Kd_sales, Keterangan, Flag_Ppn, PPn, Total_qty, Departement, " +
                " Status, Last_Create_Date, Last_Created_By, Program_Name, Jenis_sp, JML_RP_TRANS, JML_VALAS_TRANS, " +
                " Tgl_Kirim_Marketing,  SP_REFF, Discount, Potongan, SP_REFF2, pending, STATUS_DO, Jatuh_Tempo, " +
                " Status_Simpan, dp,Biaya, inc_ongkir) " +
                    "VALUES(@Kd_Cabang,@No_sp,@Tipe_trans,@Tgl_sp,@Kd_Customer,@Atas_Nama,@Almt_pnrm,@Kd_sales,@Keterangan," +
                    "@Flag_Ppn,@PPn,@Total_qty,@Departement,@Status,@Last_Create_Date,@Last_Created_By,@Program_Name,@Jenis_sp,@JML_RP_TRANS,@JML_VALAS_TRANS," +
                    "@Tgl_Kirim_Marketing,@SP_REFF,@Discount,@Potongan,@SP_REFF2,@pending,@STATUS_DO,@Jatuh_Tempo,@Status_Simpan,@dp,@Biaya, @inc_ongkir);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_sp", data.No_sp);
            param.Add("@Tipe_trans", data.Tipe_trans);
            param.Add("@Tgl_sp", data.Tgl_sp);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Atas_Nama", data.Atas_Nama);
            param.Add("@Almt_pnrm", data.Almt_pnrm);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@PPn", data.PPn);
            param.Add("@Flag_Ppn", data.Flag_Ppn);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Departement", data.Departement);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Jenis_sp", data.Jenis_sp);
            param.Add("@JML_RP_TRANS", data.JML_RP_TRANS);
            param.Add("@JML_VALAS_TRANS", data.JML_VALAS_TRANS);
            param.Add("@Tgl_Kirim_Marketing", data.Tgl_Kirim_Marketing);
            param.Add("@SP_REFF", data.SP_REFF);
            param.Add("@Discount", data.Discount);
            param.Add("@Potongan", data.Potongan);
            param.Add("@SP_REFF2", data.SP_REFF2);
            param.Add("@pending", "");
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@Jatuh_Tempo", data.Jatuh_Tempo);
            param.Add("@Status_Simpan", data.Status_Simpan);
            param.Add("@Biaya", data.Biaya);
            param.Add("@dp", data.dp);
            param.Add("@inc_ongkir", data.inc_ongkir);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateRetur(SALES_SO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_SO]  SET STATUS_DO=@STATUS_DO, JML_RP_TRANS=@JML_RP_TRANS, " +
                " JML_VALAS_TRANS=@JML_VALAS_TRANS, PPN=@PPN, Total_qty=@Total_qty, Biaya=@Biaya WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@No_sp", data.No_sp);
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@JML_RP_TRANS", data.JML_RP_TRANS);
            param.Add("@JML_VALAS_TRANS", data.JML_VALAS_TRANS);
            param.Add("@PPN", data.PPn);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Discount", data.Discount);
            param.Add("@Biaya", data.Biaya);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateLimit(string kdcust,decimal total, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE SIF.dbo.SIF_Customer WITH(ROWLOCK) SET saldo_limit=saldo_limit - @total WHERE Kd_Customer=@kdcust";
            param = new DynamicParameters();
            param.Add("@total", total);
            param.Add("@kdcust", kdcust);
            


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static async Task<int> UpdateStatRetur(string no_sp,string Kd_Stok ,decimal qty_tarik,string user, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_SO_D] SET qty_retur=isnull(qty_retur,0)+@qty_retur, Last_updated_by=@user,Last_update_date=GETDATE() " +
                "WHERE No_sp=@No_sp and Kd_Stok=@Kd_Stok";
            param = new DynamicParameters();
            param.Add("@No_sp", no_sp);
            param.Add("@qty_retur", qty_tarik);
            param.Add("@user", user);
            param.Add("@Kd_Stok", Kd_Stok);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateDO(SALES_SO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_SO] SET Kd_Cabang=@Kd_Cabang, Tipe_trans=@Tipe_trans, " +
                " Tgl_sp=@Tgl_sp, Kd_Customer=@Kd_Customer, Atas_Nama=@Atas_Nama, Almt_pnrm=@Almt_pnrm, " +
                " Kd_sales=@Kd_sales, Keterangan=@Keterangan, Flag_Ppn=@Flag_Ppn, PPn=@PPn, Total_qty=@Total_qty, " +
                " Departement=@Departement, Status=@Status, Last_Create_Date=@Last_Create_Date, Last_Created_By=@Last_Created_By, Last_Update_Date=@Last_Update_Date, " +
                " Last_Updated_By=@Last_Updated_By, Program_Name=@Program_Name, Jenis_sp=@Jenis_sp, JML_RP_TRANS=@JML_RP_TRANS, " +
                "  JML_VALAS_TRANS=@JML_VALAS_TRANS, Biaya=@Biaya, Tgl_Kirim_Marketing=@Tgl_Kirim_Marketing, " +
                " SP_REFF=@SP_REFF,  Discount=@Discount, Potongan=@Potongan, SP_REFF2=@SP_REFF2, STATUS_DO=@STATUS_DO, " +
                " Jatuh_Tempo=@Jatuh_Tempo, Status_Simpan=@Status_Simpan,  dp=@dp ,  inc_ongkir=@inc_ongkir" +
                " WHERE No_sp =@No_sp;";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_sp", data.No_sp);
            param.Add("@Tipe_trans", data.Tipe_trans);
            param.Add("@Tgl_sp", data.Tgl_sp);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Atas_Nama", data.Atas_Nama);
            param.Add("@Almt_pnrm", data.Almt_pnrm);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@PPn", data.PPn);
            param.Add("@Flag_Ppn", data.Flag_Ppn);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Departement", data.Departement);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Jenis_sp", data.Jenis_sp);
            param.Add("@JML_RP_TRANS", data.JML_RP_TRANS);
            param.Add("@JML_VALAS_TRANS", data.JML_VALAS_TRANS);
            param.Add("@Tgl_Kirim_Marketing", data.Tgl_Kirim_Marketing);
            param.Add("@SP_REFF", data.SP_REFF);
            param.Add("@Discount", data.Discount);
            param.Add("@Potongan", data.Potongan);
            param.Add("@SP_REFF2", data.SP_REFF2);
            param.Add("@pending", "");
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@Jatuh_Tempo", data.Jatuh_Tempo);
            param.Add("@Status_Simpan", data.Status_Simpan);
            param.Add("@Biaya", data.Biaya);
            param.Add("@dp", data.dp);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@inc_ongkir", data.inc_ongkir);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateDetailRetur(SALES_SO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_SO_D]  SET STATUS_DO=@STATUS_DO, Qty=@Qty " +
                " WHERE No_sp=@No_sp AND Kd_Stok=@Kd_Stok;";
            param = new DynamicParameters();
            param.Add("@No_sp", data.No_sp);
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Qty", data.Qty);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> Delete(string No_sp,string Last_Updated_By, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            //Query = "DELETE [SALES].[dbo].[SALES_SO] WITH(ROWLOCK) " +
            //    " WHERE No_sp=@No_sp;";
            Query = "UPDATE [SALES].[dbo].[SALES_SO] SET STATUS_DO='BATAL',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
                " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@No_sp", No_sp);
            param.Add("@Last_Updated_By", Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> DeleteDetail(SALES_SO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [SALES].[dbo].[SALES_SO_D] WITH(ROWLOCK) " +
                " WHERE No_sp=@No_sp;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO_D] set STATUS_DO='BATAL'  " +
            //  " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@No_sp", data.No_sp);

            
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateStatSOD(string No_sp,string Last_Updated_By, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            //Query = "DELETE [SALES].[dbo].[SALES_SO_D] WITH(ROWLOCK) " +
            //    " WHERE No_sp=@No_sp;";
            Query = "UPDATE [SALES].[dbo].[SALES_SO_D] set STATUS_DO='BATAL'  " +
              " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@No_sp", No_sp);
            param.Add("@Last_Updated_By", Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveDetail(SALES_SO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_SO_D] WITH(ROWLOCK) " +
                " (Kd_Cabang, tipe_trans, No_sp, No_seq, Kd_Stok, Qty, harga, Kd_satuan, Keterangan, Last_create_date," +
                " Last_created_by, Programe_name, Deskripsi, Status_Simpan, STATUS_DO, Bonus, vol, potongan_total, potongan,qty_order,qty_alokasi,qty_alocated,disc1,disc2,disc3,disc4) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@No_sp,@No_seq,@Kd_Stok,@Qty,@harga,@Kd_satuan,@Keterangan,@Last_create_date,@Last_created_by,@Programe_name,@Deskripsi,@Status_Simpan,@STATUS_DO, @Bonus, @vol, @potongan_total, @potongan, @Qty,0,0,@disc1,@disc2,@disc3,@disc4);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_sp", data.No_sp);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@No_seq", data.No_seq);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Qty", data.Qty);
            //param.Add("@Qty", data.qty_order);
            param.Add("@harga", data.harga);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Last_create_date", data.Last_create_date);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Programe_name", data.Programe_name);
            param.Add("@Deskripsi", data.Deskripsi);
            param.Add("@Status_Simpan", data.Status_Simpan);
            param.Add("@STATUS_DO", data.STATUS_DO);
            param.Add("@Bonus", data.Bonus);
            param.Add("@vol", data.vol);
            param.Add("@potongan_total", data.potongan_total);
            param.Add("@potongan", data.potongan);
            param.Add("@disc1", data.disc1);
            param.Add("@disc2", data.disc2);
            param.Add("@disc3", data.disc3);
            param.Add("@disc4", data.disc4);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveBOStokBoked(SALES_SO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[sales_booked] " +
                " (qty, Kd_stok,id,Kd_Cabang,kd_sales,kd_customer,kd_satuan,nama_barang,qty_alokasi,idDisplay,tgl_inden,harga,total ) " +
                    " VALUES(0,@Kd_stok,NEWID(),@Kdcabang,'','',@Kdsatuan,@nmbarang,@alokasi,NEWID(),GETDATE(),@harga,0);";
            param = new DynamicParameters();
            param.Add("@Kd_stok", data.Kd_Stok);
            param.Add("@Kdcabang", data.Kd_Cabang);
            param.Add("@Kdsatuan", data.Kd_satuan);
            param.Add("@nmbarang", data.nama_Barang);
            param.Add("@alokasi", data.alokasi);
            param.Add("@harga", data.harga);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateBOStokBoked(SALES_SO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();
           

            Query = "update [SALES].[dbo].[sales_booked] " +
                " set qty= qty + @qty " +
                    " where kd_stok = @Kd_stok;";
            param = new DynamicParameters();
            param.Add("@Kd_stok", data.Kd_Stok);
            param.Add("@qty", data.alokasi);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetailBooked(SALES_BOOKED data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_BOOKED]  " +
                " SET idDisplay=@idDisplay, No_sp=@No_sp, Kd_Cabang=@Kd_Cabang, tgl_inden=@tgl_inden, Kd_sales=@Kd_sales, Kd_Customer=@Kd_Customer, Kd_Stok=@Kd_Stok, Nama_Barang=@Nama_Barang, Qty=@Qty, Harga=@Harga, " +
                " total=@total, Kd_satuan=@Kd_satuan, Keterangan=@Keterangan, Status=@Status, Last_Create_Date=@Last_Create_Date, Last_Created_By=@Last_Created_By, Last_Update_Date=@Last_Update_Date, Last_Updated_By=@Last_Updated_By WHERE id=@id; ";
            param = new DynamicParameters();
            param.Add("@idDisplay", data.idDisplay);
            param.Add("@No_sp", data.no_sp);
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tgl_inden", data.tgl_inden);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@Qty", data.Qty);
            param.Add("@Harga", data.harga);
            param.Add("@total", data.total);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@id", data.id);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<SALES_SO>> GetDO(string no_po = null, string DateFrom = null, string DateTo = null, string status_po = null, string barang = null,string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO,Tgl_Kirim, " +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales,s.jenis_so,isnull(s.no_paket,'') no_paket,isnull(S.inc_ongkir,'N') as inc_ongkir, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya, C.alamat, C.nama,C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@status_po", status_po);


                if (filter == "")
                {
                    filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                }

                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                    else
                    {
                        filter += " AND ";
                        filter += " D.kd_stok = @barang ";
                    }
                    
                }
                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                        filter += " No_sp =@no_po ";
                    }
                    
                }

                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                    }
                    else
                    {
                        //filter += " AND ";
                        filter += " AND Tgl_sp >= @DateFrom ";
                    }
                    
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                    else
                    {
                        //filter += " AND ";
                        filter += "AND Tgl_sp <= @DateTo ";
                    }
                    
                }
                if (status_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                    else
                    {
                        //filter += " AND ";
                        filter += " AND status_do = @status_po ";
                    }
                   
                }

                
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO>> GetDO_mon(string no_po = null, string DateFrom = null, string DateTo = null, string Kd_sales = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO," +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales,isnull(S.inc_ongkir,'N') as inc_ongkir, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya, C.alamat, C.nama,C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@Kd_sales", Kd_sales);

                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " D.kd_stok = @barang ";
                }
                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " No_sp =@no_po ";
                }

                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                    }
                 
                    filter += " AND Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                
                    filter += "AND Tgl_sp <= @DateTo ";
                }
                if (Kd_sales != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
              
                    filter += " AND Kd_sales = @Kd_sales ";
                }

                if (filter == "")
                {
                    filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO>> GetByDO(string no_po = null, string DateFrom = null, string DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT isnull(S.inc_ongkir,'N') as inc_ongkir,S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO," +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya,C.nama, C.alamat, C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales ";
                    

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                //param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                //param.Add("@status_po", status_po);

                if (no_po != string.Empty && no_po != null)
                {

                    filter += " WHERE S.Kd_Cabang=@kd_cabang and No_sp LIKE CONCAT('%',@no_po,'%') and S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    //filter += " WHERE  No_sp = @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE Tgl_sp >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                       // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  Tgl_sp <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND Tgl_sp <= @DateTo ";
                    }

                }
                
                
                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO>> GetByCust(string kd_cust = null, string DateFrom = null, string DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT isnull(S.inc_ongkir,'N') as inc_ongkir,S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO," +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya,C.nama, C.alamat, C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cust", kd_cust);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                //param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                //param.Add("@status_po", status_po);

                if (kd_cust != string.Empty && kd_cust != null)
                {

                    filter += " WHERE Kd_Customer LIKE CONCAT('%',@kd_cust,'%')  ";
                    //filter += " WHERE  No_sp = @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    //if (DateFrom != null)
                    //{
                    //    if (filter == "")
                    //    {
                    //        filter += " WHERE Tgl_sp >= @DateFrom ";
                    //    }
                    //    else
                    //    {
                    //        filter += " AND Tgl_sp >= @DateFrom ";
                    //    }
                        
                    //}

                    //if (DateTo != null)
                    //{
                    //    if (filter == "")
                    //    {
                    //        filter += " WHERE Tgl_sp <=  @DateTo ";
                    //    }
                    //    else
                    //    {
                    //        filter += "AND Tgl_sp <= @DateTo ";
                    //    }
                        
                    //}

               }


                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=60 and S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO>> GetByStok(string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT isnull(S.inc_ongkir,'N') as inc_ongkir,S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO," +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya,C.nama, C.alamat, C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                //param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                //param.Add("@status_po", status_po);

                if (kd_stok != string.Empty && kd_stok != null)
                {

                    filter += " WHERE S.Kd_Cabang=@kd_cabang and No_sp LIKE CONCAT('%',@kd_stok,'%') and S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    //filter += " WHERE  No_sp = @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE Tgl_sp >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  Tgl_sp <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND Tgl_sp <= @DateTo ";
                    }

                }


                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO>> GetNotaSM(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT isnull(s.inc_ongkir,'N') as inc_ongkir,S.Kd_Cabang, S.No_sp, S.Tipe_trans, S.Tgl_sp, S.Kd_Customer, CASE WHEN j.nama_agent='88' then S.Atas_Nama else j.nama_agent end as Atas_Nama, S.Nama_pnrm,CASE WHEN j.Almt_agen='88' THEN S.Almt_pnrm ELSE j.Almt_agen END as Almt_pnrm, S.Tgl_Kirim, S.Kd_sales, S.Keterangan, S.Flag_Ppn, S.PPn, S.Total_qty, Departement,   S.Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By,  S.Program_Name,  S.Jenis_sp, S.JML_RP_TRANS, S.Valas, S.JML_VALAS_TRANS, S.Kurs, S.Alasan, S.Biaya,  S.Tgl_Kirim_Marketing,  S.Kode_Wilayah, S.isClosed, S.media, S.isPrinted, S.CetakKe, S.desc_promo,  S.No_Telp, S.JamKirim, S.sts_centi,  S.tgl_lahir_umum, S.Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK,  TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO,  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales,  JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya, C.alamat, C.fax1 as telp, C.fax2 as wa   " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " inner join sales.dbo.SALES_SJ j WITH(NOLOCK) on s.No_sp=j.No_sp " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@status_po", status_po);

                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " D.kd_stok = @barang AND S.STATUS_DO<>'BATAL' ";
                }
                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " j.no_sj =@no_po ";
                }

                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                    }
                    //else
                    //{
                    //    filter += " AND ";
                    //}
                    filter += " AND Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                    }
                    //else
                    //{
                    //    filter += " AND ";
                    //}
                    filter += "AND Tgl_sp <= @DateTo ";
                }
                if (status_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                    }
                    //else
                    //{
                    //    filter += " AND ";
                    //}
                    filter += " AND status_do = @status_po ";
                }

                if (filter == "")
                {
                    filter += " WHERE S.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, Tgl_sp, getdate()) <=120 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }


        public static async Task<IEnumerable<PiutangSOVM>> GetPiutangCustomer(string kd_cust = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "select * from sales.dbo.getInv_Masalah(@kd_cust)  ";
                string sql = "select c.Kd_Customer,c.Nama_Customer,c.Limit_Piutang_Rupiah as total,c.jatuh_tempo as hari_jatuh_tempo,saldo_limit from SIF.dbo.SIF_Customer c WITH(NOLOCK) where c.Kd_Customer=@kd_cust ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cust", kd_cust);

                var res = con.Query<PiutangSOVM>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SOAmountVM>> GetAmountSO()
        {
            using (var con = DataAccess.GetConnection())
            {
               // string filter = "";
                string sql = "SELECT * FROM(" +
                        " SELECT SUM(JML_VALAS_TRANS) as amount, c.nama as cabang, C.kd_cabang   " +
                        " FROM SALES.dbo.SALES_SO S " +
                        " INNER JOIN SIF.DBO.SIF_CABANG C ON S.kd_cabang = C.kd_cabang " +
                        " WHERE MONTH(S.Last_Create_Date)= MONTH(GETDATE()) AND YEAR(S.Last_Create_Date)= YEAR(GETDATE()) " +
                        " GROUP BY c.nama, C.kd_cabang  " +
                        " UNION ALL " +
                        " SELECT SUM(JML_VALAS_TRANS) as amount, 'TOTAL', '999' " +
                        " FROM SALES.dbo.SALES_SO S " +
                        " WHERE MONTH(S.Last_Create_Date)= MONTH(GETDATE()) AND YEAR(S.Last_Create_Date)= YEAR(GETDATE()) " +
                        " ) X ORDER BY X.kd_cabang ";

                DynamicParameters param = new DynamicParameters();
              
                var res = con.Query<SOAmountVM>(sql, param, null, true, 36000);

                return res;
            }
        }
        public async static Task<List<SALES_SO>> GetDOPartial(DateTime DateFrom, DateTime DateTo, string kdcb, string filterquery = "", string sortingquery = "",  int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "SELECT DISTINCT PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                //    " PO.tgl_kirim,tgl_jth_tempo,  qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,PO.prosen_diskon, " +
                //    " PO.jml_diskon,PO.keterangan,PO.rec_stat,lama_bayar,  tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve, " +
                //    " tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,   " +
                //    " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,PO.total,ongkir, " +
                //    " isClosed, S.Nama_Supplier,PO.atas_nama,PO.kop_surat,  STUFF( " +
                //     " (SELECT ',' + Nama_Barang " +
                //     "  FROM PURC.DBO.PURC_PO_D t1 " +
                //     "  INNER JOIN SIF.dbo.SIF_Barang B ON B.Kode_Barang = T1.kd_stok " +
                //     "  WHERE t1.no_po = D.no_po " +
                //     "  FOR XML PATH('')) " +
                //     " , 1, 1, '') stuffbarang  " +
                //    " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                //    " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier " +
                //    " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po ";
                // string sql = "SELECT DISTINCT TOP " + seq + " PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                string sql = "SELECT DISTINCT TOP " + seq + " S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,  " +
                    " Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn, Total_qty, Departement,  " +
                    " Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, " +
                    " S.Program_Name,  Jenis_sp, JML_RP_TRANS, Valas, JML_VALAS_TRANS, Kurs, Alasan, Biaya, " +
                    " Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo, " +
                    " No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK, " +
                    " TGL_SERAH_FORM, S.KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO," +
                    "  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales,isnull(S.inc_ongkir,'N') as inc_ongkir, " +
                    " JML_RP_TRANS-PPn AS subtotal, Nama_Sales AS sales, dp,Biaya, C.alamat, C.nama,C.fax1 as telp, C.fax2 as wa " +
                    " FROM [SALES].[dbo].[SALES_SO] S WITH(NOLOCK) " +
                    " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_Cabang=C.kd_cabang " +
                    " INNER JOIN  SIF.dbo.SIF_Sales SF WITH(NOLOCK) ON SF.Kode_Sales=S.Kd_sales  " +
                    " WHERE S.Kd_Cabang=@Kd_Cabang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Kd_Cabang", kdcb);


                if (DateFrom != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    //    filter += " AND ";
                    //}
                    filter += " AND S.Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    //    filter += " AND ";
                    //}
                    filter += " AND S.Tgl_sp <= @DateTo ";
                }

                if (filterquery != null && filterquery != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filterquery = filterquery.Replace("status_po", "S.STATUS_DO");
                    filterquery = filterquery.Replace("no_po", "S.No_sp");
                    filterquery = filterquery.Replace("tgl_po", "S.Tgl_sp");
                   // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
                    filter += " " + filterquery + " ";
                }
                //if (barang != null && barang != "")
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    filter += " S.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                //}


                sql += filter;

                if (sortingquery != null && sortingquery != "")
                {
                    filterquery = filterquery.Replace("status_po", "S.STATUS_DO");
                    filterquery = filterquery.Replace("no_po", "S.No_sp");
                    filterquery = filterquery.Replace("tgl_po", "S.Tgl_sp");
                    // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
                    filter += " " + filterquery + " ";

                }
                else
                {
                    sql += " ORDER BY S.Last_Create_Date DESC ";
                }


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }
        public static async Task<IEnumerable<SALES_SO_DVM>> GetDODetail(string no_po = null, string DateFrom = null, string DateTo = null, string status_po = null, string barang = null,string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT Kd_Stok AS kode_Barang,no_sp, Deskripsi AS nama_Barang, Kd_satuan AS satuan, qty, qty as qty_awal, harga, qty * harga as total, Keterangan, 0 as stok, CASE WHEN Bonus IS NULL THEN 0 ELSE Bonus END as flagbonus, vol, ISNULL(potongan, 0) as diskon,ISNULL(disc1, 0) as disc1,ISNULL(disc4, 0) as disc4,ISNULL(disc2, 0) as disc2,ISNULL(disc3, 0) as disc3    FROM [SALES].[dbo].[SALES_SO_D] WITH(NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", cb);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE  ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " No_sp =@no_po ";
                }
                if (filter == "")
                {
                    filter += " WHERE Kd_Cabang=@kd_cabang AND STATUS_DO<>'BATAL' ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY No_Seq ASC ";


                var res = con.Query<SALES_SO_DVM>(sql, param, null, true, 36000);

                return res;
            }
        }

      
        public static List<Response> getCountAlokasi(DateTime? DateFrom=null, DateTime? DateTo=null, string filterquery = "", string sortingquery = "", string kd_cust = "", int seq = 0, string no_po = "")
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result, d.no_sp,d.no_seq " +
                 " from Sales.dbo.SALES_SO_D d  WITH(NOLOCK) inner join Sales.dbo.SALES_SO s  WITH(NOLOCK) on d.No_sp = s.No_sp " +
" inner join INV.dbo.INV_STOK_SALDO sld  WITH(NOLOCK) on d.Kd_Stok = sld.kd_stok and d.Kd_Cabang = sld.Kd_Cabang inner join sif.dbo.SIF_Barang br on br.kode_barang=sld.kd_stok inner join sif.dbo.sif_sales ss on ss.kode_sales =  s.kd_sales inner join sif.dbo.sif_customer cc on cc.kd_customer = s.kd_customer" +
" and sld.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6)) " +
" where isnull(d.isClosed,'') <> 'Y' and d.qty - isnull(d.qty_alocated,0) > 0 " +
" and  d.No_sp in (select a.no_sp from sales.dbo.sales_so a  WITH(NOLOCK) where jenis_so in ('BOOKING ORDER', 'BO PAKET') and " +
" isnull(isClosed, '') <> 'Y' and isnull(s.pending,'')<> 'Y' and isnull(s.status_simpan,'')= 'Y' and isnull(s.isClosed,'') <> 'Y'  and isnull(s.[Status],'OK') <> 'CANCEL') ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_customer", kd_cust);


                if (no_po != string.Empty && no_po != null)
                {

                    filter += " AND ";

                    filter += " d.No_sp = @no_po ";
                }

                if (kd_cust != string.Empty && kd_cust != null)
                {

                    filter += " AND ";

                    filter += " s.kd_customer = @kd_customer ";
                }

                if (DateFrom != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    filter += " AND ";
                    //}
                    filter += " s.tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    filter += " AND ";
                    //}
                    filter += " s.tgl_sp <= @DateTo ";
                }


                sql += filter;
                sql += " GROUP BY d.no_sp,d.no_seq ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static async Task<IEnumerable<SALES_SO_D>> GetBOPAKET(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string kd_cust = null, string cb = null)
        {

            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select isnull(d.tunda,'T') tunda,s.jenis_so,br.nama_barang,ss.nama_Sales,cc.nama_Customer,s.tgl_sp tgl_inden,0 alokasi,sld.qty_available,d.Kd_Cabang,d.tipe_trans,d.id_booked,d.No_sp,d.No_seq,d.Kd_Stok,d.Qty,d.QtyCetak,d.Qty_book,d.PriceList,d.harga,d.vol,d.tgl_kirim,d.prioritas,d.Kd_satuan,d.Jns_service,d.qty_prod,d.qty_kirim,d.Qty_sisa,d.Keterangan,d.disc1,d.disc2,d.disc3,d.disc4,d.disc5,d.potongan,d.potongan_total,d.Status,d.departemen,d.Last_create_date,d.Last_created_by,d.Last_update_date,d.Last_updated_by,d.Programe_name,d.kd_parent,d.No,d.set_paket,d.ambil_bonus,d.Deskripsi,d.key_bonus,d.nama_potongan,d.thnbuat,d.Tgl_Kirim_Marketing,d.Nomor_Bonus,d.Bom_Service,d.qty_batal,d.Status_Inspeksi,d.reff,d.CONFIRMED,d.BIAYA_SERVICE,d.KOMPLAIN,d.NO_REFF,d.sudah_qc,d.Bonus,d.pending,d.No_Paket,d.Status_Simpan,d.sudahPO,d.STATUS_DO,d.isClosed,d.QtyLastCetak,d.qty_order,d.qty_retur,d.qty_sisa_krm,d.alokasi,isnull(d.qty_alocated,0) as total_qty ,isnull(sld.QTY_AVAILABLE,0) AS stok" +
" from Sales.dbo.SALES_SO_D d  WITH(NOLOCK) inner join Sales.dbo.SALES_SO s  WITH(NOLOCK) on d.No_sp = s.No_sp " +
" inner join INV.dbo.INV_STOK_SALDO sld  WITH(NOLOCK) on d.Kd_Stok = sld.kd_stok and d.Kd_Cabang = sld.Kd_Cabang inner join sif.dbo.SIF_Barang br on br.kode_barang=sld.kd_stok inner join sif.dbo.sif_sales ss on ss.kode_sales =  s.kd_sales inner join sif.dbo.sif_customer cc on cc.kd_customer = s.kd_customer" +
" and sld.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6)) " +
" where isnull(d.isClosed,'') <> 'Y' and d.qty - isnull(d.qty_alocated,0) > 0 " +
" and  d.No_sp in (select a.no_sp from sales.dbo.sales_so a  WITH(NOLOCK) where jenis_so in ('BOOKING ORDER', 'BO PAKET') and " +
" isnull(isClosed, '') <> 'Y' and isnull(s.pending,'')<> 'Y' and isnull(s.status_simpan,'')= 'Y' and isnull(s.isClosed,'') <> 'Y'  and isnull(s.[Status],'OK') <> 'CANCEL') ";
                //" order by d.No_sp Desc  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@kd_customer", kd_cust);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

                if (no_po != string.Empty && no_po != null)
                {

                    filter += " AND ";

                    filter += " d.No_sp = @no_po ";
                }

                if (kd_cust != string.Empty && kd_cust != null)
                {

                    filter += " AND ";

                    filter += " s.kd_customer = @kd_customer ";
                }

                if (DateFrom != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    filter += " AND ";
                    //}
                    filter += " s.tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                    filter += " AND ";
                    //}
                    filter += " s.tgl_sp <= @DateTo ";
                }


                //if (filter == "")
                //{
                //    filter += " WHERE Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                //}
                ////else
                ////{
                ////    filter += " AND ";
                ////}
                //filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

               
                    sql += " order by d.No_sp Desc  ";
               




                var res = con.Query<SALES_SO_D>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<List<SALES_SO_D>> GetBOPAKETPartial(DateTime? DateFrom=null, DateTime? DateTo=null, string filterquery = "", string sortingquery = "", string kd_cust = "", int seq = 0,string no_po = "")
        {

            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select d.tunda,s.jenis_so,br.nama_barang,ss.nama_Sales,cc.nama_Customer,s.tgl_sp tgl_inden,0 alokasi,sld.qty_available,d.Kd_Cabang,d.tipe_trans,d.id_booked,d.No_sp,d.No_seq,d.Kd_Stok,d.Qty,d.QtyCetak,d.Qty_book,d.PriceList,d.harga,d.vol,d.tgl_kirim,d.prioritas,d.Kd_satuan,d.Jns_service,d.qty_prod,d.qty_kirim,d.Qty_sisa,d.Keterangan,d.disc1,d.disc2,d.disc3,d.disc4,d.disc5,d.potongan,d.potongan_total,d.Status,d.departemen,d.Last_create_date,d.Last_created_by,d.Last_update_date,d.Last_updated_by,d.Programe_name,d.kd_parent,d.No,d.set_paket,d.ambil_bonus,d.Deskripsi,d.key_bonus,d.nama_potongan,d.thnbuat,d.Tgl_Kirim_Marketing,d.Nomor_Bonus,d.Bom_Service,d.qty_batal,d.Status_Inspeksi,d.reff,d.CONFIRMED,d.BIAYA_SERVICE,d.KOMPLAIN,d.NO_REFF,d.sudah_qc,d.Bonus,d.pending,d.No_Paket,d.Status_Simpan,d.sudahPO,d.STATUS_DO,d.isClosed,d.QtyLastCetak,d.qty_order,d.qty_retur,d.qty_sisa_krm,d.alokasi,isnull(d.qty_alocated,0) as total_qty ,isnull(sld.QTY_AVAILABLE,0) AS stok" +
" from Sales.dbo.SALES_SO_D d  WITH(NOLOCK) inner join Sales.dbo.SALES_SO s  WITH(NOLOCK) on d.No_sp = s.No_sp " +
" inner join INV.dbo.INV_STOK_SALDO sld  WITH(NOLOCK) on d.Kd_Stok = sld.kd_stok and d.Kd_Cabang = sld.Kd_Cabang inner join sif.dbo.SIF_Barang br on br.kode_barang=sld.kd_stok inner join sif.dbo.sif_sales ss on ss.kode_sales =  s.kd_sales inner join sif.dbo.sif_customer cc on cc.kd_customer = s.kd_customer" +
" and sld.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6)) " +
" where isnull(d.isClosed,'') <> 'Y' and d.qty - isnull(d.qty_alocated,0) > 0 " +
" and  d.No_sp in (select a.no_sp from sales.dbo.sales_so a  WITH(NOLOCK) where jenis_so in ('BOOKING ORDER', 'BO PAKET') and " +
" isnull(isClosed, '') <> 'Y' and isnull(s.pending,'')<> 'Y' and isnull(s.status_simpan,'')= 'Y' and isnull(s.isClosed,'') <> 'Y'  and isnull(s.[Status],'OK') <> 'CANCEL') ";
//" order by d.No_sp Desc  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@kd_customer", kd_cust);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

                if (no_po != string.Empty && no_po != null)
                {
                    
                    filter += " AND ";
                    
                    filter += " d.No_sp = @no_po ";
                }

                if (kd_cust != string.Empty && kd_cust != null)
                {
                    
                    filter += " AND ";
                   
                    filter += " s.kd_customer = @kd_customer ";
                }

                if (DateFrom != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                        filter += " AND ";
                    //}
                    filter += " s.tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    //if (filter == "")
                    //{
                    //    filter += " WHERE ";
                    //}
                    //else
                    //{
                        filter += " AND ";
                    //}
                    filter += " s.tgl_sp <= @DateTo ";
                }


                //if (filter == "")
                //{
                //    filter += " WHERE Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                //}
                ////else
                ////{
                ////    filter += " AND ";
                ////}
                //filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                if (sortingquery != null && sortingquery != "")
                {
                    //sortingquery = sortingquery.Replace("status_po", "PO.status_po");
                    //sortingquery = sortingquery.Replace("no_po", "PO.no_po");
                    //sortingquery = sortingquery.Replace("tgl_po", "PO.tgl_po");
                    //sortingquery = sortingquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    //sortingquery = sortingquery.Replace("jml_rp_trans", "PO.jml_rp_trans");
                    //sortingquery = sortingquery.Replace("total", "PO.total");
                    //sql += " " + sortingquery + " ";
                    sql += " order by d.No_sp Desc  ";

                }
                else
                {
                    sql += " order by d.No_sp Desc  ";
                }

               


                var res = con.Query<SALES_SO_D>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static async Task<IEnumerable<SALES_SO_D>> GetSO_D(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "SELECT Kd_Stok AS kode_Barang, Deskripsi AS nama_Barang, Kd_satuan AS satuan, qty, harga, qty * harga as total, Keterangan, 0 as stok, CASE WHEN Bonus IS NULL THEN 0 ELSE Bonus END as flagbonus, vol, ISNULL(potongan, 0) as diskon    FROM [SALES].[dbo].[SALES_SO_D] WITH(NOLOCK) ";
                string sql = "SELECT * FROM [SALES].[dbo].[SALES_SO_D] WITH(NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", cb);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE  ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " No_sp =@no_po ";
                }
                if (filter == "")
                {
                    filter += " WHERE Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL' ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY No_Seq ASC ";

                var res = con.Query<SALES_SO_D>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO_DVM>> GetByDODetail(string no_po = null,string DateFrom = null, string DateTo = null, string status_po = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT No_sp, Kd_Stok AS kode_Barang, Deskripsi AS nama_Barang, Kd_satuan AS satuan, qty, harga, qty * harga as total, Keterangan, 0 as stok, CASE WHEN Bonus IS NULL THEN 0 ELSE Bonus END as flagbonus, vol, ISNULL(potongan, 0) as diskon    FROM [SALES].[dbo].[SALES_SO_D] WITH(NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                //param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                //param.Add("@status_po", status_po);

                if (no_po != string.Empty && no_po != null)
                {

                    filter += " WHERE Kd_Cabang=@kd_cabang and No_sp LIKE CONCAT('%',@no_po,'%') AND STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE cast(Last_create_date as Date) >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  cast(Last_create_date as Date) <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND cast(Last_create_date as Date) <= @DateTo ";
                    }

                }


               
                filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY Last_create_date,No_Seq ASC ";


                var res = con.Query<SALES_SO_DVM>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO_DVM>> GetByCustDetail(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT No_sp, Kd_Stok AS kode_Barang, Deskripsi AS nama_Barang, Kd_satuan AS satuan, qty, harga, qty * harga as total, Keterangan, 0 as stok, CASE WHEN Bonus IS NULL THEN 0 ELSE Bonus END as flagbonus, vol, ISNULL(potongan, 0) as diskon    FROM [SALES].[dbo].[SALES_SO_D] WITH(NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                //param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                //param.Add("@status_po", status_po);

                if (no_po != string.Empty && no_po != null)
                {

                    filter += " WHERE Kd_Cabang=@kd_cabang and No_sp LIKE CONCAT('%',@no_po,'%') AND STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE cast(Last_create_date as Date) >= @DateFrom ";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        // filter += " AND Tgl_sp >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE  cast(Last_create_date as Date) <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND cast(Last_create_date as Date) <= @DateTo ";
                    }

                }



                filter += " AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY Last_create_date,No_Seq ASC ";


                var res = con.Query<SALES_SO_DVM>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<SALES_SO_DVM>> GetNotaSMDetail(string no_po = null,  DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT isnull (j.Qty_kirim,0) as qty,j.qty_out,s.Kd_Stok AS kode_Barang, case when ISNULL(j.nama_barang,'')='' then s.Deskripsi else j.nama_barang end AS nama_Barang, s.Kd_satuan AS satuan, s.harga, (isnull (j.Qty_kirim,0) * s.harga) as total, s.Keterangan, 0 as stok, CASE WHEN s.Bonus IS NULL THEN 0 ELSE s.Bonus END as flagbonus, s.vol, ISNULL(s.potongan, 0) as diskon,ISNULL(s.potongan_total, 0) as potongan_total  " + 
        " FROM[SALES].[dbo].[SALES_SO_D] s WITH (NOLOCK) " +
        " left join SALES.dbo.SALES_SJ_D j WITH (NOLOCK) on j.No_sp = s.No_sp and s.No_seq = j.no_seq_kirim ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", cb);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE  ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " j.no_sj =@no_po ";
                }
                if (filter == "")
                {
                    filter += " WHERE s.Kd_Cabang=@kd_cabang AND S.STATUS_DO<>'BATAL'";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, s.Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY s.No_Seq DESC ";


                var res = con.Query<SALES_SO_DVM>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<SALES_SO>> GetDORetur(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT S.Kd_Cabang, No_sp, Tipe_trans, Tgl_sp, Kd_Customer, Atas_Nama, Nama_pnrm,   Almt_pnrm, Tgl_Kirim, Kd_sales, S.Keterangan, Flag_Ppn, PPn*-1 as PPn, Total_qty*-1 as Total_qty, Departement,   Status, S.Last_Create_Date, S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By,  S.Program_Name,  Jenis_sp, JML_RP_TRANS*-1 as JML_RP_TRANS, Valas, JML_VALAS_TRANS*-1 as JML_VALAS_TRANS, Kurs, Alasan, Biaya,  Tgl_Kirim_Marketing,  S.Kode_Wilayah, isClosed, media, isPrinted, CetakKe, desc_promo,  No_Telp, JamKirim, sts_centi,  tgl_lahir_umum, Confirmed, old_num, SP_REFF, TGL_BARANG_MASUK,  TGL_SERAH_FORM, KOTA, Bonus,  Discount, Potongan, Alamat_Tarik, SP_REFF2, pending, STATUS_DO,  FLAG, Flag_Paket, Qty_Paket,  Kd_Paket, Jatuh_Tempo, Status_Simpan, JnsSales,  (JML_RP_TRANS-PPn)*-1 AS subtotal, Nama_Sales AS sales  FROM [SALES].[dbo].[SALES_SO] S WITH (NOLOCK)  INNER JOIN  SIF.dbo.SIF_Sales SF WITH (NOLOCK) ON SF.Kode_Sales=S.Kd_sales  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@barang", barang);
                if (barang != string.Empty && barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " D.kd_stok = @barang ";
                }
                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " No_sp =@no_po ";
                }

                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Tgl_sp <= @DateTo ";
                }
                if (status_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " status_do = @status_po ";
                }

                if (filter == "")
                {
                    filter += " WHERE ";
                }
                else
                {
                    filter += " AND ";
                }
                filter += " DATEDIFF(day, Tgl_sp, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<SALES_SO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SO_DVM>> GetDODetailRetur(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT Kd_Stok AS kode_Barang, Deskripsi AS nama_Barang, Kd_satuan AS satuan, qty*-1 as qty, harga*-1 as harga, (qty * harga)*-1 as total, Keterangan, 0 as stok, CASE WHEN Bonus IS NULL THEN 0 ELSE Bonus END as flagbonus  FROM [SALES].[dbo].[SALES_SO_D] ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@status_po", status_po);

                if (no_po != string.Empty && no_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " No_sp =@no_po ";
                }
                if (filter == "")
                {
                    filter += " WHERE ";
                }
                else
                {
                    filter += " AND ";
                }
                filter += " DATEDIFF(day, Last_create_date, getdate()) <=60 ";

                sql += filter;

                sql += " ORDER BY No_Seq DESC ";


                var res = con.Query<SALES_SO_DVM>(sql, param, null, true, 36000);

                return res;
            }
        }



        public static async Task<int> SaveInden(SALES_BOOKED data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_BOOKED]  " +
                " (id, Kd_Cabang, tgl_inden, Kd_sales, Kd_Customer, Kd_Stok, Nama_Barang, Qty, Harga, total, " +
                " Kd_satuan, Keterangan, Status, Last_Create_Date, Last_Created_By, idDisplay, dp_inden) " +
                    "VALUES(@id,@Kd_Cabang,@tgl_inden,@Kd_sales,@Kd_Customer,@Kd_Stok,@Nama_Barang,@Qty,@Harga,@total,@Kd_satuan,@Keterangan,@Status,@Last_Create_Date,@Last_Created_By,@idDisplay, @dp_inden);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@id", data.id);
            param.Add("@tgl_inden", data.tgl_inden);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@Qty", data.Qty);
            param.Add("@Harga", data.harga);
            param.Add("@total", data.total);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Status", data.Status);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@idDisplay", data.idDisplay);
            param.Add("@dp_inden", data.dp_inden);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateInden(SALES_BOOKED data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [SALES].[dbo].[SALES_BOOKED] " +
                " SET  idDisplay =@idDisplay, No_sp=@No_sp, Kd_Cabang=@Kd_Cabang, tgl_inden=@tgl_inden, Kd_sales=@Kd_sales, " +
                " Kd_Customer=@Kd_Customer, Kd_Stok=@Kd_Stok, Nama_Barang=@Nama_Barang, Qty=@Qty, Harga=@Harga, total=@total, " +
                " Kd_satuan=@Kd_satuan, Keterangan=@Keterangan, Status=@Status, Last_Update_Date=@Last_Update_Date,  Last_Updated_By =@Last_Updated_By,  dp_inden =@dp_inden, alokasiLevel=@alokasiLevel, qty_Alokasi=@qty_Alokasi " +
                " WHERE id=@id ;";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_sp", data.no_sp);
            param.Add("@id", data.id);
            param.Add("@tgl_inden", data.tgl_inden);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Nama_Barang", data.Nama_Barang);
            param.Add("@Qty", data.Qty);
            param.Add("@Harga", data.harga);
            param.Add("@total", data.total);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Status", data.Status);
            param.Add("@Status", data.Status);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@idDisplay", data.idDisplay);
            param.Add("@dp_inden", data.dp_inden);
            param.Add("@alokasiLevel", data.alokasiLevel);
            param.Add("@qty_Alokasi", data.qty_Alokasi);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> CekStok(string Kd_Cabang, string kd_stok,decimal qty, string bultah, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "SELECT (s.qty_available - (@qty)) as akhir from [INV].[dbo].[INV_STOK_SALDO] s WITH (NOLOCK) " +
                "where s.bultah=@bultah and s.Kd_Cabang=@Kd_Cabang and s.kd_stok=@kd_stok ";
                
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", Kd_Cabang);
            param.Add("@kd_stok", kd_stok);
            param.Add("@qty", qty);
            param.Add("@bultah", bultah);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<SALES_BOOKED>> GetIndenList(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "", Guid? unix_id = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "SELECT  id, idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, Kd_Stok,  " +
                //    " Nama_Barang, Qty, Harga, total, Kd_satuan, S.Keterangan, Status, S.Last_Create_Date,  " +
                //    " S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, C.Nama_Customer " +
                //    " FROM [SALES].[dbo].[SALES_BOOKED] S " +
                //    " INNER JOIN SIF.dbo.SIF_CUSTOMER C ON S.Kd_Customer=c.Kd_Customer ";

                //string sql = "SELECT  id, idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, Kd_Stok,   S.Nama_Barang, Qty, Harga, total, S.Kd_satuan, S.Keterangan, Status, S.Last_Create_Date,   S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, C.Nama_Customer, SS.Nama_Sales, B.berat, B.berat*Qty AS 'TotalBerat',  " +
                //            " (SELECT SUM(BX.BERAT * Qty) FROM[SALES].[dbo].[SALES_BOOKED] X " +
                //            " INNER JOIN SIF.dbo.SIF_BARANG BX ON BX.Kode_Barang = X.Kd_Stok " +
                //            " WHERE idDisplay = S.idDisplay AND Status='ENTRY') AS 'TotalBeratInden' " +
                //            " FROM[SALES].[dbo].[SALES_BOOKED] S " +
                //            " INNER JOIN SIF.dbo.SIF_CUSTOMER C ON S.Kd_Customer = c.Kd_Customer " +
                //            " INNER JOIN SIF.dbo.SIF_SALES SS ON SS.Kode_Sales = S.Kd_sales " +
                //            " INNER JOIN SIF.dbo.SIF_BARANG B ON B.Kode_Barang = S.Kd_Stok";

                string sql = "SELECT  idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, C.Nama_Customer, SS.Nama_Sales, " +
                        " SUM(QTY) AS qty, SUM(QTY * B.BERAT) AS totalBeratInden, SUM(TOTAL) AS total, max(dp_inden) as dp_inden " +
                        " FROM[SALES].[dbo].[SALES_BOOKED] S " +
                        " INNER JOIN SIF.dbo.SIF_CUSTOMER C ON S.Kd_Customer = c.Kd_Customer " +
                        " INNER JOIN SIF.dbo.SIF_SALES SS ON SS.Kode_Sales = S.Kd_sales " +
                        " INNER JOIN SIF.dbo.SIF_BARANG B ON B.Kode_Barang = S.Kd_Stok";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@status", status);
                param.Add("@kd_customer", kd_customer);
                param.Add("@Kd_sales", Kd_sales);
                param.Add("@unix_id", unix_id);

                if (id != Guid.Empty && id != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " idDisplay = @id ";
                }
                if (unix_id != Guid.Empty && unix_id != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " id = @unix_id ";
                }
                if (kd_customer != string.Empty && kd_customer != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " S.kd_customer = @kd_customer ";
                }

                if (status != string.Empty && status != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Status IN (" + status +")";
                }
                if (Kd_sales != string.Empty && Kd_sales != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Kd_sales = @Kd_sales ";
                }
                sql += filter;


                sql += "GROUP BY idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, C.Nama_Customer, SS.Nama_Sales" +
                    " ORDER BY tgl_inden DESC, idDisplay";

                var res = con.Query<SALES_BOOKED>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<List<SALES_BOOKED>> GetInden(Guid? id, string status = "", string kd_customer = "", string Kd_sales = "", Guid? unix_id=null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "SELECT  id, idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, Kd_Stok,  " +
                //    " Nama_Barang, Qty, Harga, total, Kd_satuan, S.Keterangan, Status, S.Last_Create_Date,  " +
                //    " S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, C.Nama_Customer " +
                //    " FROM [SALES].[dbo].[SALES_BOOKED] S " +
                //    " INNER JOIN SIF.dbo.SIF_CUSTOMER C ON S.Kd_Customer=c.Kd_Customer ";

                string sql = "SELECT  id, idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer," +
                    " Kd_Stok,   S.Nama_Barang, Qty, Harga, total, S.Kd_satuan, S.Keterangan, Status, S.Last_Create_Date,  " +
                    " S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, C.Nama_Customer, SS.Nama_Sales," +
                    " B.berat,  cb.alamat, cb.nama,cb.fax1 as telp,cb.fax2 as wa , B.berat*Qty AS 'TotalBerat', S.dp_inden, S.alokasiLevel, qty_Alokasi, " +
                            " (SELECT SUM(BX.BERAT * Qty) FROM [SALES].[dbo].[SALES_BOOKED] X " +
                            " INNER JOIN SIF.dbo.SIF_BARANG BX ON BX.Kode_Barang = X.Kd_Stok " +
                            " WHERE idDisplay = S.idDisplay AND Status='ENTRY') AS 'TotalBeratInden' " +
                            " FROM[SALES].[dbo].[SALES_BOOKED] S WITH (NOLOCK) " +
                            " INNER JOIN SIF.dbo.SIF_CABANG cb WITH(NOLOCK) ON S.Kd_Cabang=cb.kd_cabang " +
                            " INNER JOIN SIF.dbo.SIF_CUSTOMER C WITH (NOLOCK) ON S.Kd_Customer = c.Kd_Customer " +
                            " INNER JOIN SIF.dbo.SIF_SALES SS WITH (NOLOCK) ON SS.Kode_Sales = S.Kd_sales " +
                            " INNER JOIN SIF.dbo.SIF_BARANG B WITH (NOLOCK) ON B.Kode_Barang = S.Kd_Stok";
                            

           DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@status", status);
                param.Add("@kd_customer", kd_customer);
                param.Add("@Kd_sales", Kd_sales);
                param.Add("@unix_id", unix_id);

                if (id != Guid.Empty && id != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " idDisplay = @id ";
                }
                if (unix_id != Guid.Empty && unix_id != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " id = @unix_id ";
                }
                if (kd_customer != string.Empty && kd_customer != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " S.kd_customer = @kd_customer ";
                }

                if (status != string.Empty && status != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Status IN (" + status +")";
                }
                else
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Status = 'ENTRY' ";
                }
                if (Kd_sales != string.Empty && Kd_sales != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " Kd_sales = @Kd_sales ";
                }
                sql += filter;


                sql += "GROUP BY id, idDisplay, No_sp, S.Kd_Cabang, tgl_inden, Kd_sales, S.Kd_Customer, Kd_Stok,  " +
                    " S.Nama_Barang, Qty, Harga, total, S.Kd_satuan, S.Keterangan, Status, S.Last_Create_Date,   S.Last_Created_By, S.Last_Update_Date, S.Last_Updated_By, C.Nama_Customer, SS.Nama_Sales, B.berat ,cb.alamat, cb.nama,cb.fax1 , cb.fax2, S.dp_inden, S.alokasiLevel, qty_Alokasi " +
                    " ORDER BY Last_Create_Date DESC, idDisplay";

                var res = con.Query<SALES_BOOKED>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static async Task<List<SALES_BOOKED>> checkLevel(string Kd_Stok, string alokasiLevel)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                string sql = "SELECT * FROM SALES.dbo.SALES_BOOKED WHERE Kd_Stok=@Kd_Stok AND STATUS='ALOKASI' AND alokasiLevel IN (" + alokasiLevel + ")";

                DynamicParameters param = new DynamicParameters();
                param.Add("@Kd_Stok", Kd_Stok);
                var res = con.Query<SALES_BOOKED>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


        public static async Task<IEnumerable<SALES_BOOKED>> GetDPInden(string id)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql="SELECT SUM(IIF(dp_inden IS NULL, 0, DP_INDEN)) as dp_inden FROM SALES.dbo.SALES_BOOKED WITH(NOLOCK) WHERE idDisplay IN( " +
                    " select idDisplay from SALES.dbo.SALES_BOOKED where id IN(" + id + "))";
                DynamicParameters param = new DynamicParameters();
               // param.Add("@id", id);
                var res = con.Query<SALES_BOOKED>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<int> SPFIN_INS_NOTA_JUAL_LANGSUNG(SALES_SO data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
               // param.Add("@kdcabang", data.Kd_Cabang);
                param.Add("@no_so", data.No_sp);

                return conn.Execute("[FIN].[dbo].[FIN_INS_NOTA_JUAL_LANGSUNG]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPFIN_INS_NOTA_JUAL_LANGSUNG2(string No_sp, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                // param.Add("@kdcabang", data.Kd_Cabang);
                param.Add("@no_so", No_sp);

                return conn.Execute("[FIN].[dbo].[FIN_INS_NOTA_JUAL_LANGSUNG]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPFIN_INS_NOTA_JUAL(SALES_SJ data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                // param.Add("@kdcabang", data.Kd_Cabang);
                param.Add("@no_sj", data.no_sj);

                return conn.Execute("[FIN].[dbo].[FIN_INS_NOTA_JUAL]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SaveRetur(SALES_RETUR data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_RETUR]  (" +
                "Kd_Cabang, No_retur, Tipe_trans, Tgl_retur, No_ref1, No_ref2, Kd_Customer, Nama_agen, " +
                "Nama_Paket, Tgl_tarik, Kd_sales, Keterangan, Total_qty, Status, departemen, Last_Create_Date, " +
                "Last_Created_By, Program_Name, Jenis_Retur, CetakKe, isPrinted, No_ref3, jml_rp_trans, flag_ppn) " +
                    "VALUES(@Kd_Cabang,@No_retur,@Tipe_trans,@Tgl_retur,@No_ref1,@No_ref2,@Kd_Customer,@Nama_agen,@Nama_Paket,@Tgl_tarik,@Kd_sales,@Keterangan,@Total_qty,@Status,@departemen,@Last_Create_Date,@Last_Created_By,@Program_Name,@Jenis_Retur,@CetakKe,@isPrinted,@No_ref3,@jml_rp_trans,@flag_ppn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_retur", data.No_retur);
            param.Add("@Tipe_trans", data.Tipe_trans);
            param.Add("@Tgl_retur", data.Tgl_retur);
            param.Add("@No_ref1", data.No_ref1);
            param.Add("@No_ref2", data.No_ref2);
            param.Add("@Kd_Customer", data.Kd_Customer);
            param.Add("@Nama_agen", data.Nama_agen);
            param.Add("@Nama_Paket", data.Nama_Paket);
            param.Add("@Tgl_tarik", data.Tgl_tarik);
            param.Add("@Kd_sales", data.Kd_sales);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Total_qty", data.Total_qty);
            param.Add("@Status", data.Status);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@departemen", data.departemen);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@Jenis_Retur", data.Jenis_Retur);
            param.Add("@CetakKe", data.CetakKe);
            param.Add("@isPrinted", data.isPrinted);
            param.Add("@No_ref3", data.No_ref3);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@flag_ppn", data.flag_ppn);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveReturDetail(SALES_RETUR_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [SALES].[dbo].[SALES_RETUR_D]  " +
                " (Kd_Cabang, No_retur, tipe_trans, No_seq, Kd_Stok, Nama, Qty, Kd_satuan, qty_tarik, " +
                "harga, Jns_retur, Keterangan, Status, departemen, " +
                "Last_create_date, Last_created_by, Programe_name, potongan, potongan_total, nama_potongan, " +
                "lokasi, Total) " +
                    "VALUES(@Kd_Cabang,@No_retur,@tipe_trans,@No_seq,@Kd_Stok,@Nama,@Qty,@Kd_satuan,@qty_tarik,@harga,@Jns_retur,@Keterangan,@Status,@departemen,@Last_create_date,@Last_created_by,@Programe_name,@potongan,@potongan_total,@nama_potongan,@lokasi,@Total);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@No_retur", data.No_retur);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@No_seq", data.No_seq);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@Nama", data.Nama);
            param.Add("@Qty", data.Qty);
            param.Add("@Kd_satuan", data.Kd_satuan);
            param.Add("@qty_tarik", data.qty_tarik);
            param.Add("@harga", data.harga);
            param.Add("@Jns_retur", data.Jns_retur);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@Status", data.Status);
            param.Add("@departemen", data.departemen);
            param.Add("@Last_create_date", data.Last_create_date);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Programe_name", data.Programe_name);

            param.Add("@potongan", data.potongan);
            param.Add("@potongan_total", data.potongan_total);
            param.Add("@nama_potongan", data.nama_potongan);
            param.Add("@lokasi", data.lokasi);
            param.Add("@Total", data.Total);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SPFIN_AUTOMAN_JURNAL(SALES_RETUR data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@tipe_trans", data.Tipe_trans);
                param.Add("@no_inv", data.No_retur);

                return conn.Execute("FIN.dbo.FIN_AUTOMAN_JURNAL", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public static async Task<int> SPFIN_CATALOG_MAKEJUR(string invNo, string tipetrans, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@tipe_trans", tipetrans);
                param.Add("@no_inv", invNo);

                return conn.Execute("FIN.dbo.FIN_CATALOG_MAKEJUR", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<string> SPFIN_POSTING(string no_jurnal, DateTime vtgl, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@vnojur", no_jurnal, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@vtgl", vtgl, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                param.Add("@vno_posting", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var result = conn.Execute("FIN.dbo.FIN_POSTING", param, trans, null, CommandType.StoredProcedure);
                var retVal = param.Get<string>("@vno_posting");
                return retVal;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }


        public static async Task<string> SaveSJ(string no_krm, string no_so, string kdcabang, string pegawai, int jns_sj, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();

                param.Add("@kd_cabang", kdcabang, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@rcn_krm", no_krm, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@no_sp", no_so, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@kdpeg", pegawai, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@no_sj2", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var result = conn.Execute("[SALES].[dbo].[BuatSuratJalan2]", param, trans, null, CommandType.StoredProcedure);
                var retVal = param.Get<string>("@no_sj2");
                return retVal;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> UpdateCetakDPB(CETAK_DPB data, string Status_Inspeksi, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE sales.dbo.sales_so_d set QtyCetak=@QtyCetak, Qty_sisa=ISNULL(Qty_sisa,0) + 1, Status_Inspeksi=@Status_Inspeksi" +
                " where no_sp=@no_sp AND Status_Simpan = 'Y' and Kd_Stok =@Kd_Stok and No_seq =@No_seq";
            param = new DynamicParameters();
            param.Add("@QtyCetak", data.sisa);
            param.Add("@Status_Inspeksi", Status_Inspeksi);
            param.Add("@no_sp", data.No_sp);
            param.Add("@Kd_Stok", data.Kd_Stok);
            param.Add("@No_seq", data.no_seq_d);
           
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static List<Response> getCountDO(DateTime DateFrom, DateTime DateTo, string filterquery = "", string kdcb = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result, SO.No_sp " +
                   " FROM SALES.DBO.SALES_SO SO WITH(NOLOCK) " +
                   " WHERE SO.Kd_Cabang=@Kd_Cabang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Kd_Cabang", kdcb);


                if (DateFrom != null)
                {


                    filter += " AND SO.Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    filter += " AND SO.Tgl_sp <= @DateTo ";
                }

                if (filterquery != null && filterquery != "")
                {

                    filterquery = filterquery.Replace("status_po", "S.STATUS_DO");
                    filterquery = filterquery.Replace("no_po", "S.No_sp");
                    filterquery = filterquery.Replace("tgl_po", "S.Tgl_sp");
                    // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
                    filter += " " + filterquery + " ";
                    //filter += filterquery;
                }
                //if (barang != null && barang != "")
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                //}


                sql += filter;
                sql += " GROUP BY SO.No_sp ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


    }
}
