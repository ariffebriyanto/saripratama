using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Base;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class PROD_rcn_krmRepo
    {
        public static async Task<IEnumerable<PRODV_MON_SO>> Getrcnkirim(string kd_cabang, string kode_sales, string no_sp = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "select v.*,'' kd_kenek,ss.Nama_Sales,v.qty_rcn_krm as jumlah, v.qty_sisa_krm as sisa,ISNULL(i.nilai, 0) nilai, ISNULL(i.nilai_m3, 0) nilai_m3,'' as no_dpb,0.00 jml_indeks,0.00 jml_m3, '' rec_stat, '' program_name, '' pending, ''pendingh" +
                //            " from PROD.dbo.PRODV_MON_SO v" +
                //            " left join SIF.dbo.SIF_Barang b on v.Kd_Stok = b.Kode_Barang" +
                //            " left join SIF.dbo.SIF_Sales ss on v.Kd_Sales = ss.kode_sales" +
                //            " left join(select* from SIF.dbo.SIF_indeks_barang where jns_kegiatan = '02') i on b.kd_jenis = i.kd_jenis and b.kd_tipe = i.kd_tipe and b.kd_ukuran = i.kd_ukuran" +
                //            " where(Len(v.no_sp) > 5 And v.qty_sisa_krm > 0)" +
                //            " and(v.no_sp not in (select DISTINCT no_sp from prod.dbo.PROD_rcn_krm_d where isnull(jumlah_balik, 0) > 0) OR v.Jenis_sp in('BO PAKET', 'BOOKING ORDER'))";


                string sql = "SELECT * FROM ( " +
                        " select DISTINCT v.*,'' kd_kenek,v.qty_rcn_krm as jumlah, v.qty_sisa_krm as sisa,ISNULL(i.nilai, 0) nilai, ISNULL(i.nilai_m3, 0) nilai_m3,'' as no_dpb,0.00 jml_indeks,0.00 jml_m3, '' rec_stat, '' program_name, ''pendingh, Nama_Gudang, akhir_qty " +
                        " from PROD.dbo.PRODV_MON_SO v " +
                        " left join SIF.dbo.SIF_Barang b on v.Kd_Stok = b.Kode_Barang " +
                        " left join SIF.dbo.SIF_Sales ss on v.Kd_Sales = ss.kode_sales " +
                        " left join(select* from SIF.dbo.SIF_indeks_barang where jns_kegiatan = '02') i on b.kd_jenis = i.kd_jenis and b.kd_tipe = i.kd_tipe and b.kd_ukuran = i.kd_ukuran " +
                        " LEFT OUTER JOIN INV.dbo.INV_STOK_GUDANG SG ON SG.kd_stok = B.Kode_Barang AND SG.Kd_Cabang = '1 ' AND SG.bultah = FORMAT(GetDate(), 'yyyyMM') " +
                        " LEFT OUTER JOIN SIF.dbo.SIF_Gudang G ON SG.kode_gudang = G.Kode_Gudang and G.divisi='1' " +
                        " where(Len(v.no_sp) > 5 And v.qty_sisa_krm > 0) " +
                        " and(v.no_sp not in (select DISTINCT no_sp from prod.dbo.PROD_rcn_krm_d where isnull(jumlah_balik, 0) > 0) OR v.Jenis_sp in('BO PAKET', 'BOOKING ORDER'))  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@kode_sales", kode_sales);
                param.Add("@no_sp", no_sp);

                if (no_sp != string.Empty && no_sp != null)
                {
                    filter += " AND v.No_sp=@no_sp ";
                }
                else if (kode_sales != string.Empty && kode_sales != null)
                {
                    filter += " AND kd_sales = @kode_sales and ss.kode_sales=@kode_sales and isnull(v.pending,'N') <> 'Y' and isnull(v.isClosed,'N') <> 'Y'  and v.Status_Simpan = 'Y' and v.Kd_Cabang=@kd_cabang and v.Jenis_sp='NON CASH' ";
                }
                else
                {
                    filter += " and isnull(v.pending,'N') <> 'Y' and isnull(v.isClosed,'N') <> 'Y'  and v.Status_Simpan = 'Y' and v.Kd_Cabang=@kd_cabang and v.Jenis_sp='NON CASH'  ";

                }
                sql += filter;
                sql += " AND ((Nama_Gudang IS NULL AND akhir_qty IS NULL) OR (Nama_Gudang IS NOT NULL AND akhir_qty>0)) ) X ORDER BY x.No_sp, len(x.No_seq_d) ";
                var res = con.Query<PRODV_MON_SO>(sql, param);

                return res;
            }

        }

        public static async Task<int> SaveRcnKrm(PROD_rcn_krm data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PROD].[dbo].[PROD_rcn_krm_m]   (Kd_Cabang,kd_departemen,tanggal,no_trans,tipe_trans,kd_kenek,kd_kendaraan,rec_stat,keterangan,last_create_date,Last_Created_By,program_name,inv_stat) " +
                    "VALUES(@Kd_Cabang,@kd_departemen,@tanggal,@no_trans,@tipe_trans,@kd_kenek,@kd_kendaraan,@rec_stat,@keterangan,@last_create_date,@Last_Created_By,@program_name,@inv_stat);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.kd_cabang);
            param.Add("@kd_departemen", data.kd_departemen);
            param.Add("@tanggal", data.tanggal);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@kd_kenek", data.kd_kenek);
            param.Add("@kd_sopir", data.kd_sopir);
            param.Add("@kd_kendaraan", data.kd_kendaraan);
            param.Add("@keterangan", data.keterangan);
            param.Add("@Last_Created_By", data.last_created_by);
            param.Add("@last_create_date", data.last_create_date);
            param.Add("@program_name", data.program_name);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@inv_stat", 0);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static Response getCountMonRcnKirim(DateTime? DateFrom=null, DateTime? DateTo=null, string filterquery = "", string no_sp = "", string kd_customer = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result " +
                             "FROM [PROD].[dbo].[PROD_rcn_krm_m] m left outer join[SIF].[dbo].[SIF_Pegawai] a on a.Kode_Pegawai = m.kd_kenek left outer join[SIF].[dbo].[SIF_Pegawai] b on b.Kode_Pegawai = m.kd_sopir " +
                             "left outer join[PROD].[dbo].[PROD_rcn_krm_d] c on c.no_trans = m.no_trans";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@no_sp", no_sp);
                param.Add("@kd_customer", kd_customer);



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
                    filter += " m.tanggal >= @DateFrom ";
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
                    filter += " m.tanggal <= @DateTo ";
                }

                //if (filterquery != null && filterquery != "")
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    //filterquery = filterquery.Replace("status_po", "PO.status_po");
                //    //filterquery = filterquery.Replace("no_po", "PO.no_po");
                //    //filterquery = filterquery.Replace("tgl_po", "PO.tgl_po");
                //    //filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                //    //filterquery = filterquery.Replace("jml_rp_trans", "PO.jml_rp_trans");
                //    //filterquery = filterquery.Replace("total", "PO.total");
                //    filter += filterquery;
                //}

                if (kd_customer != null && kd_customer != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " c.kd_customer LIKE CONCAT('%',@kd_customer,'%') ";
                }
                if (no_sp != null && no_sp != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " c.no_sp LIKE CONCAT('%',@no_sp,'%') ";
                }

                if (kd_customer != null && kd_customer != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " c.kd_customer LIKE CONCAT('%',@kd_customer,'%') ";
                }


                sql += filter;
                // sql += " GROUP BY PO.no_po ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }


        public async static Task<List<PROD_rcn_krm>> GetMonRcnKirimPartial(DateTime? DateFrom=null, DateTime? DateTo=null, string filterquery = "", string sortingquery = "", string no_sp = "", string kd_customer = "", int seq = 0)
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
                string sql = "SELECT distinct(m.no_trans) no_trans,m.tanggal,m.kd_kendaraan,b.Nama_Pegawai as Nama_Supir,a.Nama_Pegawai as Nama_kenek,m.kd_sopir,m.kd_kenek,m.last_created_by " +
                             "FROM [PROD].[dbo].[PROD_rcn_krm_m] m left outer join[SIF].[dbo].[SIF_Pegawai] a on a.Kode_Pegawai = m.kd_kenek left outer join[SIF].[dbo].[SIF_Pegawai] b on b.Kode_Pegawai = m.kd_sopir " +
                             "left outer join[PROD].[dbo].[PROD_rcn_krm_d] c on c.no_trans = m.no_trans";

                //" INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po" +
                //" INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON D.kd_stok = B.kode_barang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@no_sp", no_sp);
                param.Add("@kd_customer", kd_customer);


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
                    filter += " m.tanggal >= @DateFrom ";
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
                    filter += " m.tanggal <= @DateTo ";
                }

                //if (filterquery != null && filterquery != "")
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    //filterquery = filterquery.Replace("status_po", "PO.status_po");
                //    //filterquery = filterquery.Replace("no_po", "PO.no_po");
                //    //filterquery = filterquery.Replace("tgl_po", "PO.tgl_po");
                //    //filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                //    //filterquery = filterquery.Replace("jml_rp_trans", "PO.jml_rp_trans");
                //    //filterquery = filterquery.Replace("total", "PO.total");
                //    filter += " " + filterquery + " ";
                //}
                if (no_sp != null && no_sp != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " c.no_sp LIKE CONCAT('%',@no_sp,'%') ";
                }

                if (kd_customer != null && kd_customer != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " c.kd_customer LIKE CONCAT('%',@kd_customer,'%') ";
                }


                sql += filter;

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " order by m.tanggal desc, m.no_trans desc ";

                }
                else
                {
                    sql += " order by m.tanggal desc, m.no_trans desc ";
                }


                var res = con.Query<PROD_rcn_krm>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static IEnumerable<PROD_rcn_krm> GetMonRcnKirim(string no_sp = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT distinct(m.no_trans) no_trans,m.tanggal,m.kd_kendaraan,b.Nama_Pegawai as Nama_Supir,a.Nama_Pegawai as Nama_kenek,m.kd_sopir,m.kd_kenek,m.last_created_by " +
                             "FROM [PROD].[dbo].[PROD_rcn_krm_m] m left outer join[SIF].[dbo].[SIF_Pegawai] a on a.Kode_Pegawai = m.kd_kenek left outer join[SIF].[dbo].[SIF_Pegawai] b on b.Kode_Pegawai = m.kd_sopir " +
                             "left outer join[PROD].[dbo].[PROD_rcn_krm_d] c on c.no_trans = m.no_trans";



                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sp", no_sp);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

                if (no_sp != string.Empty && no_sp != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " c.no_sp LIKE CONCAT('%',@no_sp,'%') ";
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
                    filter += " m.tanggal >= @DateFrom ";
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
                    filter += " m.tanggal <= @DateTo ";
                }

                sql += filter;

                sql += " order by m.tanggal desc, m.no_trans desc";


                var res = con.Query<PROD_rcn_krm>(sql, param);

                return res;
            }
        }

        public static IEnumerable<PROD_rcn_krm_D> GetDetRcnKirim(string no_trans = null,DateTime? DateFrom = null, DateTime? DateTo = null,string kd_customer = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select  d.no_trans, d.no_sp ,s.jenis_sp ,s.atas_nama,s.Almt_pnrm,d.kd_barang,b.Nama_Barang from PROD.dbo.PROD_rcn_krm_d d left join PROD.dbo.PROD_rcn_krm_m k on k.no_trans = d.no_trans  " +
                    " left join sales.dbo.sales_so s on s.no_sp = d.no_sp " +
                    " left join sif.dbo.sif_barang b on b.Kode_Barang = d.kd_barang COLLATE Latin1_General_CI_AS";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_customer", kd_customer);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " d.no_trans LIKE CONCAT('%',@no_trans,'%') ";
                }

                if (kd_customer != string.Empty && kd_customer != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " d.kd_customer LIKE CONCAT('%',@kd_customer,'%') ";
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
                    filter += " k.tanggal >= @DateFrom ";
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
                    filter += "  k.tanggal <= @DateTo ";
                }

                sql += filter;

                sql += " order by d.no_sp";


                var res = con.Query<PROD_rcn_krm_D>(sql, param);

                return res;
            }
        }

        public static async Task<int> UpdateSOStatus(PROD_rcn_krm_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update sales.dbo.sales_so SET STATUS_DO='PERSIAPAN BARANG',isClosed='N',pending='N', Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by,Program_Name=@Program_name  where No_sp = @no_sp";
            param = new DynamicParameters();
            param.Add("@no_sp", data.no_sp);
            param.Add("@Last_updated_by", data.last_updated_by);
            param.Add("@Program_name", data.program_name);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSOStatusDetail(PROD_rcn_krm_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update sales.dbo.sales_so SET STATUS_DO='PERSIAPAN BARANG', Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by where no_sp=@no_sp";
            param = new DynamicParameters();
            param.Add("@no_sp", data.no_sp);
            param.Add("@Last_updated_by", data.last_updated_by);
            param.Add("@program_name", data.program_name);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> Deletercnkirim(PROD_rcn_krm data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "delete prod.dbo.PROD_rcn_krm_m where no_trans=@no_trans";
            param = new DynamicParameters();
            param.Add("@no_trans", data.no_trans);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DeletercnkirimDetail(PROD_rcn_krm data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "delete prod.dbo.PROD_rcn_krm_d where no_trans=@no_trans";
            param = new DynamicParameters();
            param.Add("@no_trans", data.no_trans);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSODStatus(INV_GUDANG_OUT_D data, SqlTransaction trans)
        {
            int res = 0;

            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "update sales.dbo.sales_so_d set STATUS_DO='SURAT JALAN' where no_sp=@no_sp and no_seq=@no_sp_dtl";
            param = new DynamicParameters();
            param.Add("@no_sp", data.no_sp);
            param.Add("@no_sp_dtl", data.no_sp_dtl);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }


        public static async Task<int> UpdateSOKirim(string no_sp, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sp", no_sp);

                return conn.Execute("[PROD].[dbo].[sp_close_kirim]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static List<PROD_rcn_krm> GetNorcnkirimCbo(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT distinct(m.no_trans) as no_trans FROM PROD.dbo.PROD_rcn_krm_m as m " +
                    " inner join PROD.dbo.PROD_rcn_krm_d d on m.no_trans = d.no_trans inner join SIF.dbo.SIF_Customer c on c.Kd_Customer = d.kd_customer " +
                    " where m.inv_stat = 0 and m.kd_cabang = @kd_cabang order by m.no_trans";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                var res = con.Query<PROD_rcn_krm>(sql, param);

                return res.ToList();
            }
        }

        public static async Task<int> InvStat(string no_ref, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update PROD.dbo.PROD_rcn_krm_m set inv_stat = 1 where no_trans=@no_trans";
            param = new DynamicParameters();
            param.Add("@no_trans", no_ref);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static IEnumerable<INV_GUDANG_OUT> GetMonSiapKirim(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT GD.no_trans,GD.no_ref,GD.tgl_trans,RKM.kd_kenek,PG.Nama_Pegawai as supir,RKM.kd_kendaraan,KD.Nama_Kendaraan FROM [INV].[dbo].[INV_GUDANG_OUT] GD" +
                    " inner join[PROD].[dbo].[PROD_rcn_krm_m] RKM on RKM.no_trans = GD.no_ref" +
                    " inner join[SIF].[dbo].[SIF_Pegawai] PG ON PG.Kode_Pegawai = kd_kenek" +
                    " inner join[SIF].[dbo].[S_SIF_Kendaraan] KD ON KD.Kode_Kendaraan = kd_kendaraan";




                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE";
                    }
                    else
                    {
                        filter += " ";
                    }
                    filter += " GD.no_trans LIKE CONCAT('%',@no_trans,'%') ";
                }

                sql += filter;

                sql += " order by GD.tgl_trans desc";


                var res = con.Query<INV_GUDANG_OUT>(sql, param);

                return res;
            }
        }

        public static IEnumerable<INV_GUDANG_OUT_D> GetMonSiapKirimD(string no_trans)
        {
            DynamicParameters param = new DynamicParameters();
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select GDD.no_trans,SOD.Deskripsi as nama_Barang,SOD.no_sp,SOD.Kd_satuan ,RCND.jumlah,GDD.qty_out,CS.Nama_Customer from [INV].[dbo].[INV_GUDANG_OUT_D] GDD " +
                    "INNER JOIN[PROD].[dbo].[PROD_rcn_krm_m] RCN ON RCN.no_trans = GDD.no_ref " +
                    "LEFT OUTER JOIN[PROD].[dbo].[PROD_rcn_krm_d] RCND ON RCN.no_trans = RCND.no_trans AND GDD.no_sp_dtl = RCND.no_sp_dtl AND RCND.no_sp = GDD.no_ref2 " +
                    "LEFT OUTER JOIN[SALES].[dbo].[SALES_SO_D] SOD ON SOD.No_sp = RCND.no_sp and SOD.No_seq = RCND.no_sp_dtl " +
                    "INNER JOIN[SIF].[dbo].[SIF_Customer] CS ON CS.Kd_Customer = RCND.kd_customer";

                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE";
                    }
                    else
                    {
                        filter += " ";
                    }
                    filter += " GDD.no_trans LIKE CONCAT('%',@no_trans,'%') ";
                }

                sql += filter;
                var res = con.Query<INV_GUDANG_OUT_D>(sql, param);



                return res;
            }
        }

        public static INV_GUDANG_OUT GetPrintSiapKirim(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT GD.no_trans,GD.no_ref,GD.tgl_trans,RKM.kd_kenek,PG.Nama_Pegawai as supir,RKM.kd_kendaraan,KD.Nama_Kendaraan FROM [INV].[dbo].[INV_GUDANG_OUT] GD" +
                    " inner join[PROD].[dbo].[PROD_rcn_krm_m] RKM on RKM.no_trans = GD.no_ref" +
                    " inner join[SIF].[dbo].[SIF_Pegawai] PG ON PG.Kode_Pegawai = kd_kenek" +
                    " inner join[SIF].[dbo].[S_SIF_Kendaraan] KD ON KD.Kode_Kendaraan = kd_kendaraan";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                var res = con.Query<INV_GUDANG_OUT>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static async Task<List<CETAK_DPB>> GetCetakDPB(DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            DynamicParameters param = new DynamicParameters();
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "select Case when isnull(v.Status_Inspeksi,'')= '' Then 'Belum Cetak' Else 'Sudah Cetak' end status_cetak, v.*, " +
                //        " ss.Nama_Sales, v.qty_sisa_krm as sisa ,ISNULL(i.nilai, 0) nilai, ISNULL(i.nilai_m3, 0) nilai_m3,'' as no_dpb,  " +
                //        " 0.00 jumlah, 0.00 jml_indeks,0.00 jml_m3, '' AS rec_stat, '' AS program_name, '' AS pending, '' AS pendingh " +
                //        " from PROD.dbo.PRODV_MON_SO v " +
                //        " left " +
                //        " join SIF.dbo.SIF_Barang b on v.Kd_Stok = b.Kode_Barang " +
                //        " left " +
                //        " join SIF.dbo.SIF_Sales ss on v.Kd_Sales = ss.kode_sales " +
                //        " left " +
                //        " join (select *from SIF.dbo.SIF_indeks_barang where jns_kegiatan = '02') i on b.kd_jenis = i.kd_jenis and b.kd_tipe = i.kd_tipe and b.kd_ukuran = i.kd_ukuran  where(Len(v.no_sp) > 5 And v.qty_sisa_krm > 0) " +
                //        " and(v.no_sp not in (select DISTINCT no_sp from prod.dbo.PROD_rcn_krm_d where isnull(jumlah_balik, 0) > 0) OR v.Jenis_sp in('BO PAKET', 'BOOKING ORDER')) and isnull(v.pending,'N') <> 'Y' and isnull(v.isClosed,'N') <> 'Y'  and v.Status_Simpan = 'Y' order by v.No_sp, len(v.No_seq_d)";

                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

                string sql = "SELECT SO.No_sp, Tgl_sp, ss.Nama_Sales, UPPER(jenis_so) as jenis_sp, C.Nama_Customer, B.Nama_Barang, Case when isnull(SOD.Status_Inspeksi,'')= '' Then 'Belum Cetak' Else 'Sudah Cetak' end status_cetak, SOD.Kd_Stok, SOD.Qty -  (ISNULL(SUM(KD.jumlah), 0) + ISNULL(SUM(KD.jumlah_balik), 0) + ISNULL(SUM(KD.jumlah_batal), 0)) as sisa, Status_Inspeksi, SOD.No_seq AS no_seq_d " +
                            " FROM SALES.DBO.SALES_SO SO WITH(NOLOCK) " +
                            " INNER JOIN SALES.DBO.SALES_SO_D SOD WITH(NOLOCK) ON SO.no_sp = SOD.No_sp " +
                            " left join SIF.dbo.SIF_Sales ss WITH(NOLOCK) on SO.Kd_Sales = ss.kode_sales " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_Customer C WITH(NOLOCK) ON SO.Kd_Customer = C.Kd_Customer " +
                            " left join SIF.dbo.SIF_Barang b WITH(NOLOCK) on SOD.Kd_Stok = b.Kode_Barang " +
                            " LEFT OUTER JOIN PROD.DBO.PROD_rcn_krm_d KD WITH(NOLOCK) ON KD.no_sp = SOD.No_sp " +
                            " WHERE SO.Status_Simpan = 'Y' AND  SOD.STATUS_DO NOT IN('BO PAKET', 'BOOKING ORDER') AND ISNULL(SO.isClosed,'N') <> 'Y' ";


                if (DateFrom != null)
                {
                    filter += " AND Tgl_sp >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    filter += " AND Tgl_sp <= @DateTo ";
                }

                sql += filter;

                sql += " GROUP BY SO.No_sp, Tgl_sp, ss.Nama_Sales, jenis_so, C.Nama_Customer, B.Nama_Barang, SOD.Status_Inspeksi, SO.Last_Create_Date, SOD.Kd_Stok, SOD.Qty, SOD.No_seq " +
                            " HAVING(SOD.Qty - (ISNULL(SUM(KD.jumlah), 0) + ISNULL(SUM(KD.jumlah_balik), 0) + ISNULL(SUM(KD.jumlah_batal), 0))) > 0 " +
                            " ORDER BY SO.Last_Create_Date DESC";

                var res = con.Query<CETAK_DPB>(sql, param, null, true, 3600);

                return res.ToList();
            }
        }



    }
}
