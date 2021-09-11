using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Helpers;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class FIN_APRepo
    {
        public static async Task<IEnumerable<FIN_PEMBELIAN>> GetInvoice(string kdcb = null, string kd_supplier = null, string kdvaluta = null)

        {
            using (var con = DataAccess.GetConnection())
            {

                string Query = "";
                DynamicParameters param = new DynamicParameters();

                string filter = "";
                Query = " select a.kd_supplier,a.kd_cabang,a.kd_valuta,a.no_posting,a.no_ref2, a.no_inv,a.tgl_inv,a.jml_bayar_pending,a.jml_akhir_tagihan as jml_tagihan,a.tgl_jth_tempo from(select x.*, (select distinct sum(jml_tagihan) from FIN.dbo.FIN_PEMBELIAN where kd_supplier=X.kd_supplier and kd_valuta=X.kd_valuta and tipe_trans =X.tipe_trans and no_ref1 = x.no_ref1 and jml_akhir > 0 and not (x.no_posting is null or x.no_posting='')) as jmltagihan,(x.jml_tagihan - isnull(sum(bd.jml_bayar),0)) - ISNULL(x.jml_bayar_pending,0) as jml_akhir_tagihan " +
 " from FIN.dbo.FIN_PEMBELIAN X left " +
 " join FIN.dbo.FIN_BELI_LUNAS_D bd on bd.prev_no_inv = X.no_inv " +
 " where X.kd_supplier = @kd_supplier and X.kd_valuta = @kdvaluta " +
 " and X.tipe_trans in('JPP-KUT-01', 'JPP-KUT-03', 'JPP-KUT-04') " +
 " and X.jml_akhir > 0 and ISNULL(X.no_posting,'')= '' " +
 " group by x.Kd_cabang,x.tipe_trans,x.no_inv,x.tgl_inv,x.no_ref1,x.no_ref2,x.no_ref3,x.thnbln,x.kd_supplier,x.nm_supplier,x.almt_agen,x.kd_valuta,x.kurs_valuta,x.jml_val_trans,x.jml_rp_trans, " +
 " x.flag_ppn,x.jml_val_ppn,x.jml_rp_ppn,x.no_fakt_pajak,x.jml_diskon,x.jml_tagihan,x.jml_bayar,x.jml_akhir,x.tgl_jth_tempo,x.tgl_posting,x.no_posting,x.tgl_batal,x.prev_no_inv,x.keterangan,x.kd_buku_besar, " +
 " x.sts_jur,x.no_jurnal,x.Last_create_date,x.Last_created_by,x.Last_update_date,x.Last_updated_by,x.Program_name,x.status, " +
 " x.tgl_approve,x.ket_approve,x.no_sj,x.sts_tagihan,x.ongkir,x.jml_bayar_pending,x.jml_giro,x.jml_tunai,x.jml_transfer) a " +
 " where a.jml_akhir_tagihan > 0 " +
 " ORDER BY a.tgl_inv desc  ";
                param = new DynamicParameters();
                param.Add("@kdcb", kdcb);
                param.Add("@kd_supplier", kd_supplier);
                param.Add("@kdvaluta", kdvaluta);


//                if (kd_supplier != string.Empty && kd_supplier != null && kdvaluta != string.Empty && kdvaluta != null)
//                {

//                    filter += " where a.Kd_cabang = @kdcb and a.kd_cust = @kdcustomer and a.kd_valuta = @kdvaluta " +
//" and(a.tipe_trans in ('JPJ-KPT-01', 'JPJ-KPT-05', 'JPJ-KPT-06')) " +
//" and(a.jml_tagihan - a.jml_bayar) - isnull(a.jml_bayar_pending, 0) > 0 " +
//"  and isnull( a.no_posting ,'')='' and kd_cust <> 'CASH' ";
//                }

                Query += filter;
                var res = con.Query<FIN_PEMBELIAN>(Query, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<int> DelDataDetail(string nomor, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [FIN].[dbo].[FIN_BELI_LUNAS_D] WITH(ROWLOCK) " +
                " WHERE no_trans=@nomor;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO] SET STATUS_DO='BATAL',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
            //    " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@nomor", nomor);
            // param.Add("@Last_Updated_By", data.Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> Save(FIN_BELI_LUNAS data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_BELI_LUNAS] (Kd_cabang,tipe_trans,no_trans,tgl_trans,no_ref1,no_ref2,no_ref3,thnbln,kd_kartu,kd_valuta,kurs_valuta,jml_val_trans,jml_rp_trans,jml_tagihan,jml_bayar,Jns_bayar,no_um," +
                "kd_giro,kd_bank,tgl_posting,no_posting,tgl_batal,keterangan,status,kd_buku_besar,kartu,Last_create_date,Last_created_by,Program_name,no_batal,jml_titipan,jml_giro,jml_transfer,jml_tunai) " +
                    "VALUES(@Kd_cabang,@tipe_trans,@no_trans,@tgl_trans,@no_ref1,@no_ref2,@no_ref3,@thnbln,@kd_kartu,@kd_valuta,@kurs_valuta,@jml_val_trans,@jml_rp_trans,@jml_tagihan,@jml_bayar,@Jns_bayar,@no_um," +
                    "@kd_giro,@kd_bank,@tgl_posting,@no_posting,@tgl_batal,@keterangan,@status,@kd_buku_besar,@kartu,GETDATE(),@Last_created_by,@Program_name,@no_batal,@jml_titipan,@jml_giro,@jml_transfer,@jml_tunai);";
            param = new DynamicParameters();
            param.Add("@Kd_cabang", data.Kd_cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@no_ref1", data.no_ref1);
            param.Add("@no_ref2", data.no_ref2);
            param.Add("@no_ref3", data.no_ref3);
            param.Add("@thnbln", data.thnbln);
            param.Add("@kd_kartu", data.kd_kartu);
            param.Add("@kartu", data.kartu);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_val_trans", data.jml_val_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@jml_tagihan", data.jml_tagihan);
            param.Add("@jml_bayar", data.jml_bayar);
            param.Add("@Jns_bayar", data.Jns_bayar);
            param.Add("@no_um", data.no_um);
            param.Add("@kd_giro", data.kd_giro);
            param.Add("@kd_bank", data.kd_bank);
            param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@no_posting", data.no_posting);
            param.Add("@tgl_batal", data.tgl_batal);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Program_name", data.Program_name);
            //param.Add("@no_do", data.no_do);
            param.Add("@jml_titipan", data.jml_titipan);
            param.Add("@no_batal", data.no_batal);
            param.Add("@jml_giro", data.jml_giro);
            param.Add("@jml_transfer", data.jml_transfer);
            param.Add("@jml_tunai", data.jml_tunai);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(FIN_BELI_LUNAS_D data, SqlTransaction trans)
        {
            //no rowlock
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "INSERT INTO [FIN].[dbo].[FIN_BELI_LUNAS_D]  " +
                " (Kd_Cabang,tipe_trans,no_trans,no_seq,prev_no_inv,jml_tagihan,jml_piut,jml_ppn,jml_pph,jml_diskon,jml_admin,pendp_lain,biaya_lain,jml_pembulatan,jml_bayar," +
                "kd_buku_besar,kd_kartu,keterangan,status,Last_create_date,Last_created_by,Programe_name) " +
                    "VALUES (@Kd_Cabang,@tipe_trans,@no_trans,@no_seq,@prev_no_inv,@jml_tagihan,@jml_piut,@jml_ppn,@jml_pph,@jml_diskon,@jml_admin,@pendp_lain,@biaya_lain,@jml_pembulatan,@jml_bayar," +
                    "@kd_buku_besar,@kd_kartu,@keterangan,@status,GETDATE(),@Last_created_by,@Programe_name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_seq", data.no_seq);
            param.Add("@prev_no_inv", data.prev_no_inv);
            param.Add("@jml_tagihan", data.jml_tagihan);
            param.Add("@jml_piut", data.jml_piut);
            param.Add("@jml_pph", data.jml_pph);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@jml_admin", data.jml_admin);
            //param.Add("@jml_biaya1", data.jml_biaya1);
            param.Add("@jml_pembulatan", data.jml_pembulatan);
           // param.Add("@rek_biayalain", data.rek_biayalain);
            param.Add("@biaya_lain", data.biaya_lain);
            param.Add("@pendp_lain", data.pendp_lain);
            param.Add("@jml_bayar", data.jml_bayar);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_kartu", data.kd_kartu);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Programe_name", data.Programe_name);
            //param.Add("@potongan", data.potongan);
            //param.Add("@potongan", data.potongan);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<FIN_BELI_LUNAS>> GetMonAP(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT C.alamat, C.nama,C.fax1 as telp, C.fax2 as wa,sp.Nama_Supplier as kd_kartu,*FROM FIN.dbo.FIN_BELI_LUNAS S WITH(NOLOCK) " +
 "INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON S.Kd_cabang = C.kd_cabang " +
 " left JOIN  SIF.dbo.SIF_Supplier sp  WITH(NOLOCK) on sp.Kode_Supplier = S.kd_kartu ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@stat", stat);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE S.Kd_Cabang=@kd_cabang and S.no_trans LIKE CONCAT('%',@id,'%')  ";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE S.tgl_trans >= @DateFrom ";
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
                            filter += " WHERE  S.tgl_trans <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND S.tgl_trans <= @DateTo ";
                    }

                }

                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, S.tgl_trans, getdate()) <=30 ";

                sql += filter;

                sql += " ORDER BY S.Last_Create_Date DESC ";


                var res = con.Query<FIN_BELI_LUNAS>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_BELI_LUNAS_D>> GetMonAP_D(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT * FROM FIN.dbo.FIN_BELI_LUNAS_D WITH (NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@stat", stat);
                param.Add("@kd_cabang", cb);

                if (id != string.Empty && id != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE  ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " no_trans = @id ";
                }
                if (filter == "")
                {
                    filter += " WHERE Kd_Cabang=@kd_cabang AND status<>'BATAL' AND DATEDIFF(day, Last_create_date, getdate()) <=60 ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                //filter += "  ";

                sql += filter;

                sql += " ORDER BY no_seq ASC ";

                var res = con.Query<FIN_BELI_LUNAS_D>(sql, param, null, true, 36000);

                return res;
            }
        }
    }
}
