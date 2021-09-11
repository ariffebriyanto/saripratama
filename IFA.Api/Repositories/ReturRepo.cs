using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class ReturRepo
    {
        public static IEnumerable<InvRetur> GetReturInv(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select  n.no_ref2, n.nm_cust, no_inv, atas_nama, jenis_sp,n.no_posting  " +
                    " from fin.dbo.FIN_NOTA n  WITH (NOLOCK)" +
                    //  " left join SALES.dbo.SALES_RETUR r on r.No_ref1 = n.no_ref2 " +
                    " INNER JOIN SALES.dbo.SALES_SO SWITH (NOLOCK) ON N.no_ref1=S.No_sp " +
                    // " where r.No_ref1 IS NULL and  isnull(n.no_posting, '') <> '' " +
                    " and n.Kd_cabang = @kd_cabang " +
                    " order by no_ref2 DESC ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);

                var res = con.Query<InvRetur>(sql, param);

                return res;
            }
        }

        public static IEnumerable<InvRetur> GetReturInv2(string kd_cabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select s.no_sp as no_ref2,c.Kd_Customer,c.Nama_Customer as nm_cust,s.Atas_Nama atas_nama,c.Alamat1 as alamat,s.Jenis_sp jenis_sp from SALES.dbo.SALES_SO s inner join SIF.dbo.SIF_Customer c on  c.Kd_Customer=s.Kd_Customer " +
                    "where s.kd_cabang=@kd_cabang and s.jenis_sp='NON CASH' and s.STATUS_DO='TERKIRIM' ";
                //" from fin.dbo.FIN_NOTA n  " +
                ////  " left join SALES.dbo.SALES_RETUR r on r.No_ref1 = n.no_ref2 " +
                //" INNER JOIN SALES.dbo.SALES_SO S ON N.no_ref1=S.No_sp " +
                //// " where r.No_ref1 IS NULL and  isnull(n.no_posting, '') <> '' " +
                //" and n.Kd_cabang = @kd_cabang " +
                //" order by no_ref2 DESC ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kd_cabang);

                var res = con.Query<InvRetur>(sql, param);

                return res;
            }
        }


        public static IEnumerable<InvReturDtl> GetDODetails(string id)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from sales.dbo.returbaru where no_inv = @id ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);

                var res = con.Query<InvReturDtl>(sql, param);

                return res;
            }
        }

        public static IEnumerable<InvReturDtl> GetDODetails2(string id)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select b.Nama_Barang,d.Kd_Stok,d.Kd_satuan,d.harga,d.potongan,d.Qty,(d.Qty - isnull(d.qty_retur,0)) as qty_available,0 as qty_retur,0 as total from SALES.dbo.SALES_SO_D d WITH (NOLOCK) " +
                "inner join SIF.dbo.SIF_Barang b WITH (NOLOCK) on d.Kd_Stok = b.Kode_Barang where no_sp = @id ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@id", id);

                var res = con.Query<InvReturDtl>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_RETUR>> GetMonRetur(string kdcb, string no_trans = null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select r.Kd_Cabang,r.No_retur,r.Tgl_retur,r.Last_Create_Date,r.Keterangan,r.No_ref1,r.No_ref2,r.No_ref3,r.Nama_agen,d.Qty,d.qty_nota,d.qty_tarik,d.Kd_Stok,r.Last_Created_By, C.alamat, C.fax1 as telp, C.fax2 as wa " +
                   " from SALES.dbo.SALES_RETUR r WITH (NOLOCK) inner " +
                   " join SALES.dbo.SALES_RETUR_D d WITH (NOLOCK) on r.No_retur = d.No_retur  " +
                    " LEFT JOIN SIF.dbo.SIF_CABANG C WITH(NOLOCK) ON r.Kd_Cabang=C.kd_cabang " +
                    " where r.Kd_Cabang=@kdcb ";

                DynamicParameters param = new DynamicParameters();
                //     param.Add("@kd_stok", kd_stok);
                param.Add("@kdcb", kdcb);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " AND ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " r.Tgl_retur >= @DateFrom"; // convert(date,'" + DateFrom + "',103)  ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        //filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " r.Tgl_retur <= @DateTo"; // convert(date,'" + DateFrom + "',103)  ";
                }

                filter += " ORDER BY r.Last_Create_Date DESC ";

                sql += filter;

                var res = con.Query<SALES_RETUR>(sql, param);
                //var res = con.Query<INV_GUDANG_IN_D>(sql, param);
                return res;
            }
        }

        public static async Task<IEnumerable<SALES_RETUR_D>> GetMonReturD(string kdcb, string no_trans = null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select r.Kd_Cabang, d.no_seq, r.No_retur,r.Tgl_retur,r.No_ref1,r.No_ref2,r.No_ref3,r.Nama_agen,r.Last_Create_Date,r.Last_Created_By,b.Nama_Barang nama_barang,b.Kd_Satuan kd_satuan,d.Qty,d.qty_nota,d.qty_tarik,d.Kd_Stok " +
                    "from SALES.dbo.SALES_RETUR r WITH (NOLOCK) inner " +
                    "join SALES.dbo.SALES_RETUR_D d WITH (NOLOCK) on r.No_retur = d.No_retur " +
                    "inner join SIF.dbo.SIF_Barang b WITH (NOLOCK) on d.Kd_Stok = b.Kode_Barang  " +
                     " where r.Kd_Cabang=@kdcb ";

                DynamicParameters param = new DynamicParameters();
                //     param.Add("@kd_stok", kd_stok);
                param.Add("@kdcb", kdcb);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


                if (DateFrom != null)
                {
                    if (filter == "")
                    {
                        filter += " AND ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " r.Tgl_retur >= @DateFrom"; // convert(date,'" + DateFrom + "',103)  ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        //filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " r.Tgl_retur <= @DateTo"; // convert(date,'" + DateTo + "',103) ";
                }

                filter += " ORDER BY r.Last_Create_Date DESC ";

                sql += filter;

                var res = con.Query<SALES_RETUR_D>(sql, param);
                //var res = con.Query<INV_GUDANG_IN_D>(sql, param);
                return res;
            }
        }
    }
}
