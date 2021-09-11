using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class GUDANG_OUTRepo
    {
        public static async Task<int> SaveGudangOut(INV_GUDANG_OUT data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_OUT]   (Kd_Cabang, no_trans, tipe_trans, tgl_trans, no_ref, tgl_ref, kd_kegiatan, " +
                    " no_rph, jml_rp_trans, jml_ppn, penerima, blthn, no_posting, tgl_posting, keterangan, no_jurnal, Last_Create_Date, Last_Created_By, " +
                      "   Program_Name, sudah_sj, cetak_ke, cetak_ulang,gudang_asal,gudang_tujuan) " +
                    "VALUES(@Kd_Cabang,@no_trans,@tipe_trans,@tgl_trans,@no_ref,@tgl_ref,@kd_kegiatan,@no_rph,@jml_rp_trans,@jml_ppn,@penerima,@blthn,@no_posting," +
                    "@tgl_posting,@keterangan,@no_jurnal,GETDATE(),@Last_Created_By,@Program_Name,@sudah_sj,@cetak_ke,@cetak_ulang,@gd_asal,@gd_tujuan) ";

            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_ref", data.no_ref);
            param.Add("@tgl_ref", data.tgl_ref);
            param.Add("@kd_kegiatan", data.kd_kegiatan);
            param.Add("@no_rph", data.no_rph);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@penerima", data.penerima);
            param.Add("@blthn", data.blthn);
            param.Add("@no_posting", data.no_posting);
            param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@keterangan", data.keterangan);
            param.Add("@no_jurnal", data.no_jurnal);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@sudah_sj", 0);
            param.Add("@cetak_ke", data.cetak_ke);
            param.Add("@cetak_ulang", data.cetak_ulang);
            param.Add("@Last_Create_Date", DateTime.Now);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@gd_tujuan", data.gudang_tujuan);
            param.Add("@gd_asal", data.gudang_asal);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveAdjGudangOut(INV_OPNAME data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_OUT]   (Kd_Cabang, no_trans, tipe_trans, tgl_trans, no_ref, jml_rp_trans, jml_ppn, penerima, blthn, no_posting, keterangan, no_jurnal, Last_Create_Date, Last_Created_By, " +
                      "   Program_Name, sudah_sj, cetak_ke,gudang_asal,gudang_tujuan) " +
                    "VALUES(@Kd_Cabang,@no_trans,@tipe_trans,@tgl_trans,@no_ref,@jml_rp_trans,@jml_ppn,@penerima,@blthn,@no_posting," +
                    "@keterangan,@no_jurnal,GETDATE(),@Last_Created_By,@Program_Name,@sudah_sj,@cetak_ke,@gd_asal,@gd_tujuan) ";

            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_ref", "ADJ OPN");
            //param.Add("@tgl_ref", data.tgl_ref);
            //param.Add("@kd_kegiatan", data.kd_kegiatan);
            //param.Add("@no_rph", data.no_rph);
            param.Add("@jml_rp_trans", 0);
            param.Add("@jml_ppn", 0);
            param.Add("@penerima", data.Last_Created_By);
            param.Add("@blthn", data.blthn);
            param.Add("@no_posting", data.no_posting);
            //param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@keterangan", data.keterangan);
            param.Add("@no_jurnal", data.no_jurnal);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@sudah_sj", 0);
            param.Add("@cetak_ke", 0);
            //param.Add("@cetak_ulang", data.cetak_ulang);
            param.Add("@Last_Create_Date", DateTime.Now);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@gd_tujuan", data.gudang);
            param.Add("@gd_asal", data.gudang);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveOpnDetail(INV_OPNAME_DTL data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_OUT_D]   (Kd_Cabang, no_trans, tipe_trans, no_seq, kd_stok,kd_satuan, kd_ukuran, qty_order, qty_out, qty_sisa, gudang_asal, gudang_tujuan, " +
                       "  harga, rp_trans, blthn, kd_buku_besar, kd_buku_biaya, keterangan, Last_Create_Date, Last_Created_By, Program_Name, siap_sj, qty_batal) " +
                    " VALUES(@Kd_Cabang,@no_trans,@tipe_trans,@no_seq,@kd_stok,@kd_satuan,@kd_ukuran,@qty_order,@qty_out,@qty_sisa,@gudang_asal,@gudang_tujuan," +
                    "@harga,@rp_trans,@blthn,@kd_buku_besar,@kd_buku_biaya,@keterangan,@Last_Create_Date,@Last_Created_By,@Program_Name,@siap_sj,@qty_batal); ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);

            param.Add("@tipe_trans", data.kd_jenis);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@kd_satuan", data.kd_satuan);
            //param.Add("@panjang", data.panjang);
            //param.Add("@lebar", data.lebar);
            //param.Add("@tinggi", data.tinggi);
            param.Add("@qty_order", data.qty_opname);
            param.Add("@qty_out", data.qty_kartu);
            param.Add("@qty_sisa", data.qty_selisih);
            param.Add("@gudang_asal", data.kode_gudang);
            param.Add("@gudang_tujuan", data.kode_gudang);
            param.Add("@harga", data.nilai_rata);
            param.Add("@rp_trans", 0); // (data.nilai_rata) * (data.qty_kartu));
            param.Add("@blthn", data.bultah);
            param.Add("@kd_buku_besar", data.rek_persediaan);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@keterangan", "Adjustment Kartu vs Saldo");
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", "Adjustment Opn");
            param.Add("@siap_sj", 1);
            param.Add("@qty_batal", 0);
            //param.Add("@status_brg_batal", data.status_brg_batal);
            //param.Add("@kd_dept", data.kd_dept);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> DeleteDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [INV].[dbo].[INV_GUDANG_OUT_D]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> CekGudangOut(string no_ref, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "Select count(no_ref) FROM [INV].[dbo].[INV_GUDANG_OUT] WHERE no_ref=@no_ref;";
            param = new DynamicParameters();
            param.Add("@no_ref", no_ref);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(INV_GUDANG_OUT_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_OUT_D]   (Kd_Cabang, no_trans, no_ref, no_ref2, tgl_ref2, no_pol, sopir, no_sp_dtl, seq_dpb, " +
                " tipe_trans, no_seq, kd_stok, kd_ukuran, kd_satuan, panjang, lebar, tinggi, qty_order, qty_out, qty_sisa, gudang_asal, gudang_tujuan, " +
                       "  harga, rp_trans, blthn, kd_buku_besar, kd_buku_biaya, keterangan, Last_Create_Date, Last_Created_By, Program_Name, siap_sj, qty_batal, " +
                       " status_brg_batal, kd_dept) " +
                    " VALUES(@Kd_Cabang,@no_trans,@no_ref,@no_ref2,@tgl_ref2,@no_pol,@sopir,@no_sp_dtl,@seq_dpb,@tipe_trans,@no_seq,@kd_stok,@kd_ukuran,@kd_satuan,@panjang,@lebar,@tinggi,@qty_order,@qty_out,@qty_sisa,@gudang_asal,@gudang_tujuan,@harga,@rp_trans,@blthn,@kd_buku_besar,@kd_buku_biaya,@keterangan,@Last_Create_Date,@Last_Created_By,@Program_Name,@siap_sj,@qty_batal,@status_brg_batal,@kd_dept); ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_ref", data.no_ref);
            param.Add("@no_ref2", data.no_ref2);
            param.Add("@tgl_ref2", data.tgl_ref2);
            param.Add("@no_pol", data.no_pol);
            param.Add("@sopir", data.sopir);
            param.Add("@no_sp_dtl", data.no_sp_dtl);
            param.Add("@seq_dpb", data.seq_dpb);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@panjang", data.panjang);
            param.Add("@lebar", data.lebar);
            param.Add("@tinggi", data.tinggi);
            param.Add("@qty_order", data.qty_order);
            param.Add("@qty_out", data.qty_out);
            param.Add("@qty_sisa", data.qty_sisa);
            param.Add("@gudang_asal", data.gudang_asal);
            param.Add("@gudang_tujuan", data.gudang_tujuan);
            param.Add("@harga", data.harga);
            param.Add("@rp_trans", data.rp_trans);
            param.Add("@blthn", data.blthn);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@keterangan", data.keterangan);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", "SJ");
            param.Add("@siap_sj", data.siap_sj);
            param.Add("@qty_batal", data.qty_batal);
            param.Add("@status_brg_batal", data.status_brg_batal);
            param.Add("@kd_dept", data.kd_dept);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<INV_GUDANG_OUT>> getGudang(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT a.Kd_Cabang,c.alamat,c.fax1,c.fax2, g.Nama_Gudang nama_gdtujuan , a.no_trans, a.tipe_trans, a.tgl_trans, a.no_ref, a.tgl_ref, a.kd_kegiatan, a.no_rph, a.jml_rp_trans, a.jml_ppn, a.penerima, a.blthn, a.no_posting, a.tgl_posting, a.keterangan, a.no_jurnal,a.Last_Created_By,a.Last_Create_Date, a.sudah_sj, a.cetak_ke, a.cetak_ulang,a.gudang_asal,a.gudang_tujuan        " +
                    " FROM [INV].[dbo].[INV_GUDANG_OUT] a WITH(NOLOCK)  " +
                    " LEFT OUTER join SIF.dbo.SIF_Gudang g WITH(NOLOCK) on g.Kode_Gudang=a.gudang_tujuan " +
                    " LEFT OUTER JOIN SIF.dbo.SIF_cabang c WITH(NOLOCK) on a.Kd_Cabang=c.kd_cabang ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE a.no_trans=@no_trans ";
                }

                sql += " ORDER BY a.Last_Create_Date DESC, CASE WHEN a.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_OUT>(sql, param);

                return res;
            }
        }
        public static async Task<IEnumerable<INV_GUDANG_OUT>> getMonMutasi(string no_trans = null, string tipe_trans=null, DateTime? DateFrom=null,DateTime? DateTo=null, string kdcb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT Kd_Cabang, no_trans, tipe_trans, tgl_trans, no_ref, tgl_ref, kd_kegiatan, no_rph, jml_rp_trans, jml_ppn, penerima, blthn, no_posting, tgl_posting, keterangan, no_jurnal, Last_Create_Date, Last_Created_By, Program_Name, sudah_sj, cetak_ke, cetak_ulang  " +
                //    " FROM [INV].[dbo].[INV_GUDANG_OUT] WITH(NOLOCK) where isnull(rec_stat,'Y')<> 'N' ";
                string sql = " SELECT g.Kd_Cabang,sg2.Nama_Gudang as gudang_tujuan,sg1.Nama_Gudang as gudang_asal,no_trans, tipe_trans,case when g.sudah_sj=1 then 'CLOSE' else 'OUTSTANDING' END as stat, tgl_trans, no_ref, tgl_ref,  jml_rp_trans, jml_ppn, penerima, blthn, no_posting, tgl_posting, g.keterangan, no_jurnal, g.Last_Create_Date, g.Last_Created_By, g.Program_Name, sudah_sj, cetak_ke, cetak_ulang    " +
                             " FROM[INV].[dbo].[INV_GUDANG_OUT] g WITH(NOLOCK) " +
                             " inner join SIF.dbo.SIF_cabang c  WITH(NOLOCK) on c.kd_cabang = g.Kd_Cabang " +
                             " LEFT join SIF.dbo.SIF_Gudang sg2   WITH(NOLOCK) on sg2.Kode_Gudang=g.gudang_tujuan" +
                             " LEFT join SIF.dbo.SIF_Gudang sg1   WITH(NOLOCK) on sg1.Kode_Gudang=g.gudang_asal   " +
                             " where isnull(g.rec_stat,'Y')<> 'N' ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@tipe_trans", tipe_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kdcb", kdcb);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " AND no_trans=@no_trans ";
                }
                else if (tipe_trans != string.Empty && tipe_trans != null)
                {
                    sql += " AND tipe_trans=@tipe_trans "; //and isnull(sudah_sj,0) = 0
                }
                if (DateFrom != null)
                {

                    sql += " AND tgl_trans >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    sql += " AND tgl_trans <= @DateTo ";
                }

                sql += " ORDER BY g.Last_Create_Date DESC, CASE WHEN g.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_OUT>(sql, param);

                return res;
            }
        }

        public static async Task<int> InsertStok_out(INV_GUDANG_IN data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN]   (Kd_Cabang,tipe_trans,no_qc,no_ref,tgl_trans,jml_rp_trans,penyerah,keterangan,blthn,Program_Name,sj_supplier,Last_Create_Date,Last_Created_By,no_trans,kode_gudang) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_qc,@no_ref,GETDATE(),@jml_rp_trans,@penyerah,@keterangan,@blthn,@Program_Name,@sj_supplier,GETDATE(),@Last_Created_By,@no_trans,@kode_gudang);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_trans", data.no_trans);// no gudang in
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_ref", data.no_ref);
            param.Add("@sj_supplier", data.sj_supplier);
            param.Add("@jml_rp_trans", 0);
            param.Add("@penyerah", data.Last_Updated_By);
            param.Add("@keterangan", "Barang Kembali");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));
            param.Add("@Program_Name", "Pembatalan MtsOut");


            // param.Add("@Last_Create_Date", data.Last_updated_by);
            param.Add("@Last_Created_By", data.Last_Updated_By);

            param.Add("@no_qc", data.no_ref);
            param.Add("@kode_gudang", "EXP01");


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> InsertStokDtl_out(INV_GUDANG_IN_D data, string gudang_tujuan, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_OUT_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan," +
                    " Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,GETDATE(),@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_qc", data.no_qc); // no ref
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@keterangan", "Pembatalan Mutasi In");
            param.Add("@kd_satuan", data.kd_satuan);
            //param.Add("@kd_ukuran", data.kd_uku);
            //param.Add("@qty_sisa", data.qty_sisa);
            // param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);

            param.Add("@qty_in", data.qty_in); // jika sjk


            param.Add("@qty_order", data.qty_order);
            param.Add("@harga", 0);
            param.Add("@rp_trans", 0);
            param.Add("@gudang_asal", gudang_tujuan);
            param.Add("@gudang_tujuan", "EXP01");
            param.Add("@kd_buku_besar", "00000");
            param.Add("@kd_buku_biaya", "0000");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> DelGudang(string kdcb, string No_Gudang_Out, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_GUDANG_OUT  where no_trans = @no_inv and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);

            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DelGudangDtl(string kdcb, string No_Gudang_Out, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_GUDANG_OUT_D  where no_trans = @no_inv and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);
            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateGudang(string kdcb, string No_Gudang_Out, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE INV.dbo.INV_GUDANG_OUT set sudah_sj=1 where no_trans = @no_inv ";
            param = new DynamicParameters();
           // param.Add("@Kd_Cabang", kdcb);

            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DELGudang(string kdcb, string No_Gudang_Out,string user, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE INV.dbo.INV_GUDANG_OUT set rec_stat='N',Last_Update_Date=GETDATE(),Last_Updated_By=@user where no_trans = @no_inv and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);
            param.Add("@user", user);
            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateGudangDtl(string kdcb, string No_Gudang_Out, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE INV.dbo.INV_GUDANG_OUT_D set where no_trans = @no_inv and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);
            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<IEnumerable<INV_GUDANG_OUT_D>> getGudangDetail(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT D.Kd_Cabang, no_trans, no_ref, no_ref2, tgl_ref2, no_pol, sopir, no_sp_dtl, seq_dpb, tipe_trans, no_seq, D.kd_stok, D.kd_ukuran, D.kd_satuan, D.panjang, D.lebar, D.tinggi, qty_order, qty_out, qty_sisa, gudang_asal, gudang_tujuan, harga, rp_trans, blthn, kd_buku_besar, kd_buku_biaya, D.keterangan, D.Last_Create_Date, D.Last_Created_By, D.Program_Name, siap_sj, qty_batal, status_brg_batal, kd_dept, B.Nama_Barang, G.Nama_Gudang, 0 as qty_in " +
                            " FROM[INV].[dbo].[INV_GUDANG_OUT_D] D WITH(NOLOCK) " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G WITH(NOLOCK) ON D.gudang_tujuan = G.Kode_Gudang   " +
                            " INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE no_trans=@no_trans ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_OUT_D>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_OUT_D>> getGudangDetailD(string tipe_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string kdcb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT D.Kd_Cabang, D.no_trans, D.no_ref, D.no_ref2, D.tgl_ref2, D.no_pol, D.sopir, D.no_sp_dtl, D.seq_dpb, D.tipe_trans, D.no_seq, D.kd_stok, D.kd_ukuran, D.kd_satuan, D.panjang, D.lebar, D.tinggi, D.qty_order, D.qty_out, D.qty_sisa, D.gudang_asal, D.gudang_tujuan, D.harga, " +
                             " D.rp_trans, D.blthn, D.kd_buku_besar, D.kd_buku_biaya, D.keterangan, D.Last_Create_Date, D.Last_Created_By, D.Program_Name, D.siap_sj, D.qty_batal, D.status_brg_batal, D.kd_dept, B.Nama_Barang, G.Nama_Gudang, 0 as qty_in " +
                            " FROM [INV].[dbo].[INV_GUDANG_OUT_D] D WITH(NOLOCK) " +
                            " INNER JOIN [INV].[dbo].[INV_GUDANG_OUT] DO WITH(NOLOCK) ON D.no_trans=DO.no_trans " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G WITH(NOLOCK) ON D.gudang_tujuan = G.Kode_Gudang   " +
                            " INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@tipe_trans", tipe_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kdcb", kdcb);

                if (tipe_trans != string.Empty && tipe_trans != null)
                {
                    sql += " WHERE D.tipe_trans=@tipe_trans";
                }
                if (DateFrom != null)
                {

                    sql += " AND DO.tgl_trans >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    sql += " AND DO.tgl_trans <= @DateTo ";
                }
                sql += " ORDER BY D.Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_OUT_D>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_OUT_D>> getSavedGudang(string no_trans = null, string kdcabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT D.Kd_Cabang, D.no_trans, D.no_ref, D.no_ref2, D.tgl_ref2, D.no_pol, D.sopir, D.no_sp_dtl, D.seq_dpb, D.tipe_trans, D.no_seq, D.kd_stok, D.kd_ukuran, D.kd_satuan, D.panjang, D.lebar, D.tinggi, D.qty_order, D.qty_out, D.qty_order as qty_sisa, D.gudang_asal, D.gudang_tujuan, D.harga, D.rp_trans, D.blthn, D.kd_buku_besar, D.kd_buku_biaya, D.keterangan, D.Last_Create_Date, D.Last_Created_By, D.Program_Name, D.siap_sj, D.qty_batal, D.status_brg_batal, D.kd_dept, B.Nama_Barang, G1.Nama_Gudang,G.Nama_Gudang as cb_asal, D.qty_order as qty_in  " +
                            " FROM[INV].[dbo].[INV_GUDANG_OUT_D] D WITH(NOLOCK) " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G WITH(NOLOCK) ON @kdcabang = G.Kd_Cabang AND G.Kode_Gudang=D.gudang_asal  " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G1 WITH(NOLOCK) ON '1 ' = G.Kd_Cabang    AND G1.Kode_Gudang=D.gudang_tujuan " +
                                 " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON C.kd_cabang=D.Kd_Cabang " +
                            //"  LEFT OUTER JOIN SIF.dbo.SIF_Gudang sg WITH (NOLOCK) ON sg.Kode_Gudang=D.gudang_asal " +
                            " INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang  ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@kdcabang", kdcabang);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE no_trans=@no_trans ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_OUT_D>(sql, param);

                return res;
            }
        }

        public static async Task<int> SPGudangOut(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty_out,string gudang_asal, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_ukuran", "00");
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@kode_gudang", gudang_asal);
                param.Add("@tinggi", 0);
                param.Add("@lebar", 0);
                param.Add("@panjang", 0);
                param.Add("@qty_out", qty_out);
                return await conn.ExecuteAsync("[INV].[dbo].[inv_stok_saldo_gudang_out]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPStokOut(string kd_cabang, string blthn,string kd_stok,string kd_satuan,decimal qty_out,  SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_ukuran", "00");
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@tinggi", 0);
                param.Add("@lebar", 0);
                param.Add("@panjang", 0);
                param.Add("@onstok_out", qty_out);
                param.Add("@vnilai", 0);

                return await conn.ExecuteAsync("[INV].[dbo].[inv_onstok_saldo_out]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPGudangKeluarBebas(INV_GUDANG_OUT_D data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", data.Kd_Cabang);
                param.Add("@bultah", data.blthn);
                param.Add("@kd_stok", data.kd_stok);
                param.Add("@kd_ukuran", "00");
                param.Add("@kd_satuan", data.kd_satuan);
                param.Add("@kode_gudang", data.gudang_asal);
                param.Add("@tinggi", 0);
                param.Add("@lebar", 0);
                param.Add("@panjang", 0);
                param.Add("@qty_out", data.qty_out);
                return conn.Execute("[INV].[dbo].[INV_GUDANG_KELUAR_BEBAS]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPStokKeluarBebas(INV_GUDANG_OUT_D data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", data.Kd_Cabang);
                param.Add("@bultah", data.blthn);
                param.Add("@kd_stok", data.kd_stok);
                param.Add("@kd_ukuran", data.kd_ukuran);
                param.Add("@kd_satuan", data.kd_satuan);
                param.Add("@tinggi", data.tinggi);
                param.Add("@lebar", data.lebar);
                param.Add("@panjang", data.panjang);
                param.Add("@onstok_out", data.qty_out);
                param.Add("@vnilai", 0);

                return conn.Execute("[INV].[dbo].[INV_SALDO_KELUAR_BEBAS]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }
        public static async Task<int> SPJurnal(INV_GUDANG_OUT data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@tipe_trans", data.tipe_trans);
                param.Add("@no_inv", data.no_trans);

                return conn.Execute("FIN.dbo.FIN_AUTOMAN_JURNAL", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<IEnumerable<PROD_rcn_krm_D>> GetDsSiapKirim(string no_trans,string kd_cabang, string blnthn)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
           

                string sql = "SELECT s.Atas_Nama,sod.Kd_stok,sod.No_sp as no_do, " +
                    " isnull((select nilai_rata from inv.dbo.inv_stok_saldo where kd_stok = sod.Kd_stok and kd_cabang=@kd_cabang and bultah=(select @blnthn as blthn where kd_stok=sod.Kd_stok )),0) harga ," +
                    " sod.Deskripsi as deskripsi,brg.kd_satuan,dpbm.tanggal,dpbm.no_trans,dpbd.kd_customer,dpbm.kd_kendaraan,dpbm.kd_sopir,spr.Nama_Sopir,dpbm.kd_kenek,dpbd.no_trans as no_dpb,dpbm.tanggal,dpbd.no_sp,dpbd.no_sp_dtl,dpbd.no_seq as seq_dpb," +
                    " isnull(dpbd.panjang, 0) as panjang,isnull(dpbd.lebar, 0) as lebar,isnull(dpbd.tinggi, 0) as tinggi,dpbd.jumlah,brg.kd_ukuran,brg.rek_persediaan,brg.rek_biaya,dpbd.jumlah as qty_out, isnull(gd.akhir_qty, 0) as qty_gd from PROD.dbo.PROD_rcn_krm_d as dpbd " +
                    " inner join PROD.dbo.PROD_rcn_krm_m as dpbm on dpbd.no_trans = dpbm.no_trans" +
                    " INNER JOIN SALES.dbo.SALES_SO_D as sod on dpbd.no_sp = sod.No_sp and dpbd.no_sp_dtl = sod.No_seq" +
                    " left join SALES.dbo.sales_so s on s.No_sp = dpbd.no_sp inner join SIF.dbo.SIF_Barang as brg on sod.Kd_Stok = brg.Kode_Barang" +
                    " LEFT join SIF.dbo.SIF_Sopir as spr on dpbm.kd_sopir = spr.Kode_Sopir" +
                    " left join INV.dbo.INV_STOK_GUDANG gd on gd.kd_stok = sod.Kd_Stok and gd.kode_gudang = @kd_cabang and gd.bultah = (select SUBSTRING(CONVERT(varchar, GETDATE(), 112), 1, 6) as blthn )" +
                    " where dpbm.no_trans = @no_trans order by dpbd.no_sp, len(dpbd.no_sp_dtl),dpbd.no_sp_dtl";
                param.Add("@no_trans", no_trans);
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@blnthn", blnthn);

                var res = con.Query<PROD_rcn_krm_D>(sql, param);

                return res;
            }
        }



        public static async Task<int> DeleteSiapKirimDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [INV].[dbo].[INV_GUDANG_OUT_D]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }



    }
}
