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
    public class FIN_LunasRepo
    {
        public static async Task<IEnumerable<FIN_NOTA>> GetInvoice(string kdcb = null, string kdcustomer = null, string kdvaluta = null)

        {
            using (var con = DataAccess.GetConnection())
            {

                string Query = "";
                DynamicParameters param = new DynamicParameters();

                string filter = "";
                Query = " select a.kd_cust,a.kd_cabang,a.kd_valuta,a.no_posting,a.no_ref2, a.no_inv,a.tgl_inv,(a.jml_tagihan - isnull(sum(d.jml_bayar),0) ) - isnull(a.jml_bayar_pending,0) as jml_tagihan,a.tgl_jth_tempo,(a.jml_tagihan - isnull(sum(d.jml_bayar),0)) - isnull(a.jml_bayar_pending,0) as jml_akhir from FIN.dbo.FIN_Nota a left join FIN.dbo.FIN_NOTA_LUNAS_D d on d.prev_no_inv = a.no_inv   " +
                        " inner join SALES.dbo.SALES_SO s on a.no_ref2=s.No_sp and s.Jenis_sp <> 'CASH' ";
                param = new DynamicParameters();
                param.Add("@kdcb", kdcb);
                param.Add("@kdcustomer", kdcustomer);
                param.Add("@kdvaluta", kdvaluta);


                if (kdcustomer != string.Empty && kdcustomer != null && kdvaluta != string.Empty && kdvaluta != null)
                {

                    filter += " where a.Kd_cabang = @kdcb and a.kd_cust = @kdcustomer and a.kd_valuta = @kdvaluta " +
" and(a.tipe_trans in ('JPJ-KPT-01', 'JPJ-KPT-05', 'JPJ-KPT-06')) " +
" and(a.jml_tagihan - a.jml_bayar) - isnull(a.jml_bayar_pending, 0) > 0 " +
"  and isnull( a.no_posting ,'')='' and kd_cust <> 'CASH' " +
" group by a.kd_cust,a.kd_cabang,a.kd_valuta,a.no_posting,a.no_ref2, a.no_inv,a.tgl_inv,a.jml_bayar_pending,a.jml_tagihan,a.tgl_jth_tempo ";
                }

                Query += filter;
                var res = con.Query<FIN_NOTA>(Query, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<v_masalah>> GetOverdue(string kd_cust = null)

        {
            using (var con = DataAccess.GetConnection())
            {

                string Query = "";
                DynamicParameters param = new DynamicParameters();

                string filter = "";
                Query = " select no_inv,tgl_jatuh_tempo,hari_jatuh_tempo,jml_akhir,lama_hari from  sales.dbo.getInv_Masalah(@kdcustomer)";
                param = new DynamicParameters();
              
                param.Add("@kdcustomer", kd_cust);
               

                Query += filter;
                var res = con.Query<v_masalah>(Query, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<int> DelNotaDetail(string nomor, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [FIN].[dbo].[FIN_NOTA_LUNAS_D] WITH(ROWLOCK)  " +
                " WHERE no_trans=@nomor;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO] SET STATUS_DO='BATAL',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
            //    " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@nomor", nomor);
            // param.Add("@Last_Updated_By", data.Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> Save(FIN_NOTA_LUNAS data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_NOTA_LUNAS] (Kd_cabang,tipe_trans,no_trans,tgl_trans,no_ref1,no_ref2,no_ref3,thnbln,kd_kartu,kd_valuta,kurs_valuta," +
                "jml_val_trans,jml_rp_trans,jml_tagihan,jml_bayar,Jns_bayar,jns_giro_trans,no_giro,kd_bank,tgl_posting,no_posting,tgl_batal,keterangan,status,kd_buku_besar," +
                "Last_create_date,Last_created_by,Program_name,no_do,jml_titipan,no_batal,jml_giro,jml_transfer,jml_tunai) " +
                    "VALUES(@Kd_cabang,@tipe_trans,@no_trans,@tgl_trans,@no_ref1,@no_ref2,@no_ref3,@thnbln,@kd_kartu,@kd_valuta,@kurs_valuta,@jml_val_trans,@jml_rp_trans," +
                    "@jml_tagihan,@jml_bayar,@Jns_bayar,@jns_giro_trans,@no_giro,@kd_bank,@tgl_posting,@no_posting,@tgl_batal,@keterangan,@status,@kd_buku_besar," +
                    "GETDATE(),@Last_created_by,@Program_name,@no_do,@jml_titipan,@no_batal,@jml_giro,@jml_transfer,@jml_tunai);";
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
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_val_trans", data.jml_val_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@jml_tagihan", data.jml_tagihan);
            param.Add("@jml_bayar", data.jml_bayar);
            param.Add("@Jns_bayar", data.Jns_bayar);
            param.Add("@jns_giro_trans", data.jns_giro_trans);
            param.Add("@no_giro", data.no_giro);
            param.Add("@kd_bank", data.kd_bank);
            param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@no_posting", data.no_posting);
            param.Add("@tgl_batal", data.tgl_batal);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Program_name", data.Program_name);
            param.Add("@no_do", data.no_do);
            param.Add("@jml_titipan", data.jml_titipan);
            param.Add("@no_batal", data.no_batal);
            param.Add("@jml_giro", data.jml_giro);
            param.Add("@jml_transfer", data.jml_transfer);
            param.Add("@jml_tunai", data.jml_tunai);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(FIN_NOTA_LUNAS_D data, SqlTransaction trans)
        {
            //no rowlock
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "INSERT INTO [FIN].[dbo].[FIN_NOTA_LUNAS_D]  " +
                "(Kd_Cabang,tipe_trans,no_trans,no_seq,prev_no_inv,jml_tagihan,jml_piut,jml_ppn,jml_pph,jml_diskon,rek_biaya1,jml_biaya1," +
                "jml_pembulatan,rek_biayalain,biaya_lain,pendp_lain,jml_bayar,kd_buku_besar,kd_kartu,keterangan,status," +
                "Last_create_date,Last_created_by,Programe_name) " +
                "VALUES (@Kd_Cabang,@tipe_trans,@no_trans,@no_seq,@prev_no_inv,@jml_tagihan,@jml_piut,@jml_ppn,@jml_pph,@jml_diskon,@rek_biaya1," +
                "@jml_biaya1,@jml_pembulatan,@rek_biayalain,@biaya_lain,@pendp_lain,@jml_bayar,@kd_buku_besar,@kd_kartu,@keterangan,@status," +
                "GETDATE(),@Last_created_by,@Programe_name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_seq", data.no_seq);
            param.Add("@prev_no_inv", data.prev_no_inv);
            param.Add("@jml_tagihan", data.jml_tagihan);
            param.Add("@jml_piut", data.jml_piut);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@jml_pph", data.jml_pph);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@rek_biaya1", data.rek_biaya1);
            param.Add("@jml_biaya1", data.jml_biaya1);
            param.Add("@jml_pembulatan", data.jml_pembulatan);
            param.Add("@rek_biayalain", data.rek_biayalain);
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

        public static async Task<int> SaveGiro(FIN_GIRO data, string no_trans, SqlTransaction trans)
        {
            //no rowlock
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "update [FIN].[dbo].[FIN_GIRO] set no_ref = @no_trans " +
              " where nomor = @nomor and tipe_trans=@tipe_trans and jns_trans=@jns_trans";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@jns_trans", data.jns_trans);
            param.Add("@nomor", data.nomor);
            param.Add("@no_trans", no_trans);
            //param.Add("@potongan", data.potongan);
            //param.Add("@potongan", data.potongan);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<IEnumerable<FIN_NOTA_LUNAS>> GetNota(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT * FROM FIN.dbo.FIN_NOTA_LUNAS WITH (NOLOCK) ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@stat", stat);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE Kd_Cabang=@kd_cabang and no_trans LIKE CONCAT('%',@id,'%')  ";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE tgl_trans >= @DateFrom ";
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
                            filter += " WHERE  tgl_trans <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND tgl_trans <= @DateTo ";
                    }

                }

                //{
                //    filter += " AND ";
                //}
                filter += " AND DATEDIFF(day, tgl_trans, getdate()) <=30 ";

                sql += filter;

                sql += " ORDER BY Last_Create_Date DESC ";


                var res = con.Query<FIN_NOTA_LUNAS>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_NOTA_LUNAS_D>> GetNotaD(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT * FROM FIN.dbo.FIN_NOTA_LUNAS_D WITH (NOLOCK) ";

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
                    filter += " WHERE Kd_Cabang=@kd_cabang AND status<>'BATAL' AND DATEDIFF(day, Last_create_date, getdate()) <=60  ";
                }
                //else
                //{
                //    filter += " AND ";
                //}
                //filter += " ";

                sql += filter;

                sql += " ORDER BY no_seq ASC ";

                var res = con.Query<FIN_NOTA_LUNAS_D>(sql, param, null, true, 36000);

                return res;
            }
        }


    }
}
