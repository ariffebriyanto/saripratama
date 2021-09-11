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
    public class PURC_PORepo
    {
        public async static Task<IEnumerable<PURC_PO>> GetPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT DISTINCT PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                    " PO.tgl_kirim,tgl_jth_tempo,  qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,PO.prosen_diskon, " +
                    " PO.jml_diskon,PO.keterangan,PO.rec_stat,lama_bayar,  tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve, " +
                    " tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,   " +
                    " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,PO.total,ongkir, " +
                    " isClosed, S.Nama_Supplier,PO.atas_nama,PO.kop_surat,  STUFF( " +
                     " (SELECT ',' + Nama_Barang " +
                     "  FROM PURC.DBO.PURC_PO_D t1 " +
                     "  INNER JOIN SIF.dbo.SIF_Barang B ON B.Kode_Barang = T1.kd_stok " +
                     "  WHERE t1.no_po = D.no_po " +
                     "  FOR XML PATH('')) " +
                     " , 1, 1, '') stuffbarang  " +
                    " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                    " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier " +
                    " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po ";
                //string sql = "SELECT DISTINCT PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                //   " PO.tgl_kirim,tgl_jth_tempo,  qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,PO.prosen_diskon, " +
                //   " PO.jml_diskon,PO.keterangan,PO.rec_stat,lama_bayar,  tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve, " +
                //   " tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,   " +
                //   " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,PO.total,ongkir, " +
                //   " isClosed, S.Nama_Supplier,PO.atas_nama,PO.kop_surat  " +
                //   " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                //   " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier ";

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
                    filter += " PO.no_po LIKE CONCAT('%',@no_po,'%') ";
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

                //if (status_po != string.Empty && status_po != null)
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    filter += " status_po=@status_po ";
                //}

                sql += filter;

                sql += " ORDER BY PO.Last_Create_Date DESC ";


                var res = con.Query<PURC_PO>(sql, param, null, true, 36000);

                 return res;
            }
        }
        public async static Task<List<PURC_PO>> GetPOPartial (DateTime DateFrom, DateTime DateTo, string filterquery ="", string sortingquery="", string barang="", int seq=0)
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
                string sql = "SELECT DISTINCT TOP " + seq + " PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                   " PO.tgl_kirim,tgl_jth_tempo,  qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,PO.prosen_diskon, " +
                   " PO.jml_diskon,PO.keterangan,PO.rec_stat,lama_bayar,  tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve, " +
                   " tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,   " +
                   " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,PO.total,ongkir, " +
                   " isClosed, S.Nama_Supplier,PO.atas_nama,PO.kop_surat  " +
                   " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                   " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier";
                   //" INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = PO.no_po" +
                   //" INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON D.kd_stok = B.kode_barang";


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
                    filter += " " + filterquery + " ";
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
                    //filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                    filter += " PO.no_po IN ( SELECT no_po FROM  "+
                            " PURC.DBO.PURC_PO_D D WITH(NOLOCK) " +
                            " INNER JOIN SIF.DBO.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.kode_barang " +
                            " WHERE Nama_Barang LIKE  CONCAT('%',@Nama_Barang,'%')) ";
                }


                sql += filter;

                if(sortingquery !=null && sortingquery != "")
                {
                    sortingquery = sortingquery.Replace("status_po", "PO.status_po");
                    sortingquery = sortingquery.Replace("no_po", "PO.no_po");
                    sortingquery = sortingquery.Replace("tgl_po", "PO.tgl_po");
                    sortingquery = sortingquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    sortingquery = sortingquery.Replace("jml_rp_trans", "PO.jml_rp_trans");
                    sortingquery = sortingquery.Replace("total", "PO.total");
                    sql += " " + sortingquery + " ";
                   
                }
                else
                {
                    sql += " ORDER BY PO.Last_Create_Date DESC ";
                }


                var res = con.Query<PURC_PO>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public async static Task<List<PURC_RETUR_BELI>> GetReturListPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT DISTINCT TOP " + seq + "  R.*, Nama_Supplier " +
                            " FROM [PURC].[dbo].[PURC_RETUR_BELI] R WITH(NOLOCK) " +
                            " INNER JOIN SIF.DBO.SIF_SUPPLIER S WITH(NOLOCK) ON S.Kode_Supplier = R.kd_supplier " +
                            " INNER JOIN [PURC].[dbo].[PURC_RETUR_BELI_D] RD WITH(NOLOCK) ON R.no_retur=RD.no_retur " +
                            " INNER JOIN SIF.DBO.SIF_BARANG B WITH(NOLOCK) ON RD.kd_stok=B.Kode_Barang ";

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
                    filter += " R.tanggal >= @DateFrom ";
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
                    filter += " R.tanggal <= @DateTo ";
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
                    filterquery = filterquery.Replace("no_retur", "R.no_retur");
                    filterquery = filterquery.Replace("no_po", "R.no_po");
                    filterquery = filterquery.Replace("tanggal", "R.tanggal");
                    filter += " " + filterquery + " ";
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
                    filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                }


                sql += filter;

                if (sortingquery != null && sortingquery != "")
                {
                    sortingquery = sortingquery.Replace("no_retur", "R.no_retur");
                    sortingquery = sortingquery.Replace("no_po", "R.no_po");
                    sortingquery = sortingquery.Replace("tanggal", "R.tanggal");
                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY R.Last_Create_Date DESC";
                }

                var res = con.Query<PURC_RETUR_BELI>(sql, param);

                return res.ToList();
            }
        }

        public static Response GetCountReturListPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT Count(*) as Result " +
                            " FROM [PURC].[dbo].[PURC_RETUR_BELI] R WITH(NOLOCK) " +
                            " INNER JOIN SIF.DBO.SIF_SUPPLIER S WITH(NOLOCK) ON S.Kode_Supplier = R.kd_supplier " +
                            " INNER JOIN [PURC].[dbo].[PURC_RETUR_BELI_D] RD WITH(NOLOCK) ON R.no_retur=RD.no_retur " +
                            " INNER JOIN SIF.DBO.SIF_BARANG B WITH(NOLOCK) ON RD.kd_stok=B.Kode_Barang ";

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
                    filter += " R.tanggal >= @DateFrom ";
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
                    filter += " R.tanggal <= @DateTo ";
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
                    filterquery = filterquery.Replace("no_retur", "R.no_retur");
                    filterquery = filterquery.Replace("no_po", "R.no_po");
                    filterquery = filterquery.Replace("tanggal", "R.tanggal");
                    filter += " " + filterquery + " ";
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
                    filter += " B.Nama_Barang LIKE CONCAT('%',@Nama_Barang,'%') ";
                }


                sql += filter;

                var res = con.Query<Response>(sql, param);

                return res.FirstOrDefault();
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


        public async static Task<IEnumerable<v_last_purc>> GetPOLast(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select * from PURC.dbo.v_last_purc ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@barang", barang);
               
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
                    filter += " Tgl_Po >= @DateFrom ";
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
                    filter += " Tgl_Po <= @DateTo ";
                }

             

                sql += filter;

                sql += " ORDER BY Tgl_Po DESC ";


                var res = con.Query<v_last_purc>(sql, param, null, true, 36000);

                return res;
            }
        }

        public async static Task<IEnumerable<v_last_purc>> GetPOLastPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select  TOP " + seq + " * from PURC.dbo.v_last_purc ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);

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
                    filter += " Tgl_Po >= @DateFrom ";
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
                    filter += " Tgl_Po <= @DateTo ";
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
                    filter += " " + filterquery + " ";
                }
                sql += filter;

                if (sortingquery != null && sortingquery != "")
                {
                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY Tgl_Po DESC ";
                }


                var res = con.Query<v_last_purc>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static Response GetCountPOLastPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select COUNT(*) as Result from PURC.dbo.v_last_purc ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@barang", barang);

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
                    filter += " Tgl_Po >= @DateFrom ";
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
                    filter += " Tgl_Po <= @DateTo ";
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
                    filter += " " + filterquery + " ";
                }
                sql += filter;

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.FirstOrDefault();
            }
        }

        public async static Task<IEnumerable<PURC_PO>> GetPOMobile(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string barang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT DISTINCT PO.Kd_Cabang,PO.tipe_trans,PO.no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta, " +
                    " PO.tgl_kirim,tgl_jth_tempo,  qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,PO.prosen_diskon, " +
                    " PO.jml_diskon,PO.keterangan,PO.rec_stat,lama_bayar,  tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve, " +
                    " tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,   " +
                    " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,PO.total,ongkir, " +
                    " isClosed, S.Nama_Supplier,PO.atas_nama,PO.kop_surat  " +
                    " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                    " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier " +
                    " INNER JOIN PURC.DBO.PURC_PO_D D ON D.no_po = PO.no_po ";

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
                    filter += " REPLACE(PO.no_po, '/', '')  =@no_po ";
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

                //if (status_po != string.Empty && status_po != null)
                //{
                //    if (filter == "")
                //    {
                //        filter += " WHERE ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    filter += " status_po=@status_po ";
                //}

                sql += filter;

                sql += " ORDER BY PO.Last_Create_Date DESC ";


                var res = con.Query<PURC_PO>(sql, param, null, true, 36000);

                return res;
            }
        }

        public static async Task<int> SavePO(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PURC].[dbo].[PURC_PO]  (Kd_Cabang,tipe_trans,no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta,tgl_kirim,tgl_jth_tempo,qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,prosen_diskon,jml_diskon,keterangan,rec_stat,lama_bayar,tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve,tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,Last_Create_Date,Last_Created_By,Program_Name,status_po,flag_tagih,total,ongkir,isClosed, atas_nama,kop_surat) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_po,@tgl_po,@no_pr,@no_ref,@no_qc,@kd_supplier,@kd_valuta,@kurs_valuta,@tgl_kirim,@tgl_jth_tempo,@qty_total, " +
                    " @jml_val_trans,@jml_rp_trans,@flag_diskon,@flag_ppn,@jml_ppn,@prosen_diskon,@jml_diskon,@keterangan,@rec_stat,@lama_bayar, " +
                    " @tgl_bayar,@term_bayar,@tgl_approve,@user_approve,@ket_approve,@tgl_batal,@ket_batal,@tgl_cetak,@jml_cetak,@sts_ctk_ulang,@Last_Create_Date, " +
                    " @Last_Created_By,@Program_Name,@status_po,@flag_tagih,@total,@ongkir,@isClosed,@atas_nama,@kop_surat);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_po", data.no_po);
            param.Add("@tgl_po", data.tgl_po);
            param.Add("@no_pr", data.no_pr);
            param.Add("@no_ref", data.no_ref);
            param.Add("@no_qc", data.no_qc);
            param.Add("@kd_supplier", data.kd_supplier);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@tgl_kirim", data.tgl_kirim);
            param.Add("@tgl_jth_tempo", data.tgl_jth_tempo);
            param.Add("@qty_total", data.qty_total);
            param.Add("@jml_val_trans", data.jml_val_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@flag_diskon", data.flag_diskon);
            param.Add("@flag_ppn", data.flag_ppn);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@prosen_diskon", data.prosen_diskon);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@lama_bayar", data.lama_bayar);
            param.Add("@tgl_bayar", data.tgl_bayar);
            param.Add("@term_bayar", data.term_bayar);
            param.Add("@tgl_approve", data.tgl_approve);
            param.Add("@user_approve", data.user_approve);
            param.Add("@ket_approve", data.ket_approve);
            param.Add("@tgl_batal", data.tgl_batal);
            param.Add("@ket_batal", data.ket_batal);
            param.Add("@tgl_cetak", data.tgl_cetak);
            param.Add("@jml_cetak", data.jml_cetak);
            param.Add("@sts_ctk_ulang", data.sts_ctk_ulang);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@status_po", data.status_po);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@flag_tagih", data.flag_tagih);
            param.Add("@total", data.total);
            param.Add("@ongkir", data.ongkir);
            param.Add("@isClosed", data.isClosed);
            param.Add("@atas_nama", data.atas_nama);
            param.Add("@kop_surat", data.kop_surat);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdatePO(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "UPDATE [PURC].[dbo].[PURC_PO]  SET Kd_Cabang=@Kd_Cabang,tipe_trans=@tipe_trans,tgl_po=@tgl_po,no_ref=@no_ref,kd_supplier=@kd_supplier,kd_valuta=@kd_valuta, kurs_valuta=@kurs_valuta,tgl_jth_tempo=@tgl_jth_tempo,qty_total=@qty_total,jml_val_trans=@jml_val_trans, " +
                    " jml_rp_trans= @jml_rp_trans,flag_ppn= @flag_ppn,jml_ppn= @jml_ppn,keterangan= @keterangan,rec_stat= @rec_stat,lama_bayar= @lama_bayar,tgl_bayar= @tgl_bayar,term_bayar= @term_bayar,Last_Update_Date= @Last_Update_Date, " +
                    " Last_Updated_By= @Last_Updated_By,Program_Name= @Program_Name,total= @total,ongkir= @ongkir,isClosed= @isClosed, status_po=@status_po, user_approve=@user_approve, tgl_approve=@tgl_approve, ket_batal=@ket_batal " +
                    " WHERE no_po= @no_po";

            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_po", data.no_po);
            param.Add("@tgl_po", data.tgl_po);
            param.Add("@no_ref", data.no_ref);
            param.Add("@kd_supplier", data.kd_supplier);
            param.Add("@kd_valuta", data.kd_valuta);
            param.Add("@kurs_valuta", data.kurs_valuta);
            param.Add("@tgl_jth_tempo", data.tgl_jth_tempo);
            param.Add("@qty_total", data.qty_total);
            param.Add("@jml_val_trans", data.jml_val_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@flag_ppn", data.flag_ppn);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@lama_bayar", data.lama_bayar);
            param.Add("@tgl_bayar", data.tgl_bayar);
            param.Add("@term_bayar", data.term_bayar);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@total", data.total);
            param.Add("@ongkir", data.ongkir);
            param.Add("@isClosed", data.isClosed);
            param.Add("@status_po", data.status_po);
            param.Add("@user_approve", data.user_approve);
            param.Add("@tgl_approve", data.tgl_approve);
            param.Add("@ket_batal", data.ket_batal);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static async Task<string> GenPONumber(string no_po = null, DateTime? DateFrom = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@vkd_bukti", no_po, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@vtgl_trans", DateFrom, dbType: DbType.Date, direction: ParameterDirection.Input);
                _params.Add("@vno_trans", dbType: DbType.String, direction: ParameterDirection.Output, size: 4000);
                var result = con.Execute("[dbo].[sifp_get_no_trans]", _params, null, null, CommandType.StoredProcedure);
                var retVal = _params.Get<string>("@vno_trans");


               // var res = con.Query<PURC_PO>(sql, param);

                return retVal;
            }
        }

        public async static Task<IEnumerable<PURC_PO>> GetApprovalPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT PO.Kd_Cabang,tipe_trans,no_po,tgl_po,no_pr,no_ref,no_qc,kd_supplier,kd_valuta,kurs_valuta,tgl_kirim,tgl_jth_tempo,atas_nama ,kop_surat, " +
                    " qty_total,jml_val_trans,jml_rp_trans,flag_diskon,flag_ppn,jml_ppn,prosen_diskon,jml_diskon,PO.keterangan,CASE WHEN PO.rec_stat='ENTRY' THEN '' ELSE  PO.rec_stat END rec_stat ,lama_bayar, " +
                    " tgl_bayar,term_bayar,tgl_approve,user_approve,ket_approve,tgl_batal,ket_batal,tgl_cetak,jml_cetak,sts_ctk_ulang,PO.Last_Create_Date,  " +
                    " PO.Last_Created_By,PO.Last_Update_Date,PO.Last_Updated_By,PO.Program_Name,status_po,flag_tagih,total,ongkir,isClosed, S.Nama_Supplier , REPLACE(no_po, '/', '')   as param_po  " +
                    " FROM PURC.DBO.PURC_PO PO WITH(NOLOCK) " +
                    " INNER JOIN SIF.DBO.SIF_Supplier S WITH(NOLOCK) ON S.Kode_Supplier = PO.kd_supplier";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
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
                    filter += " no_po LIKE CONCAT('%',@no_po,'%') ";
                }
                if (status_po != string.Empty && status_po != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " PO.rec_stat =@status_po AND ISNULL(PO.status_po,'') <> 'BATAL' ";
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
                    filter += " tgl_po >= @DateFrom ";
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
                    filter += " tgl_po <= @DateTo ";
                }

              
                sql += filter;

                sql += " ORDER BY PO.Last_Create_Date DESC, CASE WHEN PO.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC, no_po DESC ";


                var res = con.Query<PURC_PO>(sql, param);

                return res;

               
            }
        }

        public async static Task<PrintPO> GetPrintPO(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT C.alamat AS AlamatCabang,case when isnull(PO.kop_surat,'')= '' then C.nama ELSE PO.kop_surat END AS Cabang, C.telp1 as Telp1, C.kota +'-'+ C.propinsi as Kota, PO.no_po as PUNumber, " +
                            " cast(datepart(dd, PO.tgl_po) as char(2)) + ' ' + datename(mm, PO.tgl_po) + ' ' + cast(datepart(yyyy, PO.tgl_po) as char(4)) AS TanggalPO, " +
                            "  PO.status_po AS StatusPO, po.keterangan AS AlamatPengiriman, " +
                            " cast(datepart(dd, PO.tgl_jth_tempo) as char(2)) + ' ' + datename(mm, PO.tgl_jth_tempo) + ' ' + cast(datepart(yyyy, PO.tgl_jth_tempo) as char(4)) AS TanggalJatuhTempo, " +
                            "  cast(datepart(dd, PO.tgl_kirim) as char(2)) +' ' + datename(mm, PO.tgl_kirim) + ' ' + cast(datepart(yyyy, PO.tgl_kirim) as char(4)) AS TanggalKirim, S.Nama_Supplier AS NamaSupplier, S.Alamat1 AS AlamatSupplier, S.No_Telepon1 AS TelpSupplier, P.Nama_Pegawai AS RequestBy, PA.Nama_Pegawai AS ApprovedBy, PO.term_bayar AS Notes, PO.jml_ppn as PPN, PO.ongkir AS Ongkir, PO.jml_rp_trans as SubTotal, total as GrandTotal " +
                            " FROM PURC.dbo.PURC_PO PO " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_cabang C  WITH (NOLOCK) ON PO.Kd_Cabang = C.kd_cabang " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_Supplier S WITH (NOLOCK)  ON PO.kd_supplier = S.Kode_Supplier " +
                            " LEFT OUTER JOIN SIF.dbo.MUSER US WITH (NOLOCK)  ON PO.Last_Created_By = US.userid " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_Pegawai P WITH (NOLOCK)  ON US.nama = P.Kode_Pegawai " +
                            " LEFT OUTER JOIN SIF.dbo.MUSER USA WITH (NOLOCK)  ON PO.user_approve = USA.userid " +
                            " LEFT OUTER JOIN SIF.dbo.SIF_Pegawai PA WITH (NOLOCK)  ON USA.nama = PA.Kode_Pegawai " +
                            " WHERE po.no_po = @no_po ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);

                var res = con.Query<PrintPO>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static async Task<int> SaveApprovalPO(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [PURC].[dbo].[PURC_PO]  SET rec_stat = @rec_stat,user_approve=@user_approve,tgl_approve=@tgl_approve,keterangan=@keterangan,ket_batal=@ket_batal WHERE no_po = @no_po";
            param = new DynamicParameters();

            param.Add("@no_po", data.no_po);
            param.Add("@ket_batal", data.ket_batal);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@user_approve", data.user_approve);
            param.Add("@tgl_approve", data.tgl_approve);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SPLast_Price(PURC_PO data, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", data.no_po);

                return conn.Execute("PURC.dbo.Update_last_Price", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SaveReturPO(PURC_RETUR_BELI data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [PURC].[dbo].[PURC_RETUR_BELI]  ( " +
                " Kd_Cabang, no_retur, tanggal, no_po, no_ref, no_ref1, kd_supplier, keterangan, jml_rp_trans, " +
                " rec_stat, Last_Create_Date, Last_Created_By, Last_Update_Date, Last_Updated_By, " +
                " Program_Name, jumlah_cetak,  flag_ppn, Bonus)" +
                " VALUES(@Kd_Cabang,@no_retur,@tanggal,@no_po,@no_ref,@no_ref1,@kd_supplier,@keterangan,@jml_rp_trans,@rec_stat,@Last_Create_Date,@Last_Created_By,@Last_Update_Date,@Last_Updated_By,@Program_Name,@jumlah_cetak,@flag_ppn,@Bonus); ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@no_retur", data.no_retur);
            param.Add("@tanggal", data.tanggal);
            param.Add("@no_po", data.no_po);
            param.Add("@no_ref", data.no_ref);
            param.Add("@no_ref1", data.no_ref1);
            param.Add("@kd_supplier", data.kd_supplier);
            param.Add("@keterangan", data.keterangan);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@rec_stat", data.rec_stat);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@jumlah_cetak", data.jumlah_cetak);
            param.Add("@flag_ppn", data.flag_ppn);
            param.Add("@Bonus", data.Bonus);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

    }

    
}
