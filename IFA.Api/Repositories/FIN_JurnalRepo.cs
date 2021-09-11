using Dapper;
using ERP.Api.Utils;
using ERP.Domain.Base;
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
    public class FIN_JurnalRepo
    {
        public static async Task<int> DelDetail(string nomor, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE [FIN].[dbo].[FIN_JURNAL_D]  " +
                " WHERE no_jur=@nomor;";
            //Query = "UPDATE [SALES].[dbo].[SALES_SO] SET STATUS_DO='BATAL',Last_Update_Date=GETDATE(),Last_Updated_By=@Last_Updated_By  " +
            //    " WHERE No_sp=@No_sp;";
            param = new DynamicParameters();
            param.Add("@nomor", nomor);
            // param.Add("@Last_Updated_By", data.Last_Updated_By);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static Response getCountMonTransaksiHarianPartial(DateTime? DateFrom = null, DateTime? DateTo = null, string filterquery = "", string kd_buku_besar = "", string kd_valuta = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select Count(*) as Result " +
                            "from FIN.dbo.FIN_JURNAL_D B  " +
                            "inner join FIN.dbo.FIN_JURNAL A " +
                            "on  A.no_jur = B.no_jur " +
                            "left join SIF.dbo.SIF_buku_besar C " +
                            "on c.kd_buku_besar = B.kd_buku_besar " +
                            "left join sif.dbo.SIF_TIPE_TRANS d  " +
                            "on d.tipe_trans = a.tipe_trans  " +
                            "left join FIN.dbo.v_kartu vk " +
                            "on vk.kode = B.kartu ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@no_sp", kd_buku_besar);
                param.Add("@kd_customer", kd_valuta);



                filter += " WHERE ";

                filter += "A.tgl_trans >= @DateFrom ";



                filter += " AND ";

                filter += " A.tgl_trans <= @DateTo ";


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

                filter += " AND ";

                //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                filter += " b.kd_buku_besar LIKE CONCAT('%',@no_sp,'%') ";




                filter += " AND ";

                //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                filter += " A.kd_valuta LIKE CONCAT('%',@kd_customer,'%') ";


                sql += filter;
                // sql += " GROUP BY PO.no_po ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public static Response getCountMonTransaksiJurnalPartial(DateTime? DateFrom = null, DateTime? DateTo = null, string filterquery = "", string tipe_trans = "", string kd_valuta = "", int seq = 0, string cek_post = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select Count(*) as Result " +
                            "from FIN.dbo.FIN_JURNAL  ";
                           


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@tipe_trans", tipe_trans);
                param.Add("@kd_valuta", kd_valuta);



                filter += " WHERE ";

                filter += " tgl_posting >= @DateFrom ";



                filter += " AND ";

                filter += " tgl_posting <= @DateTo ";



                filter += " AND ";

                if (tipe_trans == null)
                {
                    filter += " tipe_trans = null ";
                }
                else
                {
                    filter += " tipe_trans LIKE CONCAT('%',@tipe_trans,'%') ";
                }

                if (cek_post == "sudah")
                {
                    filter += " AND ";

                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " isnull(no_posting,'')<>'' ";

                }
                else if (cek_post == "belum")
                {
                    filter += " AND ";

                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " isnull(no_posting,'')='' ";
                }


                filter += " AND ";

                //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                filter += " kd_valuta LIKE CONCAT('%',@kd_valuta,'%') ";


                sql += filter;
                // sql += " GROUP BY PO.no_po ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public async static Task<List<SaldoVM1>> GetMonTransaksiHarianPartial(DateTime? DateFrom = null, DateTime? DateTo = null, string filterquery = "", string sortingquery = "", string kd_buku_besar = "", string kd_valuta = "", int seq = 0)
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
                string sql = "select ROW_NUMBER() OVER (ORDER BY a.no_jur) as 'no',d.tipe_desc,  " +
                                                   "A.no_jur, tgl_trans,A.tgl_posting , A.no_ref1, A.no_ref3 ,  " +
                                                   "case when isnull(A.nama,'') = '' then vk.nama Else  A.nama END nama  , " +
                                                   "B.keterangan, B.kd_buku_besar, C.nm_buku_besar, b.saldo_val_debet, " +
                                                   "b.saldo_val_kredit from FIN.dbo.FIN_JURNAL_D B  " +
                                                   "inner join FIN.dbo.FIN_JURNAL A " +
                                                   "on  A.no_jur = B.no_jur " +
                                                   "left join SIF.dbo.SIF_buku_besar C " +
                                                   "on c.kd_buku_besar = B.kd_buku_besar " +
                                                   "left join sif.dbo.SIF_TIPE_TRANS d  " +
                                                   "on d.tipe_trans = a.tipe_trans  " +
                                                   "left join FIN.dbo.v_kartu vk " +
                                                   "on vk.kode = B.kartu ";

                //" INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po" +
                //" INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON D.kd_stok = B.kode_barang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@no_sp", kd_buku_besar);
                param.Add("@kd_customer", kd_valuta);


               
                    
                        filter += " WHERE ";
                    
                    filter += "A.tgl_trans >= @DateFrom ";
                

                
                        filter += " AND ";
                    
                    filter += " A.tgl_trans <= @DateTo ";
                

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
               
                        filter += " AND ";
                    
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " b.kd_buku_besar LIKE CONCAT('%',@no_sp,'%') ";
                

                
                   
                        filter += " AND ";
                    
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " A.kd_valuta LIKE CONCAT('%',@kd_customer,'%') ";
               


                sql += filter;

                //if (sortingquery != null && sortingquery != "")
                //{
                //    sql += " order by m.tanggal desc, m.no_trans desc ";

                //}
                //else
                //{
                //    sql += " order by m.tanggal desc, m.no_trans desc ";
                //}


                var res = con.Query<SaldoVM1>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public async static Task<List<FIN_JURNAL>> GetMonTransaksiJurnalPartial(DateTime? DateFrom = null, DateTime? DateTo = null, string filterquery = "", string sortingquery = "", string tipe_trans = "", string kd_valuta = "", int seq = 0, string cek_post = null)
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
                string sql = "select ROW_NUMBER() OVER (ORDER BY no_jur) as 'no',a.*,c.nama kartu from FIN.dbo.FIN_JURNAL a left join FIN.dbo.v_kartu c on c.kode = a.kd_kartu";

                //" INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po" +
                //" INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON D.kd_stok = B.kode_barang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@tipe_trans", tipe_trans);
                param.Add("@kd_valuta", kd_valuta);




                filter += " WHERE ";

                filter += " a.tgl_posting >= @DateFrom ";



                filter += " AND ";

                filter += " a.tgl_posting <= @DateTo ";


             
                filter += " AND ";

                if(tipe_trans == null )
                {
                    filter += " a.tipe_trans = null ";
                }
                else
                {
                    filter += " a.tipe_trans LIKE CONCAT('%',@tipe_trans,'%') ";
                }

                //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";

                if (cek_post == "sudah")
                {
                    filter += " AND ";

                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " isnull(a.no_posting,'')<>'' ";

                }
                else if (cek_post == "belum")
                {
                    filter += " AND ";

                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " isnull(a.no_posting,'')='' ";
                }



                    filter += " AND ";

                //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                filter += " a.kd_valuta LIKE CONCAT('%',@kd_valuta,'%') ";



                sql += filter;

                //if (sortingquery != null && sortingquery != "")
                //{
                //    sql += " order by m.tanggal desc, m.no_trans desc ";

                //}
                //else
                //{
                //    sql += " order by m.tanggal desc, m.no_trans desc ";
                //}


                var res = con.Query<FIN_JURNAL>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


        public async static Task<IEnumerable<FIN_JURNAL_D>> GetMonTransaksiJurnalDetail(string no_jur = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select a.*,b.nm_buku_besar nm_buku_besar,c.nm_buku_pusat,d.Nama_Barang from fin.dbo.FIN_JURNAL_D a " +
                             "left join SIF.dbo.SIF_buku_besar b on a.kd_buku_besar = b.kd_buku_besar " +
                             "left join SIF.dbo.SIF_buku_pusat c on c.kd_buku_pusat = a.kd_buku_pusat " +
                             "left join SIF.dbo.SIF_Barang d on d.Kode_Barang = a.kd_obyek ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_jur", no_jur);
               
                if (no_jur != string.Empty && no_jur != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " a.no_jur = @no_jur ";
                }
               

                sql += filter;

                var res = con.Query<FIN_JURNAL_D>(sql, param);

                return res;
            }
        }


        public static async Task<IEnumerable<SaldoVM>> GetSaldo(string kd_rekening = null, string kd_valuta = null, string tahun = null, string bulan = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                // string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH (NOLOCK) ";
                string sql = "Select isnull(SUM(jml_val_debet - jml_val_kredit),0) as Saldo_Awal " +
                            " from FIN.dbo.FIN_NERACA_SALDO " +
                            " where kd_buku_besar = @kd_rekening " +
                            " and kd_valuta = @kd_valuta " +
                            " and thn_buku=  @tahun " +
                            " and bln_buku <= @bulan ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_rekening", kd_rekening);
                param.Add("@kd_valuta", kd_valuta);
                param.Add("@tahun", tahun);
                param.Add("@bulan", bulan);
               

                //if (kd_rekening != string.Empty && id != null)
                //{

                //    filter += " WHERE FJ.Kd_cabang=@kd_cabang and FJ.no_jur  LIKE CONCAT('%',@id,'%')";
                //    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                //}
                //else
                //{
                //    if (DateFrom != null)
                //    {
                //        if (filter == "")
                //        {
                //            filter += " WHERE FJ.tgl_trans >= @DateFrom ";
                //        }
                //        //else
                //        //{
                //        //    filter += " AND ";
                //        //}
                //        // filter += " AND Tgl_sp >= @DateFrom ";
                //    }

                //    if (DateTo != null)
                //    {
                //        if (filter == "")
                //        {
                //            filter += " WHERE  FJ.tgl_trans <= @DateTo";
                //        }
                //        //else
                //        //{
                //        //    filter += " AND ";
                //        //}
                //        filter += "AND FJ.tgl_trans <= @DateTo ";
                //    }

                //}

                ////{
                ////    filter += " AND ";
                ////}
                //if (filter == "")
                //{
                //    filter += " WHERE DATEDIFF(day, FJ.tgl_trans, getdate()) <=7 ";
                //}
                //sql += filter;

                //sql += " ORDER BY FJ.Last_create_date DESC ";


                var res = con.Query<SaldoVM>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_JURNAL>> GetJurnal(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                // string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH (NOLOCK) ";
                string sql = "SELECT C.nama as cabang, TT.tipe_desc,cst.Nama_Customer as kd_kartu, FJ.* " +
" FROM FIN.dbo.FIN_JURNAL FJ WITH(NOLOCK) " +
" left JOIN  SIF.dbo.SIF_Customer cst  WITH(NOLOCK) on cst.Kd_Customer = FJ.kd_kartu " +
" left JOIN SIF.dbo.SIF_TIPE_TRANS TT WITH(NOLOCK) ON FJ.tipe_trans = TT.tipe_trans " +
" left JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON FJ.kd_cabang = C.kd_cabang ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);
                param.Add("@kd_cabang", cb);
                param.Add("@stat", stat);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE FJ.Kd_cabang=@kd_cabang and FJ.no_jur  LIKE CONCAT('%',@id,'%')";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                else
                {
                    if (DateFrom != null)
                    {
                        if (filter == "")
                        {
                            filter += " WHERE FJ.tgl_trans >= @DateFrom ";
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
                            filter += " WHERE  FJ.tgl_trans <= @DateTo";
                        }
                        //else
                        //{
                        //    filter += " AND ";
                        //}
                        filter += "AND FJ.tgl_trans <= @DateTo ";
                    }

                }

                //{
                //    filter += " AND ";
                //}
                if (filter == "")
                {
                    filter += " WHERE DATEDIFF(day, FJ.tgl_trans, getdate()) <=7 ";
                }
                sql += filter;

                sql += " ORDER BY FJ.Last_create_date DESC ";


                var res = con.Query<FIN_JURNAL>(sql, param, null, true, 36000);

                return res;
            }
        }
        public static async Task<IEnumerable<FIN_JURNAL_D>> GetJurnalDTL(string id = null, DateTime? DateFrom = null, DateTime? DateTo = null, string stat = null, string barang = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //     string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL_D WITH (NOLOCK) ";
                string sql = "SELECT B.Nama_Barang AS barang, bb.nm_buku_besar as rekening, D.* " +
                             " FROM FIN.dbo.FIN_JURNAL_D D WITH(NOLOCK) " +
                             " LEFT OUTER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_obyek = B.Kode_Barang " +
                             " LEFT OUTER JOIN SIF.dbo.SIF_buku_besar Bb WITH(NOLOCK) ON D.kd_buku_besar = BB.kd_buku_besar ";
                //" WHERE(no_posting IS NULL OR no_posting = '') AND(verifikasi = 'Y' OR verifikasi IS NULL) " +
                //" ORDER BY FJ.Last_create_date DESC";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);
                param.Add("@stat", stat);
                param.Add("@kd_cabang", cb);

                if (id != string.Empty && id != null)
                {

                    filter += " WHERE D.Kd_cabang=@kd_cabang and D.no_jur LIKE CONCAT('%',@id,'%')  ";
                    //filter += " WHEREKd_cabang @no_po AND S.STATUS_DO<>'BATAL' ";
                }
                //else
                //{
                //    if (DateFrom != null)
                //    {
                //        if (filter == "")
                //        {
                //            filter += " WHERE tgl_trans >= @DateFrom ";
                //        }
                //        //else
                //        //{
                //        //    filter += " AND ";
                //        //}
                //        // filter += " AND Tgl_sp >= @DateFrom ";
                //    }

                //    if (DateTo != null)
                //    {
                //        if (filter == "")
                //        {
                //            filter += " WHERE  tgl_trans <= @DateTo";
                //        }
                //        //else
                //        //{
                //        //    filter += " AND ";
                //        //}
                //        filter += "AND tgl_trans <= @DateTo ";
                //    }

                //}


                //if (filter == "")
                //{
                //    filter += " WHERE DATEDIFF(day, tgl_trans, getdate()) <=7 ";
                //}
                sql += filter;



                sql += " ORDER BY D.Last_Create_Date DESC ";

                var res = con.Query<FIN_JURNAL_D>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<int> Save(FIN_JURNAL data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_JURNAL]  (Kd_cabang,tipe_trans,no_jur,tgl_trans,no_ref1,no_ref2,no_ref3,thnbln,kd_kartu,nama,alamat,kd_valuta,kurs_valuta,jml_val_trans,jml_rp_trans," +
                "no_fakt_pajak,tgl_posting,no_posting,keterangan,rek_attribute1,rek_attribute2,cek_post,verifikasi,Last_create_date,Last_created_by,Program_name,jml_cetak,tgl_cetak) " +
                    "VALUES(@Kd_cabang,@tipe_trans,@no_jur,@tgl_trans,@no_ref1,@no_ref2,@no_ref3,@thnbln,@kd_kartu,@nama,@alamat,@kd_valuta,@kurs_valuta,@jml_val_trans,@jml_rp_trans," +
                    "@no_fakt_pajak,@tgl_posting,@no_posting,@keterangan,@rek_attribute1,@rek_attribute2,@cek_post,@verifikasi,GETDATE(),@Last_created_by,@Program_name,@jml_cetak,@tgl_cetak);";
            param = new DynamicParameters();
            param.Add("@Kd_cabang", data.Kd_cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_jur", data.no_jur);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@no_ref1", data.no_ref1);
            param.Add("@no_ref2", data.no_ref2);
            param.Add("@no_ref3", data.no_ref3);
            param.Add("@thnbln", data.thnbln);
            param.Add("@kd_kartu", data.kd_kartu);
            param.Add("@nama", data.nama);
            param.Add("@alamat", data.alamat);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@jml_val_trans", data.jml_val_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@no_fakt_pajak", data.no_fakt_pajak);
            param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@no_posting", data.no_posting);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rek_attribute1", data.rek_attribute1);
            param.Add("@rek_attribute2", data.rek_attribute2);
            param.Add("@cek_post", data.cek_post);
            param.Add("@tgl_posting", data.tgl_posting);
            param.Add("@no_posting", data.no_posting);
            param.Add("@keterangan", data.keterangan);
            param.Add("@verifikasi", data.verifikasi);
            param.Add("@jml_cetak", data.jml_cetak);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Program_name", data.Program_name);
            param.Add("@tgl_cetak", data.tgl_cetak);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveDetail(FIN_JURNAL_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [FIN].[dbo].[FIN_JURNAL_D]  " +
                " (Kd_Cabang,tipe_trans,no_jur,no_seq,kd_buku_besar,kartu,kd_buku_pusat,tgl_valuta,kd_valuta,kurs_valuta," +
                "saldo_val_debet,saldo_val_kredit,saldo_rp_debet,saldo_rp_kredit,keterangan,status,prev_no_inv,kd_obyek,no_ref1,no_ref2,no_ref3,val_ref1," +
                "harga,tinggi,lebar,panjang,Last_create_date,Last_created_by,Programe_name) " +
                    "VALUES (@Kd_Cabang,@tipe_trans,@no_jur,@no_seq,@kd_buku_besar,@kartu,@kd_buku_pusat,@tgl_valuta,@kd_valuta,@kurs_valuta," +
                    "@saldo_val_debet,@saldo_val_kredit,@saldo_rp_debet,@saldo_rp_kredit,@keterangan,@status,@prev_no_inv,@kd_obyek,@no_ref1,@no_ref2,@no_ref3,@val_ref1," +
                    "@harga,@tinggi,@lebar,@panjang,GETDATE(),@Last_created_by,@Programe_name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_jur", data.no_jur);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kartu", data.kartu);
            param.Add("@kd_buku_pusat", data.kd_buku_pusat);
            param.Add("@tgl_valuta", data.tgl_valuta);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@saldo_val_debet", data.saldo_val_debet);
            param.Add("@saldo_val_kredit", data.saldo_val_kredit);
            param.Add("@saldo_rp_debet", data.saldo_rp_debet);
            param.Add("@saldo_rp_kredit", data.saldo_rp_kredit);
            param.Add("@keterangan", data.keterangan);
            param.Add("@status", data.status);
            param.Add("@prev_no_inv", data.prev_no_inv);
            param.Add("@kd_obyek", data.kd_obyek);
            param.Add("@no_ref1", data.no_ref1);
            param.Add("@no_ref2", data.no_ref2);
            param.Add("@no_ref3", data.no_ref3);
            param.Add("@val_ref1", data.val_ref1);
            param.Add("@harga", data.harga);
            param.Add("@tinggi", data.tinggi);
            param.Add("@lebar", data.lebar);
            param.Add("@panjang", data.panjang);
            param.Add("@Last_created_by", data.Last_created_by);
            param.Add("@Programe_name", data.Programe_name);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<FIN_JURNAL>> GetJurnalPending()
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH(NOLOCK) WHERE (no_posting IS NULL OR no_posting='') AND (verifikasi='Y' OR verifikasi IS NULL)";
                string sql = "SELECT C.nama as cabang, TT.tipe_desc, FJ.* " +
                            " FROM FIN.dbo.FIN_JURNAL FJ WITH(NOLOCK) " +
                            " INNER JOIN SIF.dbo.SIF_TIPE_TRANS TT WITH(NOLOCK) ON FJ.tipe_trans = TT.tipe_trans " +
                            " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON FJ.kd_cabang = C.kd_cabang " +
                            " WHERE(no_posting IS NULL OR no_posting = '') AND(verifikasi = 'Y' OR verifikasi IS NULL) " +
                            " ORDER BY FJ.Last_create_date DESC";
                DynamicParameters param = new DynamicParameters();

                var res = con.Query<FIN_JURNAL>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<string> PostingJurnal(string nojur, DateTime transdate, SqlTransaction trans, SqlConnection conn)
        {
            DynamicParameters _params = new DynamicParameters();
            _params.Add("@vnojur", nojur, dbType: DbType.String, direction: ParameterDirection.Input);
            _params.Add("@vtgl", transdate, dbType: DbType.Date, direction: ParameterDirection.Input);
            _params.Add("@vno_posting", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
            var result = conn.Execute("FIN.dbo.FIN_POSTING", _params, trans, 36000, CommandType.StoredProcedure);
            var retVal = _params.Get<string>("@vno_posting");

            return retVal;
        }
        public static async Task<IEnumerable<FIN_JURNAL_D>> GetJurnalDetailPending(string nojur)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT B.Nama_Barang AS barang, bb.nm_buku_besar as rekening, D.* " +
                            " FROM FIN.dbo.FIN_JURNAL_D D WITH(NOLOCK) " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_obyek = B.Kode_Barang " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_buku_besar Bb WITH(NOLOCK) ON D.kd_buku_besar = BB.kd_buku_besar " +
                            " WHERE no_jur = @nojur" +
                            " ORDER BY D.no_seq";
                DynamicParameters param = new DynamicParameters();
                param.Add("@nojur", nojur);
                var res = con.Query<FIN_JURNAL_D>(sql, param, null, true, 36000);

                return res;
            }
        }

        public async static Task<List<FIN_PIUTANG_USAHA_Header>> GetPiutangUsaha(DateTime tanggal, string filterquery = "", string sortingquery = "", string kd_cust = "", string no_trans = "", string tipe = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                string sql = "SELECT DISTINCT top " + seq + " ROW_NUMBER() OVER (ORDER BY C.Nama_Customer) as 'nomer', A.kd_cust, SUM(A.jml_akhir) as 'sisa', C.nama_customer FROM FIN.dbo.FIN_NOTA A left join SIF.dbo.SIF_Customer C on C.Kd_Customer = A.kd_cust WHERE A.tipe_trans LIKE '%JPJ%' " +
                            " AND A.tgl_posting <= @tanggal " +
                            " AND NOT(A.no_posting IS NULL OR A.no_posting = '') ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@tanggal", tanggal);
                param.Add("@kd_cust", kd_cust);
                param.Add("@no_trans", no_trans);
                param.Add("@tipe", tipe);



                if (filterquery != null && filterquery != "")
                {

                    filter += " AND ";

                    filter += " " + filterquery + " ";
                }

                if(tipe != "" && tipe != null)
                {
                    if (tipe.ToUpper() == "OUTSTANDING")
                    {
                        sql += "AND A.jml_akhir > 0";
                    }
                }

                if (kd_cust != "" && kd_cust != null)
                {
                    sql += "AND kd_cust=@kd_cust ";

                }

                sql += filter;
                sql += " GROUP BY A.kd_cust, C.Nama_Customer ";

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " " + sortingquery + " ";

                }



                var res = con.Query<FIN_PIUTANG_USAHA_Header>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public async static Task<List<FIN_PIUTANG_USAHA_Header>> GetPiutangUsahaCount(DateTime tanggal, string filterquery = "", string sortingquery = "", string kd_cust = "", string no_trans = "", string tipe = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                string sql = "SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY C.Nama_Customer) as 'nomer', A.kd_cust, SUM(A.jml_akhir) as 'sisa', C.nama_customer FROM FIN.dbo.FIN_NOTA A left join SIF.dbo.SIF_Customer C on C.Kd_Customer = A.kd_cust WHERE A.tipe_trans LIKE '%JPJ%' " +
                            " AND A.tgl_posting <= @tanggal " +
                            " AND NOT(A.no_posting IS NULL OR A.no_posting = '') ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@tanggal", tanggal);
                param.Add("@kd_cust", kd_cust);
                param.Add("@no_trans", no_trans);
                param.Add("@tipe", tipe);

                if (filterquery != null && filterquery != "")
                {

                    filter += " AND ";

                    filter += " " + filterquery + " ";
                }

                if (tipe != "" && tipe != null)
                {
                    if (tipe.ToUpper() == "OUTSTANDING")
                    {
                        sql += "AND A.jml_akhir > 0";
                    }
                }

                if (kd_cust != "" && kd_cust != null)
                {
                    sql += "AND kd_cust=@kd_cust ";

                }
                sql += filter;
                sql += " GROUP BY A.kd_cust, C.Nama_Customer ";

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " " + sortingquery + " ";

                }



                var res = con.Query<FIN_PIUTANG_USAHA_Header>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


        public async static Task<List<FIN_HUTANG_USAHA_Header>> GetHutangUsaha(DateTime tanggal, string filterquery = "", string sortingquery = "", string kd_cust = "", string no_trans = "", string tipe = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                //string sql = "SELECT DISTINCT top " + seq + " ROW_NUMBER() OVER (ORDER BY C.Nama_Customer) as 'nomer', A.kd_cust, SUM(A.jml_akhir) as 'sisa', C.nama_customer FROM FIN.dbo.FIN_NOTA A left join SIF.dbo.SIF_Customer C on C.Kd_Customer = A.kd_cust WHERE A.tipe_trans LIKE '%JPJ%' " +
                //            " AND A.tgl_posting <= @tanggal " +
                //            " AND NOT(A.no_posting IS NULL OR A.no_posting = '') ";
                string sql = "SELECT DISTINCT top " + seq + " ROW_NUMBER() OVER (ORDER BY C.Nama_Supplier) as 'nomer',  a.kd_supplier,c.Nama_Supplier as nama_supplier,sum(a.jml_akhir) as 'sisa'     " +
                    " FROM FIN.dbo.FIN_PEMBELIAN A, SIF.dbo.SIF_Supplier C     " +
                    " WHERE  a.kd_supplier = c.Kode_Supplier      and a.tipe_trans like 'JPP%'     and not (a.no_posting is null or a.no_posting = '')       ";


                DynamicParameters param = new DynamicParameters();
                param.Add("@tanggal", tanggal);
                param.Add("@kd_cust", kd_cust);
                param.Add("@no_trans", no_trans);
                param.Add("@tipe", tipe);



                if (filterquery != null && filterquery != "")
                {

                    filter += " AND ";

                    filter += " " + filterquery + " ";
                }

                if (tipe != "" && tipe != null)
                {
                    if (tipe.ToUpper() == "OUTSTANDING")
                    {
                        sql += "AND A.jml_akhir > 0";
                    }
                }

                if (kd_cust != "" && kd_cust != null)
                {
                    sql += " AND a.kd_supplier=@kd_cust ";

                }

                sql += filter;
                sql += " group by a.kd_supplier  , c.Nama_Supplier ";

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " " + sortingquery + " ";

                }



                var res = con.Query<FIN_HUTANG_USAHA_Header>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public async static Task<List<FIN_HUTANG_USAHA_Header>> GetHutangUsahaCount(DateTime tanggal, string filterquery = "", string sortingquery = "", string kd_cust = "", string no_trans = "", string tipe = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                //string sql = "SELECT DISTINCT ROW_NUMBER() OVER (ORDER BY C.Nama_Customer) as 'nomer', A.kd_cust, SUM(A.jml_akhir) as 'sisa', C.nama_customer FROM FIN.dbo.FIN_NOTA A left join SIF.dbo.SIF_Customer C on C.Kd_Customer = A.kd_cust WHERE A.tipe_trans LIKE '%JPJ%' " +
                //            " AND A.tgl_posting <= @tanggal " +
                //            " AND NOT(A.no_posting IS NULL OR A.no_posting = '') ";

                string sql = "SELECT ROW_NUMBER() OVER (ORDER BY C.Nama_Supplier) as 'nomer',  a.kd_supplier,c.Nama_Supplier as nama_supplier,sum(a.jml_akhir) as 'sisa'     " +
                 " FROM FIN.dbo.FIN_PEMBELIAN A, SIF.dbo.SIF_Supplier C     " +
                 " WHERE  a.kd_supplier = c.Kode_Supplier      and a.tipe_trans like 'JPP%'     and not (a.no_posting is null or a.no_posting = '')       ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@tanggal", tanggal);
                param.Add("@kd_cust", kd_cust);
                param.Add("@no_trans", no_trans);
                param.Add("@tipe", tipe);

                if (filterquery != null && filterquery != "")
                {

                    filter += " AND ";

                    filter += " " + filterquery + " ";
                }

                if (tipe != "" && tipe != null)
                {
                    if (tipe.ToUpper() == "OUTSTANDING")
                    {
                        sql += "AND A.jml_akhir > 0";
                    }
                }

                if (kd_cust != "" && kd_cust != null)
                {
                    sql += " AND a.kd_supplier=@kd_cust ";

                }
                sql += filter;
                sql += " group by a.kd_supplier  , c.Nama_Supplier ";

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " " + sortingquery + " ";

                }



                var res = con.Query<FIN_HUTANG_USAHA_Header>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }


        public static Response getCountPO(DateTime DateFrom, DateTime DateTo, string filterquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result " +
                   " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                     " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Nama_Barang", barang);


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
                    filter += " PO.tgl_po >= @DateFrom ";
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
                    filter += " PO.tgl_po <= @DateTo ";
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
                    filterquery = filterquery.Replace("status_po", "PO.status_po");
                    filterquery = filterquery.Replace("no_po", "PO.no_po");
                    filterquery = filterquery.Replace("tgl_po", "PO.tgl_po");
                    filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    filterquery = filterquery.Replace("jml_rp_trans", "PO.jml_rp_trans");
                    filterquery = filterquery.Replace("total", "PO.total");
                    filter += filterquery;
                }
                if (barang != null && barang != "")
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " PO.no_po IN ( SELECT no_po FROM  " +
                           " PURC.DBO.PURC_PO_D D WITH(NOLOCK) " +
                           " INNER JOIN SIF.DBO.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.kode_barang " +
                           " WHERE Nama_Barang LIKE  CONCAT('%',@Nama_Barang,'%')) ";
                }


                sql += filter;
                // sql += " GROUP BY PO.no_po ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public static async Task<IEnumerable<FIN_PIUTANG_USAHA_Detail>> GetPiutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH(NOLOCK) WHERE (no_posting IS NULL OR no_posting='') AND (verifikasi='Y' OR verifikasi IS NULL)";
                DynamicParameters param = new DynamicParameters();

                string sql = "SELECT case when flag_ppn ='Y' then 'PPN' else 'NON PPN' END as sts_ppn, no_inv, tgl_inv, no_jurnal, tgl_posting, keterangan, jml_tagihan, " +
                    " jml_bayar, jml_akhir, kd_cust,jml_bayar_pending, tgl_jth_tempo " +
                    " FROM FIN.dbo.FIN_NOTA ";

                if(no_trans !=null && no_trans != "")
                {
                    sql += " WHERE tipe_trans LIKE '%JPJ%' AND no_inv=@no_inv and NOT (no_posting IS NULL OR no_posting = '')";
                    param.Add("@no_inv", no_trans);

                }
                else
                {
                    sql += " WHERE tipe_trans LIKE '%JPJ%' AND kd_cust=@kd_cust " +
                    " AND tgl_posting <=  @tanggal AND NOT (no_posting IS NULL OR no_posting = '')";
                    param.Add("@kd_cust", kd_cust);
                    param.Add("@tanggal", tanggal);
                }
              

                var res = con.Query<FIN_PIUTANG_USAHA_Detail>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<FIN_HUTANG_USAHA_Detail>> GetHutangUsahaDetail(string kd_cust, DateTime tanggal, string no_trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH(NOLOCK) WHERE (no_posting IS NULL OR no_posting='') AND (verifikasi='Y' OR verifikasi IS NULL)";
                DynamicParameters param = new DynamicParameters();
                param.Add("@tanggal", tanggal);

                //string sql = "SELECT case when flag_ppn ='Y' then 'PPN' else 'NON PPN' END as sts_ppn, no_inv, tgl_inv, no_jurnal, tgl_posting, keterangan, jml_tagihan, " +
                //    " jml_bayar, jml_akhir, kd_cust,jml_bayar_pending, tgl_jth_tempo " +
                //    " FROM FIN.dbo.FIN_NOTA ";

                string sql = "select  ROW_NUMBER() OVER (ORDER BY kd_supplier) as 'nomer',     case when a.flag_ppn ='Y' then 'PPN' else 'NON PPN' END as sts_ppn, a.tipe_trans,  " +
                    " a.kd_supplier,a.no_inv,a.no_ref1,a.tgl_inv ,a.tgl_jth_tempo ,     a.no_jurnal,c.Nama_Supplier,a.nm_supplier ,a.kd_buku_besar,     d.nm_buku_besar,a.keterangan ,  " +
                    " a.no_posting, a.tgl_posting,     a.jml_val_ppn, a.jml_rp_ppn, a.jml_diskon, a.jml_tagihan,     a.jml_bayar,isnull(a.jml_bayar_pending,0) jml_bayar_pending ," +
                    " a.jml_akhir  ,E.nomor as 'nomor_giro'    " +
                    " from SIF.dbo.SIF_Supplier C , SIF.dbo.SIF_buku_besar D,  FIN.dbo.FIN_PEMBELIAN A " +
                    " left outer join FIN.dbo.FIN_GIRO_D E on (A.no_inv = E.no_inv)    " +
                    " where  a.kd_supplier = c.Kode_Supplier      and a.kd_buku_besar = d.kd_buku_besar     and a.tipe_trans like 'JPP%'       and not(a.no_posting is null) AND tgl_posting <=  @tanggal ";

                if (no_trans != null && no_trans != "")
                {
                    sql += " AND no_inv=@no_inv ";
                    param.Add("@no_inv", no_trans);

                }

                if (kd_cust != null && kd_cust != "")
                {
                    sql += " AND kd_supplier=@kd_cust ";
                    param.Add("@kd_cust", kd_cust);

                }


                var res = con.Query<FIN_HUTANG_USAHA_Detail>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<FIN_PIUTANG_USAHA_Penjualan>> GetPenjualan(string no_inv)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH(NOLOCK) WHERE (no_posting IS NULL OR no_posting='') AND (verifikasi='Y' OR verifikasi IS NULL)";
                string sql = "select a.no_inv,a.no_sp,a.kd_stok,b.Nama_Barang,a.Qty,a.harga,CASE WHEN N.flag_ppn='Y' THEN (a.harga * a.Qty) + jml_ppn ELSE (a.harga * a.Qty) END AS  total," +
                            " CASE WHEN N.flag_ppn = 'Y' THEN jml_ppn ELSE 0 END AS  jml_ppn " +
                            " from fin.dbo.FIN_NOTA_D a " +
                            " inner join fin.dbo.FIN_NOTA n  on a.no_inv=n.no_inv " +
                            " left join sif.dbo.SIF_BARANG b on b.Kode_Barang = a.kd_stok where a.no_inv=@no_inv";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_inv", no_inv);
                var res = con.Query<FIN_PIUTANG_USAHA_Penjualan>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<IEnumerable<FIN_HUTANG_USAHA_Penjualan>> GetPembelian(string no_inv)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "SELECT * FROM FIN.dbo.FIN_JURNAL WITH(NOLOCK) WHERE (no_posting IS NULL OR no_posting='') AND (verifikasi='Y' OR verifikasi IS NULL)";
                string sql = "SELECT P.no_inv, p.no_ref1 as no_sp, kd_stok, b.Nama_Barang as nama_barang, qty, harga, " +
                               " case when flag_ppn = 'Y' THEN D.jml_rp_trans * 0.1 ELSE 0 END jml_ppn, " +
                               " case when flag_ppn = 'Y' THEN d.jml_rp_trans + (D.jml_rp_trans * 0.1) ELSE d.jml_rp_trans END AS total " +
                               " FROM FIN.dbo.FIN_PEMBELIAN P WITH(NOLOCK) " +
                               " INNER JOIN FIN.dbo.FIN_PEMBELIAN_D D WITH(NOLOCK) ON P.no_inv = D.no_inv " +
                               " INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                               " WHERE P.no_inv = @no_inv";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_inv", no_inv);
                var res = con.Query<FIN_HUTANG_USAHA_Penjualan>(sql, param, null, true, 36000);

                return res;
            }
        }

    }
}
