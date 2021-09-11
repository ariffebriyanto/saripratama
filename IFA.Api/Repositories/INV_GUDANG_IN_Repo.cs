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
    public class INV_GUDANG_IN_Repo
    {
        public static async Task<IEnumerable<INV_GUDANG_IN>> GetGudangIn(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string kd_cabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat," +
                             " G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,S.Nama_Supplier,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa" +
                             " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK)" +
                             " INNER JOIN PURC.DBO.PURC_PO PO WITH (NOLOCK) on G.no_ref = PO.no_po" +
                             " LEFT OUTER JOIN SIF.DBO.SIF_Supplier S WITH (NOLOCK) ON S.Kode_Supplier = PO.kd_supplier" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang=C.kd_cabang ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", kd_cabang);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.no_trans LIKE CONCAT('%',@no_trans,'%') ";
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
                    filter += " G.tgl_trans >= @DateFrom ";
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
                    filter += " G.tgl_trans <= @DateTo ";
                }

                if (kd_cabang != string.Empty && kd_cabang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY G.Last_Create_Date DESC, CASE WHEN G.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC, no_trans DESC ";


                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_IN>> GetGudangInPartial(DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT DISTINCT TOP " + seq + " G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat," +
                             " G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,S.Nama_Supplier,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa" +
                             " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK)" +
                             " INNER JOIN PURC.DBO.PURC_PO PO WITH (NOLOCK) on G.no_ref = PO.no_po" +
                             " LEFT OUTER JOIN SIF.DBO.SIF_Supplier S WITH (NOLOCK) ON S.Kode_Supplier = PO.kd_supplier" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang=C.kd_cabang " +
                             " INNER JOIN INV.DBO.INV_GUDANG_IN_D GDT WITH (NOLOCK) on G.no_trans = GDT.no_trans " +
                             " INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON GDT.kd_stok = B.kode_barang";

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
                    filter += " G.tgl_trans >= @DateFrom ";
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
                    filter += " G.tgl_trans <= @DateTo ";
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
                    filterquery = filterquery.Replace("no_trans", "G.no_trans");
                    filterquery = filterquery.Replace("no_ref", "G.no_ref");
                    filterquery = filterquery.Replace("tgl_trans", "G.tgl_trans");
                    filterquery = filterquery.Replace("nama_Supplier", "S.nama_Supplier");
                    filterquery = filterquery.Replace("keterangan", "G.keterangan");

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
                    sortingquery = sortingquery.Replace("no_trans", "G.no_trans");
                    sortingquery = sortingquery.Replace("no_ref", "G.no_ref");
                    sortingquery = sortingquery.Replace("tgl_trans", "G.tgl_trans");
                    sortingquery = sortingquery.Replace("nama_Supplier", "S.nama_Supplier");
                    sortingquery = sortingquery.Replace("keterangan", "G.keterangan");

                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY G.tgl_trans DESC ";
                }

                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static List<Response> GetCountGudang(DateTime DateFrom, DateTime DateTo, string filterquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result ,G.no_trans " +
                             " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK)" +
                             " INNER JOIN PURC.DBO.PURC_PO PO WITH (NOLOCK) on G.no_ref = PO.no_po" +
                             " LEFT OUTER JOIN SIF.DBO.SIF_Supplier S WITH (NOLOCK) ON S.Kode_Supplier = PO.kd_supplier" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang=C.kd_cabang "+
                              " INNER JOIN INV.DBO.INV_GUDANG_IN_D GDT WITH (NOLOCK) on G.no_trans = GDT.no_trans " +
                            " INNER JOIN SIF.DBO.SIF_BARANG B WITH (NOLOCK) ON GDT.kd_stok = B.kode_barang";
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
                    filter += " G.tgl_trans >= @DateFrom ";
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
                    filter += " G.tgl_trans <= @DateTo ";
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
                    filterquery = filterquery.Replace("no_trans", "G.no_trans");
                    filterquery = filterquery.Replace("no_ref", "G.no_ref");
                    filterquery = filterquery.Replace("tgl_trans", "G.tgl_trans");
                    filterquery = filterquery.Replace("nama_Supplier", "S.nama_Supplier");
                    filterquery = filterquery.Replace("keterangan", "G.keterangan");

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
                sql += " GROUP BY G.no_trans ";
                var res = con.Query<Response>(sql, param);

                return res.ToList();
            }
        }


        public static async Task<int> SaveAdjGudangIn(INV_OPNAME data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN]   (Kd_Cabang,tipe_trans,tgl_trans,jml_rp_trans,penyerah,keterangan,blthn,Program_Name,sj_supplier,Last_Create_Date,Last_Created_By,no_trans,kode_gudang) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@tgl_trans,@jml_rp_trans,@penyerah,@keterangan,@blthn,@Program_Name,@sj_supplier,@Last_Create_Date,@Last_Created_By,@no_trans,@kode_gudang);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            //param.Add("@no_ref", data.no_ref);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@jml_rp_trans", 0);
            param.Add("@penyerah", data.Last_Created_By);
            param.Add("@keterangan", data.keterangan);
            param.Add("@blthn", data.blthn);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@sj_supplier", "ADJ-OPN");
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@no_trans", data.no_trans);
            //param.Add("@no_qc", data.no_qc);
            param.Add("@rec_stat", "Y");
            param.Add("@kode_gudang", data.gudang);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<INV_GUDANG_IN>> GetGudangInBebas(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string kd_cabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat, G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa " +
                    "FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK) " +
                    "LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang " +
                    "LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang = C.kd_cabang";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", kd_cabang);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.no_trans LIKE CONCAT('%',@no_trans,'%') ";
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
                    filter += " G.tgl_trans >= @DateFrom ";
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
                    filter += " G.tgl_trans <= @DateTo ";
                }

                if (kd_cabang != string.Empty && kd_cabang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY G.Last_Create_Date DESC, CASE WHEN G.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC, no_trans DESC ";


                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_IN>> GetMTS_In(string kd_cabang , string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string kd_stok = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //string sql = "SELECT G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat, G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa " +
                //    "FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK) " +
                //    " INNER JOIN INV.DBO.INV_GUDANG_IN_D D on D.no_trans=G.no_trans " +
                //    "LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD ON G.kode_gudang = GD.kode_gudang " +
                //    "LEFT OUTER JOIN SIF.dbo.SIF_CABANG C ON GD.Kd_Cabang = C.kd_cabang " +
                //    " where substring(G.no_trans,13,3)='BMI' ";
                string sql = "SELECT G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat, G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa " +
                  "FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK) " +

                  "LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang " +
                  "LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang = C.kd_cabang " +
                  " where G.no_trans LIKE '%BM%' ";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_stok", kd_stok);
               

                if (no_trans != string.Empty && no_trans != null)
                    {
                    filter += " AND G.no_trans LIKE CONCAT('%',@no_trans,'%') ";
                    }
                else
                { 
                    if (DateFrom != null)
                    {
                    filter += " AND G.tgl_trans >= @DateFrom ";
                    }

                    if (DateTo != null)
                    { filter += " AND G.tgl_trans <= @DateTo ";
                    }
                  
                    if (kd_cabang != string.Empty && kd_cabang != null)
                    {
                    
                        filter += "AND G.Kd_Cabang=@kd_cabang ";
                    }
                    if (kd_stok != string.Empty && kd_stok != null)
                    {
                    
                        filter += "AND D.kd_stok=@kd_stok ";
                    }
                }
                sql += filter;

                sql += " ORDER BY G.Last_Create_Date DESC, CASE WHEN G.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC, no_trans DESC ";


                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_IN>> GetMTS_InPartial(string kd_cabang, DateTime DateFrom, DateTime DateTo, string filterquery = "", string sortingquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT  DISTINCT TOP " + seq + " G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat, G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa, G.Last_Create_Date " +
                  " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK) " +
                  " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang " +
                  " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang = C.kd_cabang" +
                  "  INNER JOIN INV.DBO.INV_GUDANG_IN_D GDD WITH(NOLOCK) ON G.no_trans=GDD.no_trans" +
                  "  INNER JOIN SIF.dbo.S_SIF_Barang B WITH(NOLOCK) ON GDD.kd_stok=B.Kode_Barang " +
                  " where G.no_trans LIKE '%BM%' ";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Nama_Barang", barang);

                if (DateFrom != null)
                {
                    filter += " AND G.tgl_trans >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    filter += " AND G.tgl_trans <= @DateTo ";
                }

                if (kd_cabang != string.Empty && kd_cabang != null)
                {
                    filter += "AND G.Kd_Cabang=@kd_cabang ";
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
                    filterquery = filterquery.Replace("no_trans", "G.no_trans");
                    filterquery = filterquery.Replace("no_ref", "G.no_ref");
                    filterquery = filterquery.Replace("tgl_trans", "G.tgl_trans");
                    filterquery = filterquery.Replace("keterangan", "G.keterangan");
                    filterquery = filterquery.Replace("nama_Gudang", "GD.nama_Gudang");

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
                    sortingquery = sortingquery.Replace("no_trans", "G.no_trans");
                    sortingquery = sortingquery.Replace("no_ref", "G.no_ref");
                    sortingquery = sortingquery.Replace("tgl_trans", "G.tgl_trans");
                    sortingquery = sortingquery.Replace("keterangan", "G.keterangan");
                    sortingquery = sortingquery.Replace("nama_Gudang", "GD.nama_Gudang");
                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY G.Last_Create_Date DESC ";
                }
                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static List<Response> GetCountMTS_InPartial(string kd_cabang, DateTime DateFrom, DateTime DateTo, string filterquery = "", string barang = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT  COUNT(*), G.no_trans " +
                  " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK) " +
                  " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang " +
                  " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang = C.kd_cabang" +
                  "  INNER JOIN INV.DBO.INV_GUDANG_IN_D GDD WITH(NOLOCK) ON G.no_trans=GDD.no_trans" +
                  "  INNER JOIN SIF.dbo.S_SIF_Barang B WITH(NOLOCK) ON GDD.kd_stok=B.Kode_Barang " +
                  " where G.no_trans LIKE '%BM%' ";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Nama_Barang", barang);

                if (DateFrom != null)
                {
                    filter += " AND G.tgl_trans >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    filter += " AND G.tgl_trans <= @DateTo ";
                }

                if (kd_cabang != string.Empty && kd_cabang != null)
                {
                    filter += "AND G.Kd_Cabang=@kd_cabang ";
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
                    filterquery = filterquery.Replace("no_trans", "G.no_trans");
                    filterquery = filterquery.Replace("no_ref", "G.no_ref");
                    filterquery = filterquery.Replace("tgl_trans", "G.tgl_trans");
                    filterquery = filterquery.Replace("keterangan", "G.keterangan");
                    filterquery = filterquery.Replace("nama_Gudang", "GD.nama_Gudang");

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

                sql += " GROUP BY G.no_trans ";


                var res = con.Query<Response>(sql, param);

                return res.ToList();
            }
        }



        public static async Task<IEnumerable<INV_GUDANG_IN>> GetGudangInBebas(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null, string kd_cabang = null, string tipe_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT G.Kd_Cabang,G.tipe_trans,G.no_trans,G.tgl_trans,G.no_ref,G.no_qc,G.penyerah,G.keterangan,G.rec_stat, " +
                             " G.jml_rp_trans,G.Last_Created_By,G.Last_Update_Date,G.Last_Updated_By,G.Program_Name,S.Nama_Supplier,G.kode_gudang,GD.Nama_Gudang, C.alamat, C.fax1 AS telp, C.fax2 as wa" +
                             " FROM INV.DBO.INV_GUDANG_IN G WITH(NOLOCK)" +
                             " INNER JOIN PURC.DBO.PURC_PO PO WITH (NOLOCK) on G.no_ref = PO.no_po" +
                             " LEFT OUTER JOIN SIF.DBO.SIF_Supplier S WITH (NOLOCK) ON S.Kode_Supplier = PO.kd_supplier" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_Gudang GD WITH (NOLOCK) ON G.kode_gudang = GD.kode_gudang" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_CABANG C WITH (NOLOCK) ON GD.Kd_Cabang=C.kd_cabang ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@status_po", status_po);
                param.Add("@kd_cabang", kd_cabang);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.no_trans LIKE CONCAT('%',@no_trans,'%') ";
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
                    filter += " G.tgl_trans >= @DateFrom ";
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
                    filter += " G.tgl_trans <= @DateTo ";
                }

                if (kd_cabang != string.Empty && kd_cabang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " G.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY G.Last_Create_Date DESC, CASE WHEN G.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC, no_trans DESC ";


                var res = con.Query<INV_GUDANG_IN>(sql, param);

                return res;
            }
        }

        public static async Task<int> SaveGudangIn(INV_GUDANG_IN data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN]   (Kd_Cabang,tipe_trans,no_qc,no_ref,tgl_trans,jml_rp_trans,penyerah,keterangan,blthn,Program_Name,sj_supplier,Last_Create_Date,Last_Created_By,no_trans,kode_gudang) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_qc,@no_ref,@tgl_trans,@jml_rp_trans,@penyerah,@keterangan,@blthn,@Program_Name,@sj_supplier,@Last_Create_Date,@Last_Created_By,@no_trans,@kode_gudang);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_ref", data.no_ref);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@penyerah", data.penyerah);
            param.Add("@keterangan", data.keterangan);
            param.Add("@blthn", data.blthn);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@sj_supplier", data.sj_supplier);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_qc", data.no_qc);
            param.Add("@rec_stat", "Y");
            param.Add("@kode_gudang", data.kode_gudang);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> InsertGudangSJK(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN]   (Kd_Cabang,tipe_trans,no_qc,no_ref,tgl_trans,jml_rp_trans,penyerah,keterangan,blthn,Program_Name,sj_supplier,Last_Create_Date,Last_Created_By,no_trans,kode_gudang) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_qc,@no_ref,GETDATE(),@jml_rp_trans,@penyerah,@keterangan,@blthn,@Program_Name,@sj_supplier,GETDATE(),@Last_Created_By,@no_trans,@kode_gudang);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@no_trans", data.Keterangan);// no gudang in
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_ref", data.No_sp);
            //param.Add("@tgl_trans", data.tgl);
            param.Add("@jml_rp_trans", 0);
            param.Add("@penyerah", data.Last_updated_by);
            param.Add("@keterangan", "Barang kembali dari SJ");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));
            param.Add("@Program_Name", "SJ Kembali");
            if (data.Program_name == "Pembatalan SJK")
            {
                param.Add("@sj_supplier", data.nama_customer);

            }
            else
            {
                param.Add("@sj_supplier", data.nama_agent);
            }

            // param.Add("@Last_Create_Date", data.Last_updated_by);
            param.Add("@Last_Created_By", data.Last_updated_by);

            param.Add("@no_qc", data.No_Gudang_Out);
            param.Add("@kode_gudang", "EXP001");


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> InsertGudangSJKDetil(SALES_SJ_D data, string gudang_tujuan, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan," +
                    " Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,GETDATE(),@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_trans", data.no_gdin); // no trans gd in
            param.Add("@no_qc", data.No_sp);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.Kd_stok);
            param.Add("@keterangan", "Barang Kembali SJ");
            param.Add("@kd_satuan", data.Kd_satuan);
            //param.Add("@kd_ukuran", data.kd_uku);
            //param.Add("@qty_sisa", data.qty_sisa);
            // param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_updated_by);
            param.Add("@Program_Name", data.Program_name);
            if (data.Program_name == "Pembatalan SJK")
            {
                param.Add("@qty_in", data.qty_out); // jika sjk
            }
            else
            {
                param.Add("@qty_in", data.Qty_balik); //jika SJ
            }

            param.Add("@qty_order", data.Qty_kirim);
            param.Add("@harga", 0);
            param.Add("@rp_trans", 0);
            param.Add("@gudang_asal", "EXP01");
            param.Add("@gudang_tujuan", gudang_tujuan);
            param.Add("@kd_buku_besar", "00000");
            param.Add("@kd_buku_biaya", "0000");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> InsertStok_In(INV_GUDANG_OUT data, SqlTransaction trans)
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
            param.Add("@sj_supplier", data.penerima);
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
        public static async Task<int> InsertStokDtl_in(INV_GUDANG_OUT_D data, string gudang_tujuan, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan," +
                    " Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,GETDATE(),@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_trans", data.no_trans); // no trans gd in
            param.Add("@no_qc", data.no_ref);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@keterangan", "Pembatalan Mutasi Out");
            param.Add("@kd_satuan", data.kd_satuan);
            //param.Add("@kd_ukuran", data.kd_uku);
            //param.Add("@qty_sisa", data.qty_sisa);
            // param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);

            param.Add("@qty_in", data.qty_out); // jika sjk


            param.Add("@qty_order", data.qty_out);
            param.Add("@harga", 0);
            param.Add("@rp_trans", 0);
            param.Add("@gudang_asal", "EXP01");
            param.Add("@gudang_tujuan", gudang_tujuan);
            param.Add("@kd_buku_besar", "00000");
            param.Add("@kd_buku_biaya", "0000");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> InsertGudangIn(string Kd_Cabang,string tipe_trans,string no_trans,string no_ref,DateTime tgl_trans,decimal jml_rp_trans,
            string penyerah,string keterangan,string blthn,string Program_Name,string sj_supplier,DateTime Last_Create_Date, string Last_Created_By,
            string no_qc,string gd_tujuan,string kd_customer,  SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN]   (Kd_Cabang,tipe_trans,no_qc,no_ref,tgl_trans,jml_rp_trans,penyerah,keterangan,blthn,Program_Name,sj_supplier,Last_Create_Date,Last_Created_By,no_trans,kode_gudang,kd_customer) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_qc,@no_ref,@tgl_trans,@jml_rp_trans,@penyerah,@keterangan,@blthn,@Program_Name,@sj_supplier,@Last_Create_Date,@Last_Created_By,@no_trans,@kode_gudang,@kd_customer);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", Kd_Cabang);
            param.Add("@tipe_trans", tipe_trans);
            param.Add("@no_ref", no_ref);
            param.Add("@tgl_trans",tgl_trans);
            param.Add("@jml_rp_trans", jml_rp_trans);
            param.Add("@penyerah", penyerah);
            param.Add("@keterangan", keterangan);
            param.Add("@blthn", blthn);
            param.Add("@Program_Name", Program_Name);
            param.Add("@sj_supplier", sj_supplier);
            param.Add("@Last_Create_Date", Last_Create_Date);
            param.Add("@Last_Created_By", Last_Created_By);
            param.Add("@no_trans", no_trans);
            param.Add("@no_qc", no_qc);
            param.Add("@rec_stat", "Y");
            param.Add("@kode_gudang", gd_tujuan); //kd_customer
            param.Add("@kd_customer", kd_customer); //kd_customer

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }


        public static async Task<int> InsertGudangInDetil(string kdcb, string no_tran,string tipe_tran, string no_so, decimal noseq, string kd_Stok, string satuan, string user,string ket, decimal qty_order,decimal harga, decimal qtyin, string gudang_tujuan, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan," +
                    " Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,GETDATE(),@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);
            param.Add("@tipe_trans",tipe_tran ); //"JPJ-KPT-01"
            param.Add("@no_trans", no_tran);
            param.Add("@no_qc", no_so);
            param.Add("@no_seq", noseq);
            param.Add("@kd_stok", kd_Stok);
            param.Add("@keterangan", "Retur Non Cash");
            param.Add("@kd_satuan", satuan);
            //param.Add("@kd_ukuran", data.kd_uku);
            //param.Add("@qty_sisa", data.qty_sisa);
            // param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", user);
            param.Add("@Program_Name", ket);
            //if (data.Program_name == "Pembatalan SJK")
            //{
            param.Add("@qty_in", qtyin); // jika sjk
                                         //}else
                                         //{
                                         //    param.Add("@qty_in", data.Qty_kirim); //jika SJ
                                         //}

            param.Add("@qty_order", qty_order);
            param.Add("@harga", harga);
            param.Add("@rp_trans", 0);
            param.Add("@gudang_asal", "EXP01");
            param.Add("@gudang_tujuan", gudang_tujuan);
            param.Add("@kd_buku_besar", "00000");
            param.Add("@kd_buku_biaya", "0000");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> InsertGudangSJDetil(SALES_SJ_D data, string gudang_tujuan, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan," +
                    " Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,GETDATE(),@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@tipe_trans", "JPJ-KPT-01");
            param.Add("@no_trans", data.no_gdin);
            param.Add("@no_qc", data.No_sp);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.Kd_stok);
            param.Add("@keterangan", "Pembatalan SJ");
            param.Add("@kd_satuan", data.Kd_satuan);
            //param.Add("@kd_ukuran", data.kd_uku);
            //param.Add("@qty_sisa", data.qty_sisa);
            // param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_updated_by);
            param.Add("@Program_Name", data.Program_name);
            param.Add("@qty_in", data.Qty_kirim);
            param.Add("@qty_order", data.Qty_kirim);
            param.Add("@harga", 0);
            param.Add("@rp_trans", 0);
            param.Add("@gudang_asal", "EXP01");
            param.Add("@gudang_tujuan", gudang_tujuan);
            param.Add("@kd_buku_besar", "00000");
            param.Add("@kd_buku_biaya", "0000");
            param.Add("@blthn", DateTime.Now.ToString("yyyyMM"));

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<V_MonStok>> GetMonStok(string Kode_Barang, string blnthn, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                string filter = "";

                string sql = "SELECT   A.kd_stok,A.qty_akhir_expedisi,A.bultah,B.Nama_Barang,B.Kd_Satuan, b.stok_min, " + //k.nama as kategori,k2.nama as sub_kategori,
                    "sum(a.awal_qty_onstok) as awal_qty_onstok, sum(A.qty_onstok_in) as qty_onstok_in, sum(A.qty_onstok_out) as qty_onstok_out, " +
                    "sum(A.akhir_booked) akhir_booked, sum(A.akhir_qty_onstok) as akhir_qty_onstok, sum(A.qty_available) as qty_available, " +
                    "CASE WHEN sum(A.qty_available) <= B.stok_min THEN 'Limit' ELSE 'Aman' END as 'Status_Stok' " +
                    "FROM INV.dbo.INV_STOK_SALDO A " +
                    "inner join SIF.dbo.SIF_Barang b on b.Kode_Barang = A.kd_stok " +
                    //"inner join SIF.dbo.SIF_kategori k on k.kd_kategori = b.kd_kategori " +
                    //"INNER join SIF.dbo.SIF_sub_kategori k2 on k2.kd_sub_kategori = b.kd_sub_kategori " +
                    //"left outer join[SIF].[dbo].[SIF_Merk] C ON C.Kode_Merk = B.kd_merk " +
                    "where b.Rec_Stat='Y' ";

                param.Add("@Kode_Barang", Kode_Barang);
                param.Add("@blnthn", blnthn);
                param.Add("@cb", cb);
                if (Kode_Barang != string.Empty && Kode_Barang != null)
                {
                    filter += " AND A.kd_stok =@Kode_Barang AND A.Kd_Cabang=@cb";
                }
                if (blnthn != string.Empty && blnthn != null)
                {
                    filter += " AND A.bultah =@blnthn AND A.Kd_Cabang=@cb ";
                }
                sql += filter;
                sql += " GROUP BY a.kd_stok ,A.bultah,A.qty_akhir_expedisi, b.Nama_Barang,B.Kd_Satuan, b.stok_min ";
                var res = con.Query<V_MonStok>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<V_MonStok>> GetMonStokPartial(string blnthn, string cb = null, string filterquery = "", string sortingquery = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                string filter = "";

                string sql = "SELECT TOP " + seq + "  A.kd_stok,A.qty_akhir_expedisi,A.bultah,B.Nama_Barang,B.Kd_Satuan, b.stok_min, " + 
                    "sum(a.awal_qty_onstok) as awal_qty_onstok, sum(A.qty_onstok_in) as qty_onstok_in, sum(A.qty_onstok_out) as qty_onstok_out, " +
                    "sum(A.akhir_booked) akhir_booked, sum(A.akhir_qty_onstok) as akhir_qty_onstok, sum(A.qty_available) as qty_available, " +
                    "CASE WHEN sum(A.qty_available) <= B.stok_min THEN 'Limit' ELSE 'Aman' END as 'Status_Stok' " +
                    "FROM INV.dbo.INV_STOK_SALDO A " +
                    "inner join SIF.dbo.SIF_Barang b on b.Kode_Barang = A.kd_stok " +
                    "where b.Rec_Stat='Y' ";

                param.Add("@blnthn", blnthn);
                param.Add("@cb", cb);

                if (blnthn != string.Empty && blnthn != null)
                {
                    filter += " AND A.bultah =@blnthn AND A.Kd_Cabang=@cb ";
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
                    filterquery = filterquery.Replace("bultah", "A.bultah");
                    filterquery = filterquery.Replace("kd_stok", "A.kd_stok");
                    filterquery = filterquery.Replace("Nama_Barang", "B.Nama_Barang");
                    filterquery = filterquery.Replace("Kd_Satuan", "B.Kd_Satuan");
                    filterquery = filterquery.Replace("stok_min", "B.stok_min");
                    filterquery = filterquery.Replace("akhir_qty_onstok", "A.akhir_qty_onstok");
                    filterquery = filterquery.Replace("akhir_booked", "A.akhir_booked");
                    filterquery = filterquery.Replace("qty_akhir_expedisi", "A.qty_akhir_expedisi");
                    filterquery = filterquery.Replace("qty_available", "A.qty_available");

                    filter += " " + filterquery + " ";
                }

                sql += filter;
                sql += " GROUP BY a.kd_stok ,A.bultah,A.qty_akhir_expedisi, b.Nama_Barang,B.Kd_Satuan, b.stok_min ";

                if (sortingquery != null && sortingquery != "")
                {
                    sortingquery = sortingquery.Replace("bultah", "A.bultah");
                    sortingquery = sortingquery.Replace("kd_stok", "A.kd_stok");
                    sortingquery = sortingquery.Replace("Nama_Barang", "B.Nama_Barang");
                    sortingquery = sortingquery.Replace("Kd_Satuan", "B.Kd_Satuan");
                    sortingquery = sortingquery.Replace("stok_min", "B.stok_min");
                    sortingquery = sortingquery.Replace("akhir_qty_onstok", "A.akhir_qty_onstok");
                    sortingquery = sortingquery.Replace("akhir_booked", "A.akhir_booked");
                    sortingquery = sortingquery.Replace("qty_akhir_expedisi", "A.qty_akhir_expedisi");
                    sortingquery = sortingquery.Replace("qty_available", "A.qty_available");
                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY B.nama_Barang ";
                }
                var res = con.Query<V_MonStok>(sql, param);

                return res;
            }
        }

        public static List<Response> GetCountMonStokPartial(string blnthn, string cb = null, string filterquery = "", string sortingquery = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                string filter = "";

                string sql = "SELECT COUNT(*) Result " +
                    "FROM INV.dbo.INV_STOK_SALDO A " +
                    "inner join SIF.dbo.SIF_Barang b on b.Kode_Barang = A.kd_stok " +
                    "where b.Rec_Stat='Y' ";

                param.Add("@blnthn", blnthn);
                param.Add("@cb", cb);

                if (blnthn != string.Empty && blnthn != null)
                {
                    filter += " AND A.bultah =@blnthn AND A.Kd_Cabang=@cb ";
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
                    filterquery = filterquery.Replace("bultah", "A.bultah");
                    filterquery = filterquery.Replace("kd_stok", "A.kd_stok");
                    filterquery = filterquery.Replace("Nama_Barang", "B.Nama_Barang");
                    filterquery = filterquery.Replace("Kd_Satuan", "B.Kd_Satuan");
                    filterquery = filterquery.Replace("stok_min", "B.stok_min");
                    filterquery = filterquery.Replace("akhir_qty_onstok", "A.akhir_qty_onstok");
                    filterquery = filterquery.Replace("akhir_booked", "A.akhir_booked");
                    filterquery = filterquery.Replace("qty_akhir_expedisi", "A.qty_akhir_expedisi");
                    filterquery = filterquery.Replace("qty_available", "A.qty_available");

                    filter += " " + filterquery + " ";
                }

                sql += filter;
                sql += " GROUP BY a.kd_stok ,A.bultah,A.qty_akhir_expedisi, b.Nama_Barang,B.Kd_Satuan, b.stok_min ";

                if (sortingquery != null && sortingquery != "")
                {
                    sortingquery = sortingquery.Replace("bultah", "A.bultah");
                    sortingquery = sortingquery.Replace("kd_stok", "A.kd_stok");
                    sortingquery = sortingquery.Replace("Nama_Barang", "B.Nama_Barang");
                    sortingquery = sortingquery.Replace("Kd_Satuan", "B.Kd_Satuan");
                    sortingquery = sortingquery.Replace("stok_min", "B.stok_min");
                    sortingquery = sortingquery.Replace("akhir_qty_onstok", "A.akhir_qty_onstok");
                    sortingquery = sortingquery.Replace("akhir_booked", "A.akhir_booked");
                    sortingquery = sortingquery.Replace("qty_akhir_expedisi", "A.qty_akhir_expedisi");
                    sortingquery = sortingquery.Replace("qty_available", "A.qty_available");
                    sql += " " + sortingquery + " ";

                }
                else
                {
                    sql += " ORDER BY B.nama_Barang ";
                }
                var res = con.Query<Response>(sql, param, null, true, 36000);
                return res.ToList();
            }
        }

        public static async Task<IEnumerable<V_MonStokDetail>> GetMonStokDetail(string Kode_Barang, string blnthn)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                //string filter = "";
                //string sql = "select ROW_NUMBER() OVER(ORDER BY A.kd_stok) as 'nomor',B.Kode_Gudang ,B.Nama_Gudang, A.kd_stok, A.bultah, bg.Nama_Barang,A.awal_qty_onstok awal_qty,akhir_qty_onstok as akhir_qty, A.qty_onstok_in as qty_in, A.qty_onstok_out as qty_out,bg.Kd_Satuan,qty_akhir_expedisi  " +
                //    "from INV.dbo.INV_STOK_SALDO A " +
                //    "inner join SIF.dbo.SIF_Barang bg on A.kd_stok = bg.Kode_Barang " +
                //    " inner join SIF.dbo.SIF_Gudang B on A.Kd_Cabang = B.kd_cabang";

                string sql = "select DISTINCT ROW_NUMBER() OVER(ORDER BY A.kd_stok) as 'nomor',B.Kode_Gudang ,B.Nama_Gudang, A.kd_stok, A.bultah, bg.Nama_Barang,A.awal_qty awal_qty,akhir_qty as akhir_qty, A.qty_in as qty_in, A.qty_out as qty_out,bg.Kd_Satuan,0 AS qty_akhir_expedisi  " +
                        " from INV.dbo.INV_STOK_GUDANG A " +
                        " inner join SIF.dbo.SIF_Barang bg on A.kd_stok = bg.Kode_Barang " +
                        " inner join SIF.dbo.SIF_Gudang B on A.Kd_Cabang = B.kd_cabang AND A.kode_gudang = B.Kode_Gudang ";

                param.Add("@Kode_Barang", Kode_Barang);
                param.Add("@blnthn", blnthn);

                if (Kode_Barang != string.Empty && Kode_Barang != null)
                {
                    sql += " AND A.kd_stok =@Kode_Barang ";
                }
                if (blnthn != null)
                {

                    sql += " AND A.bultah = @blnthn ";


                }
                //sql += " GROUP BY A.kd_stok, A.bultah,bg.Nama_Barang,A.awal_qty,A.qty_in, A.qty_out,bg.Kd_Satuan,bg.Kd_Satuan ";

                var res = con.Query<V_MonStokDetail>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<StokAllGudang>> GetStokAllGudang(string Kode_Barang, string blnthn)
        {
            using (var con = DataAccess.GetConnection())
            {
                DynamicParameters param = new DynamicParameters();
                string filter = "";
                string sql = "SELECT bultah,kd_stok,Nama_Barang,kd_satuan,ISNULL(BANGIL,0) as Bangil,ISNULL(KOMBESSIDOARJO,0) as Kombes_Sidoarjo,ISNULL(Lamongan,0) as Lamongan,ISNULL(LINGKARTIMUR,0) as Lingkar_timur,ISNULL(KAIROS,0) as Kairos " +
                    "FROM(SELECT REPLACE(B.Nama_Gudang, ' ', '') as Nama_Gudang, A.kd_satuan, A.kd_stok, A.bultah, bg.Nama_Barang, akhir_qty_onstok as akhir_qty " +
                    "from INV.dbo.INV_STOK_SALDO A WITH (NOLOCK) inner join SIF.dbo.SIF_Barang bg WITH (NOLOCK) on A.kd_stok = bg.Kode_Barang inner join SIF.dbo.SIF_Gudang B WITH (NOLOCK) on A.Kd_Cabang = B.kd_cabang) S " +
                    "pivot(MAX(akhir_qty) for Nama_Gudang in (KOMBESSIDOARJO, BANGIL, Lamongan, LINGKARTIMUR, KAIROS)) piv";

                //sql += " GROUP BY A.kd_stok, A.bultah,bg.Nama_Barang,A.awal_qty,A.qty_in, A.qty_out,bg.Kd_Satuan,bg.Kd_Satuan ";
                param.Add("@Kode_Barang", Kode_Barang);
                param.Add("@blnthn", blnthn);
                if (Kode_Barang != string.Empty && Kode_Barang != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " kd_stok=@Kode_Barang ";

                }
                if (blnthn != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " bultah=@blnthn ";
                }
                sql += filter;
                sql += " ORDER BY Nama_Barang";
                var res = con.Query<StokAllGudang>(sql, param);

                return res;
            }
        }
        public static async Task<int> UpdateQC(INV_QC_M data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [INV].[dbo].[INV_QC_M]   SET trx_stat = @trx_stat where no_trans = @no_qc";
            param = new DynamicParameters();
            param.Add("@no_qc", data.no_trans);
            param.Add("@trx_stat", data.trx_stat);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdatePODetail(PURC_PO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


          //  Query = "update PURC.dbo.PURC_PO_D set harga=@harga,harga_new=@harga_new,total = @total,total_new = @total_new,jml_diskon=@jml_diskon, qty_kirim=@qty_kirim, qty_sisa=@qty_sisa " +
             //   " where no_po=@no_po AND kd_stok=@kd_stok and no_seq=@no_seq ";
            Query = "update PURC.dbo.PURC_PO_D set qty_kirim=@qty_kirim, qty_sisa=@qty_sisa, tgl_kirim=getdate() " +
              " where no_po=@no_po AND kd_stok=@kd_stok and no_seq=@no_seq ";
            param = new DynamicParameters();
            param.Add("@no_po", data.no_po);
            param.Add("@harga", data.harga);
            param.Add("@harga_new", data.harga_new);
            param.Add("@total", data.total);
            param.Add("@total_new", data.total_new);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@jml_diskon", data.jml_diskon);
            param.Add("@no_seq", data.no_seq);
            param.Add("@qty_kirim", data.qty_kirim);
            param.Add("@qty_sisa", data.qty_sisa);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateGrandTotalPO(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update PURC.dbo.PURC_PO set jml_rp_trans = @jml_rp_trans where no_po=@no_po ";
            param = new DynamicParameters();
            param.Add("@no_po", data.no_po);
            param.Add("@jml_rp_trans", data.jml_rp_trans);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> UpdateQCDetail(PURC_PO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update INV.dbo.INV_QC set harga=@harga where no_ref=@no_po AND kd_stok=@kd_stok AND no_seq = @no_seq ";
            param = new DynamicParameters();
            param.Add("@no_po", data.no_po);
            param.Add("@harga", data.harga);
            param.Add("@total", data.total);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@no_seq", data.no_seq);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }



        public static async Task<List<INV_GUDANG_OUT>> GetMtsOutCbo()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select no_trans from INV.dbo.INV_GUDANG_OUT WITH (NOLOCK) where tipe_trans='JPB-KUT-02' and isnull(sudah_sj,0)=0 and isnull(rec_stat,'Y')<> 'N'" +
                    " ORDER BY Last_Create_Date DESC";
                DynamicParameters param = new DynamicParameters();
                var res = con.Query<INV_GUDANG_OUT>(sql, param);
                return res.ToList();
            }
        }

        public static async Task<int> InsertNota_Beli(string no_trans, SqlTransaction trans, SqlConnection conn)
        {
            //using (var con = DataAccess.GetConnection())
            //{
            //    string res = "";
            //    DynamicParameters _params = new DynamicParameters();
            //    _params.Add("@vno_trans", no_trans, dbType: DbType.String, direction: ParameterDirection.Input);
            //    var result = con.Execute("[FIN].[dbo].[FIN_INS_NOTA_BELI]", _params, null, 36000, CommandType.StoredProcedure);
            //    //var retVal = _params.Get<string>("@vno_trans");


            //    // var res = con.Query<PURC_PO>(sql, param);

            //    return res;
            //}
            DynamicParameters param = new DynamicParameters();
            param.Add("@vno_trans", no_trans);

            return conn.Execute("[FIN].[dbo].[FIN_INS_NOTA_BELI]", param, trans, 36000, CommandType.StoredProcedure);
        }

        public static int InsertNota_BeliThread(string no_trans, SqlTransaction trans, SqlConnection conn)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@vno_trans", no_trans);

            return conn.Execute("[FIN].[dbo].[FIN_INS_NOTA_BELI]", param, trans, 36000, CommandType.StoredProcedure);
        }

        public static async Task<int> SPStatusPO(string notrans, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@xno_po", notrans);

                return conn.Execute("[PURC].[dbo].[proses_sts_po]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPStatusQC(string no_po, string no_qc, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sp", no_po);
                param.Add("@no_qc", no_qc);

                return conn.Execute("[INV].[dbo].[sp_close_terima]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_IN_D>> getGudangDetail(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT m.no_trans,m.penyerah,m.no_ref,m.no_qc,m.jml_ppn,m.jml_rp_trans,D.Kd_Cabang, d.no_trans,  d.gudang_asal, d.gudang_tujuan, d.blthn, d.tipe_trans, D.kd_stok, D.kd_ukuran, D.kd_satuan, d.qty_order, d.qty_in, d.qty_sisa, d.harga, d.rp_trans, d.blthn, d.kd_buku_besar, d.kd_buku_biaya, D.keterangan, D.Last_Create_Date, D.Last_Created_By, D.Program_Name, B.Nama_Barang, G1.Nama_Gudang,G.Nama_Gudang as cb_asal, c.alamat, c.fax1, c.fax2  " +
                "FROM INV.dbo.INV_GUDANG_IN m WITH(NOLOCK) " +
                "INNER JOIN INV.dbo.INV_GUDANG_IN_D D WITH(NOLOCK) on m.no_trans = D.no_trans " +
                "LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G WITH(NOLOCK) ON  D.gudang_asal = G.Kode_Gudang  " +
                " LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G1 WITH(NOLOCK) ON  G1.Kode_Gudang=D.gudang_tujuan " +
                " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON C.kd_cabang=m.Kd_Cabang " +
                "INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE D.no_trans=@no_trans ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);

                return res;
            }
        }

        public static async Task<int> UpdateGudangIn(INV_GUDANG_IN data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "UPDATE [INV].[dbo].[INV_GUDANG_IN]   SET Kd_Cabang=@Kd_Cabang,tipe_trans=@tipe_trans,tgl_trans=@tgl_trans,no_ref=@no_ref,penyerah=@penyerah,jml_rp_trans=@jml_rp_trans,jml_ppn=@jml_ppn, " +
                    " jml_rp_trans=@jml_rp_trans,keterangan=@keterangan,rec_stat= @rec_stat,Last_Update_Date= @Last_Update_Date, " +
                    " sj_supplier=@sj_supplier,kd_customer=@kd_customer,gudang_tujuan=@gudang_tujuan, gudang_asal=@gudang_asal,no_qc=@no_qc " +
                    " Last_Updated_By= @Last_Updated_By,Program_Name= @Program_Name " +
                    " WHERE no_trans= @no_trans";

            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@no_ref", data.no_ref);
            param.Add("@penyerah", data.penyerah);
            param.Add("@no_qc", data.no_qc);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@sj_supplier", data.sj_supplier);
            param.Add("@kd_customer", data.kd_customer);
            param.Add("@keterangan", data.keterangan);
            param.Add("@rec_stat", data.status);
            param.Add("@gudang_tujuan", data.gudang_tujuan);
            param.Add("@gudang_asal", data.gudang_asal);
            param.Add("@no_qc", data.no_qc);
            param.Add("@Last_Update_Date", data.Last_Update_Date);
            param.Add("@Last_Updated_By", data.Last_Updated_By);
            param.Add("@Program_Name", data.Program_Name);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<PrintTerima> GetPrintTerima(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT C.alamat AS AlamatCabang,GD.no_trans,C.nama AS Cabang, C.telp1 as Telp1, C.kota + '-' + C.propinsi as Kota," +
                             " GD.no_ref as PONumber, cast(datepart(dd, GD.tgl_trans) as char(2)) + ' ' + datename(mm, GD.tgl_trans) + ' ' + cast(datepart(yyyy, GD.tgl_trans) as char(4)) AS Tanggaltrans," +
                             " S.Nama_Supplier,S.Alamat1 + CASE WHEN S.Kota1 IS NOT NULL AND S.Kota1 <> '' THEN ' - ' + S.Kota1 ELSE '' END As AlamatSupplier,GD.Keterangan, PO.atas_nama, PO.Keterangan as POKeterangan, S.No_Telepon1" +
                             " FROM INV.dbo.INV_GUDANG_IN GD WITH (NOLOCK) " +
                             " LEFT OUTER JOIN SIF.dbo.SIF_cabang C WITH (NOLOCK) ON GD.Kd_Cabang = C.kd_cabang" +
                             " LEFT OUTER JOIN PURC.dbo.PURC_PO PO WITH (NOLOCK)  ON PO.no_po = GD.no_ref" +
                             " LEFT OUTER JOIN SIF.dbo.SIF_Supplier S WITH (NOLOCK) ON PO.kd_supplier = S.Kode_Supplier" +
                             " WHERE GD.no_trans = @no_trans ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                var res = con.Query<PrintTerima>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static async Task<int> SPGudangIn(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty_in, string gudang_tujuan, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_ukuran", "00");
                param.Add("@kd_satuan", kd_satuan);
                param.Add("@kode_gudang", gudang_tujuan);
                param.Add("@tinggi", 0);
                param.Add("@lebar", 0);
                param.Add("@panjang", 0);
                param.Add("@qty_in", qty_in);
                return conn.Execute("[INV].[dbo].[inv_stok_saldo_gudang_in]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SPStokIn(string kd_cabang, string blthn, string kd_stok, string kd_satuan, decimal qty_in, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);
                param.Add("@bultah", blthn);
                param.Add("@kd_stok", kd_stok);
                //param.Add("@kd_ukuran", "00");
                param.Add("@kd_satuan", kd_satuan);
                //param.Add("@tinggi", 0);
                //param.Add("@lebar", 0);
                //param.Add("@panjang", 0);
                param.Add("@onstok_in", qty_in);
                param.Add("@vnilai", 0);

                return conn.Execute("[INV].[dbo].[inv_onstok_saldo_in]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<IEnumerable<INV_GUDANG_IN_D>> getGudangDetailByNoPO(string no_trans = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT m.no_trans,m.penyerah,m.no_ref,m.no_qc,m.jml_ppn,m.jml_rp_trans,D.Kd_Cabang, d.no_trans,  d.gudang_asal, d.gudang_tujuan, d.blthn, d.tipe_trans, D.kd_stok, D.kd_ukuran, D.kd_satuan, d.qty_order, d.qty_in, d.qty_sisa, d.harga, d.rp_trans, d.blthn, d.kd_buku_besar, d.kd_buku_biaya, D.keterangan, D.Last_Create_Date, D.Last_Created_By, D.Program_Name, B.Nama_Barang, G.Nama_Gudang,'GUDANG EXPEDISI' as cb_asal, c.alamat, c.fax1, c.fax2  " +
                "FROM INV.dbo.INV_GUDANG_IN m WITH(NOLOCK) " +
                "INNER JOIN INV.dbo.INV_GUDANG_IN_D D WITH(NOLOCK) on m.no_trans = D.no_trans " +
                "LEFT OUTER JOIN SIF.dbo.SIF_GUDANG G WITH(NOLOCK) ON D.gudang_tujuan = G.Kode_Gudang " +
                " INNER JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON C.kd_cabang=m.Kd_Cabang " +
                "INNER JOIN SIF.dbo.SIF_BARANG B WITH(NOLOCK) ON D.kd_stok = B.Kode_Barang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    sql += " WHERE m.no_ref=@no_trans ";
                }

                sql += " ORDER BY Last_Create_Date DESC, CASE WHEN D.Last_Update_Date IS NULL THEN 1 ELSE 0 END ASC ";

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);

                return res;
            }
        }

        public static async Task<int> DelGudangIN(string no_trans, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_GUDANG_IN  where no_ref = @no_ref ";
            param = new DynamicParameters();
            //param.Add("@Kd_Cabang", kdcb);
            param.Add("@no_ref", no_trans);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DelQC(string no_trans,  SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_QC_M  where no_ref = @no_ref ";
            param = new DynamicParameters();
            //param.Add("@Kd_Cabang", kdcb);
            param.Add("@no_ref", no_trans);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DelQC_detail(string no_trans,  SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_QC  where no_ref = @no_ref ";
            param = new DynamicParameters();
            //param.Add("@Kd_Cabang", kdcb);
            param.Add("@no_ref", no_trans);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
        public static async Task<int> DelGudangINDtl(string no_trans, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE INV.dbo.INV_GUDANG_IN_D  where no_ref = @no_ref ";
            param = new DynamicParameters();
          
            param.Add("@no_ref", no_trans);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
        public static async Task<int> UpdateGudang(string kdcb, string No_Gudang_Out, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE INV.dbo.INV_GUDANG_OUT set rec_stat='N' where no_trans = @no_inv and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);

            param.Add("@no_inv", No_Gudang_Out);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
    }
}
