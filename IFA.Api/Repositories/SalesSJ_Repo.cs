using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ERP.Domain.Base;

namespace IFA.Api.Repositories
{
    public class SalesSJ_Repo
    {
        public static List<SALES_SJ_D> GetListSJ()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select top 100 m.no_trans as no_krm,d.no_sp,d.no_sp_dtl,d.kd_customer as kd_customer,c.Nama_Customer nama_customer,m.kd_sopir,m.kd_kenek, '0' as cetak, '' as no_sj, getDate() as Tgl_Cetak , '' as Status, '' as Alasan, m.tanggal AS tgl_rcn  " +
                    "from PROD.dbo.PROD_rcn_krm_m m WITH (NOLOCK) " +
                    "INNER JOIN PROD.dbo.PROD_rcn_krm_d d WITH (NOLOCK) on m.no_trans = d.no_trans " +
                    "left join sales.dbo.sales_so b WITH (NOLOCK) on b.No_sp = d.no_sp " +
                    "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on b.Kd_Customer = c.Kd_Customer " +
                    "where ISNULL(b.sudah_sj,0) <> 1";

                DynamicParameters param = new DynamicParameters();

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res.ToList();
            }
        }

        public static List<SALES_SJ> GetListSJK(string kdcabang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = " select top 100 no_sj from SALES.dbo.SALES_SJ WITH (NOLOCK) where Kd_Cabang=@Kd_Cabang and ISNULL(status_sj,'OPEN') = 'OPEN'";

                DynamicParameters param = new DynamicParameters();
                param.Add("@Kd_Cabang", kdcabang);

                var res = con.Query<SALES_SJ>(sql, param);

                return res.ToList();
            }
        }
        public static async Task<IEnumerable<SALES_SJ_D>> GetSJ(string no_sj = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select ROW_NUMBER() OVER(ORDER BY d.no_seq) AS no_seq,isnull(s.no_sj2,s.no_sj) as no_sj2,case when s.nama_agent='88' then c.Nama_Customer WHEN ISNULL(s.nama_agent,'')='' then c.Nama_Customer else s.nama_agent end as nama_cust,s.no_dpb,s.No_Gudang_Out,CASE WHEN s.Almt_agen='88' THEN c.Alamat1 WHEN ISNULL(s.Almt_agen,'')='' then c.Alamat1 ELSE s.Almt_agen END as alamat_cust, d.no_sj,d.no_sp,d.Kd_stok,  case when ISNULL(d.nama_barang,'')= '' then b.Nama_Barang else d.nama_barang end as nama_barang,d.Kd_satuan,d.Qty_kirim,0 Qty_balik,d.Qty_kirim as qty_out,0 qty_sisa , s.kd_customer " +
                //" case when ISNULL(d.nama_barang,'')= '' then b.Nama_Barang else d.nama_barang end as nama_barang,d.Kd_satuan,d.Qty_kirim,0 Qty_balik,d.Qty_kirim as qty_out,0 qty_sisa " +
                " from SALES.dbo.SALES_SJ s inner join SALES.dbo.SALES_SJ_D d on s.no_sj = d.no_sj " +
                " inner join SIF.dbo.SIF_Barang b WITH(NOLOCK) on d.Kd_stok = b.Kode_Barang " +
                " inner join SIF.dbo.SIF_Customer c WITH(NOLOCK) on c.Kd_Customer = s.kd_customer " +
                " where s.no_sj=@no_sj AND ISNULL(status_sj, 'OPEN') = 'OPEN'";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sj", no_sj);

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res;
            }
        }


        public static async Task<int> SaveSJ(string no_krm, string no_so, string kdcabang,string pegawai,string no_transG, int jns_sj, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", kdcabang);
                param.Add("@rcn_krm", no_krm);
                param.Add("@no_sp", no_so);
                param.Add("@kdpeg", pegawai);
                param.Add("@No_GudangOut", no_transG);
                param.Add("@no_sj2", jns_sj);

                return conn.Execute("[SALES].[dbo].[BuatSuratJalan2]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> prodp_upd_krm_balik(string no_krm, string no_sp, string kdcabang, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                //param.Add("@vno_trans", kdcabang);
                param.Add("@vno_sp", no_sp);
                param.Add("@vno_trans", no_krm);


                return conn.Execute("[PROD].[dbo].[prodp_upd_krm_balik]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> CloseSp( string no_sp, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sp", no_sp);
                return conn.Execute("[SALES].[dbo].[CloseSp]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static async Task<int> SaveSJK(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SJ set status_sj='CLOSE',tgl_balik=GETDATE(), nama_agent=@nama_agent,Almt_agen=@Almt_agen,Tgl_terima=GETDATE(),Jam_terima=GETDATE(),ongkir=@ongkir,Last_update_date=GETDATE(),Last_updated_by=@Last_updated_by " +
            " where no_sj = @no_sj and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@nama_agent", data.nama_agent);
            param.Add("@Almt_agen", data.Almt_agen);
            param.Add("@ongkir",0);
            param.Add("@no_sj", data.no_sj);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> BatalSJK(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SJ set status_sj='BATAL', Last_update_date=GETDATE(),Last_updated_by=@Last_updated_by,Program_name=@Program_name   " +
            " where no_sj = @no_sj and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@Program_name", data.Program_name);  
            param.Add("@no_sj", data.no_sj);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> Update_SJ(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SJ set no_sj2=@no_sj2,nama_agent=@nama_agent,Almt_agen=@Almt_agen,Last_update_date=GETDATE(),Program_name='Edit Nama Brg SJ',Last_updated_by=@Last_updated_by " +
            " where no_sj=@no_sj and  Kd_Cabang=@Kd_cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@nama_agent", data.nama_agent);
            param.Add("@Almt_agen", data.Almt_agen);
            param.Add("@ongkir", 0);
            param.Add("@Last_updated_by", data.Last_updated_by);
            param.Add("@No_sp", data.No_sp);
            param.Add("@no_sj", data.no_sj);
            param.Add("@no_sj2", data.no_sj2);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
        public static async Task<int> Update_SJD(SALES_SJ_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SJ_D set nama_barang=@nama_barang,Last_update_date=GETDATE(),Last_updated_by=@Last_updated_by,Program_name='Edit Nama Brg SJ' " +
            " where Kd_stok=@Kd_stok and no_seq=@no_seq and No_sp=@No_sp and no_sj=@no_sj and Kd_Cabang=@Kd_cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@nama_barang", data.nama_barang);
            param.Add("@Kd_stok", data.Kd_stok);
            param.Add("@no_seq", data.no_seq);
            param.Add("@no_sj", data.no_sj);
            param.Add("@No_sp", data.No_sp);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSO_BATAL(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SO set STATUS_DO='PERSIAPAN BARANG', isClosed='N',pending='N', Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by,Program_Name=@Program_name  where No_sp = @no_sp and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@Program_Name", "Pembatalan SJK");
            param.Add("@no_sp", data.No_sp);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSO_D_BATAL(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SO_D set STATUS_DO='PERSIAPAN BARANG', Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by  where No_sp = @no_sp and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@Program_Name", "Pembatalan SJK");
            param.Add("@no_sp", data.No_sp);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSO_TERKIRIM(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SO set STATUS_DO='TERKIRIM',Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by,Program_Name=@Program_name  where No_sp = @no_sp and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@Program_Name", "Sudah SJK");
            param.Add("@no_sp", data.No_sp);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> UpdateSO_KIRIM(string no_sp,string Kd_cabang,string Last_updated_by, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SO set STATUS_DO='PROSES KIRIM',Last_Update_Date=GETDATE(), Last_Updated_by=@Last_updated_by,Program_Name=@Program_name  where No_sp = @no_sp and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", Kd_cabang);
            param.Add("@Program_Name", "Proses SJ");
            param.Add("@no_sp", no_sp);
            param.Add("@Last_updated_by",Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> DelGudangSJ(string kdcb, string No_Gudang_Out , SqlTransaction trans)
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

        public static async Task<int> DelGudangDtlSJ(string kdcb, string No_Gudang_Out, SqlTransaction trans)
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
        public static async Task<int> UpdateGudangSJ(string kdcb,string no_inv,string kd_stok,decimal noseq,decimal qty, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE INV.dbo.INV_GUDANG_OUT_D SET Program_Name='SJK' ,qty_out=@qty_out where no_trans = @no_trans and kd_stok=@kd_stok and  Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", kdcb);
           
            param.Add("@no_trans", no_inv);
           param.Add("@no_seq", noseq);
            param.Add("@kd_stok", kd_stok);
            param.Add("@qty_out", qty);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }
        public static async Task<int> DelSPM(SALES_SJ data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "delete PROD.dbo.PROD_rcn_krm_d where No_sp=@no_sp and Kd_Cabang=@Kd_Cabang";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@no_sp", data.No_sp);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }

        public static async Task<int> SaveSJKDetail(SALES_SJ_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "update SALES.dbo.SALES_SJ_D set Qty_balik=@Qty_balik,qty_out=@qty_out, Keterangan=@Keterangan,Last_update_date=GETDATE(),Last_updated_by=@Last_updated_by " +
            " where no_sj = @no_sj and Kd_stok=@Kd_stok and no_seq=@no_seq and Kd_cabang=@Kd_cabang ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_cabang);
            param.Add("@Qty_balik", data.Qty_balik);
            param.Add("@qty_out", data.qty_out);
            param.Add("@no_sj", data.no_sj);
            param.Add("@Kd_stok", data.Kd_stok);
            param.Add("@no_seq", data.no_seq);
            param.Add("@Keterangan", data.Keterangan);
            param.Add("@no_sj", data.no_sj);
            param.Add("@Last_updated_by", data.Last_updated_by);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;


        }

        public static async Task<int> UpdateSODetail(SALES_SJ_D data, SqlTransaction trans)
        {
            int res = 0;

            string Query = "";
            DynamicParameters param = new DynamicParameters();

            Query = "update sales.dbo.sales_so_d set QTY=@Qty where no_sp=@no_sp and Kd_Stok=@Kd_Stok";
            param = new DynamicParameters();
            param.Add("@no_sp", data.No_sp);
            param.Add("@Kd_Stok", data.Kd_stok);
            param.Add("@Qty", data.qty_out);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            return res;
        }


        public static async Task<IEnumerable<SALES_SJ>> GetMonSJ(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select j.Kd_cabang,cast(j.TglSJ as Date) as TglSJ ,j.Tgl_kirim,j.no_sj,j.no_dpb,CASE WHEN j.nama_agent='88' then c.Nama_Customer else j.nama_agent end as nama_customer,CASE WHEN j.Almt_agen='88' THEN c.Alamat1 ELSE j.Almt_agen END as alamat1,j.No_sp,CASE WHEN j.nama_agent='88' then c.Nama_Customer else j.nama_agent end as nama_agent,CASE WHEN j.Almt_agen='88' THEN c.Alamat1 ELSE j.Almt_agen END as Almt_agen,j.No_Gudang_Out,j.Last_created_by,j.Last_updated_by from SALES.dbo.SALES_SJ j WITH (NOLOCK) " +
                "left join SIF.dbo.SIF_Kendaraan k WITH (NOLOCK) on j.No_pol = k.Kode_Kendaraan  " +
                "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on j.kd_customer = c.Kd_Customer where ISNULL(j.status_sj,'OPEN') = 'OPEN' ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                if(DateTo != null)
                {
                    param.Add("@DateTo", DateTo.Value.AddDays(1));
                }
                param.Add("@kd_cabang", cb);

                if (no_sj != string.Empty && no_sj != null)
                {
                    
                    filter += " AND j.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                { 
                if (DateFrom != null)
                {
                   
                    filter += " AND TglSJ >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    
                    filter += " AND TglSJ <= @DateTo ";
                }
                }
                if (cb != string.Empty && cb != null)
                {
                  
                    filter += " AND j.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY j.Last_Create_Date DESC ";


                var res = con.Query<SALES_SJ>(sql, param);

                return res;
            }
        }

        public async static Task<List<SALES_SJ>> GetSJPartial(DateTime DateFrom, DateTime DateTo, string kdcb, string filterquery = "", string sortingquery = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";

                string sql = "select DISTINCT TOP " + seq + " isnull(j.status_sj,'OPEN') status_sj,j.Kd_cabang,cast(j.TglSJ as Date) as TglSJ ,j.Tgl_kirim,j.no_sj,j.no_dpb,c.Nama_Customer nama_customer,c.Alamat1 alamat1,j.No_sp,j.nama_agent,j.Almt_agen,j.No_Gudang_Out,j.Last_created_by,j.Last_updated_by,j.Last_create_date,j.Last_update_date " +
       "from SALES.dbo.SALES_SJ j WITH (NOLOCK)  " +
       "left join SIF.dbo.SIF_Kendaraan k WITH (NOLOCK) on j.No_pol = k.Kode_Kendaraan  " +
       "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on j.kd_customer = c.Kd_Customer " +
       "WHERE j.Kd_cabang=@Kd_Cabang ";


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
                    filter += " AND TglSJ >= @DateFrom ";
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
                    filter += " AND TglSJ <= @DateTo ";
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
                    filterquery = filterquery.Replace("status_po", "status_sj");
                    filterquery = filterquery.Replace("no_po", "no_sj");
                    filterquery = filterquery.Replace("tgl_po", "TglSJ");
                    // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    //filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    //filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
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
                    filterquery = filterquery.Replace("status_po", "status_sj");
                    filterquery = filterquery.Replace("no_po", "no_sj");
                    filterquery = filterquery.Replace("tgl_po", "TglSJ");
                    // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    //filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    //filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
                    filter += " " + filterquery + " ";

                }
                else
                {
                    sql += " ORDER BY j.Last_create_date DESC ";
                }


                var res = con.Query<SALES_SJ>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<Response> getCountSJ(DateTime DateFrom, DateTime DateTo, string filterquery = "", string kdcb = "", int seq = 0)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT count(*) as Result, no_sj " +
                   " FROM SALES.DBO.SALES_SJ WITH(NOLOCK) " +
                   " WHERE Kd_Cabang=@Kd_Cabang";


                DynamicParameters param = new DynamicParameters();
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@Kd_Cabang", kdcb);


                if (DateFrom != null)
                {


                    filter += " AND TglSJ >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    filter += " AND TglSJ <= @DateTo ";
                }

                if (filterquery != null && filterquery != "")
                {

                    filterquery = filterquery.Replace("status_po", "status_sj");
                    filterquery = filterquery.Replace("no_po", "no_sj");
                    filterquery = filterquery.Replace("tgl_po", "TglSJ");
                    // filterquery = filterquery.Replace("tgl_kirim", "PO.tgl_kirim");
                    //filterquery = filterquery.Replace("jml_rp_trans", "S.JML_VALAS_TRANS");
                    //filterquery = filterquery.Replace("total", "S.JML_VALAS_TRANS");
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
                sql += " GROUP BY no_sj ";

                var res = con.Query<Response>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }
        public static async Task<IEnumerable<SALES_SJ>> GetAll_SJ(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select isnull(j.status_sj,'OPEN') status_sj,j.Kd_cabang,cast(j.TglSJ as Date) as TglSJ ,j.Tgl_kirim,j.no_sj,j.no_dpb,c.Nama_Customer nama_customer,c.Alamat1 alamat1,j.No_sp,j.nama_agent,j.Almt_agen,j.No_Gudang_Out,j.Last_created_by,j.Last_updated_by,j.Last_create_date,j.Last_update_date " +
                "from SALES.dbo.SALES_SJ j WITH (NOLOCK)  " +
                "left join SIF.dbo.SIF_Kendaraan k WITH (NOLOCK) on j.No_pol = k.Kode_Kendaraan  " +
                "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on j.kd_customer = c.Kd_Customer  ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_cabang", cb);

                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " Where j.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                {
                    if (DateFrom != null)
                    {

                        filter += " Where TglSJ >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {

                        filter += " AND TglSJ <= @DateTo ";
                    }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND j.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY j.Last_Create_Date DESC ";


                var res = con.Query<SALES_SJ>(sql, param);

                return res;
            }
        }

        public static IEnumerable<SALES_SJ_D> GetDtlSJ(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select d.Kd_cabang,d.no_seq,s.TglSJ, d.no_sj,d.no_sp,d.Kd_stok,case when ISNULL(d.nama_barang,'')='' then b.Nama_Barang else d.nama_barang end as nama_barang ,d.Kd_satuan,d.Qty_kirim " +
                "from SALES.dbo.SALES_SJ_D d WITH (NOLOCK) left JOIN SALES.dbo.SALES_SJ s WITH (NOLOCK) on s.no_sj=d.no_sj LEFT JOIN SIF.dbo.SIF_Barang b on d.Kd_stok = b.Kode_Barang " +
                "where  ISNULL(s.status_sj,'OPEN') = 'OPEN' ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", cb);
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
               


                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " AND s.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                { 
                if (DateFrom != null)
                {

                    filter += " AND s.TglSJ >= @DateFrom ";
                }

                if (DateTo != null)
                {
                        param.Add("@DateTo", DateTo?.AddDays(1));
                        filter += " AND s.TglSJ <= @DateTo ";
                }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND d.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;
                sql += " order by d.no_sj,d.no_seq ASC ";

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res;
            }

        }

        public static async Task<IEnumerable<SALES_SJ>> GetMonSJK(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select isnull(s.inc_ongkir,'N') as inc_ongkir, j.Kd_cabang,cast(j.TglSJ as Date) as TglSJ ,j.Tgl_kirim,j.no_sj,j.no_dpb,c.Nama_Customer nama_customer,c.Alamat1 alamat1,j.No_sp,j.No_Gudang_Out,j.Last_created_by,j.Last_updated_by from SALES.dbo.SALES_SJ j  " +
                "left join SIF.dbo.SIF_Kendaraan k WITH (NOLOCK) on j.No_pol = k.Kode_Kendaraan  " +
                "left join SALES.dbo.SALES_SO s WITH (NOLOCK) on j.No_sp=s.No_sp " +
                "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on j.kd_customer = c.Kd_Customer where ISNULL(j.status_sj,'OPEN') = 'CLOSE' ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_cabang", cb);

                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " AND j.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                { 
                if (DateFrom != null)
                {

                    filter += " AND TglSJ >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    filter += " AND TglSJ <= @DateTo ";
                }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND j.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY j.Last_Create_Date DESC ";


                var res = con.Query<SALES_SJ>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<SALES_SJ>> GetMonSJ_del(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null, string jns_sj = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select j.Kd_cabang,cast(j.TglSJ as Date) as TglSJ ,j.Tgl_kirim,j.no_sj,j.no_dpb,c.Nama_Customer nama_customer,c.Alamat1 alamat1,j.No_sp,j.No_Gudang_Out,j.Last_created_by,j.Last_updated_by from SALES.dbo.SALES_SJ j WITH (NOLOCK)  " +
                "left join SIF.dbo.SIF_Kendaraan k WITH (NOLOCK) on j.No_pol = k.Kode_Kendaraan  " +
                "left join SIF.dbo.SIF_Customer c WITH (NOLOCK) on j.kd_customer = c.Kd_Customer ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@kd_cabang", cb);

                if (jns_sj == "SJK")
                {

                    filter += " where ISNULL(j.status_sj,'OPEN') = 'CLOSE' ";
                }
                else
                {
                    filter += " where ISNULL(j.status_sj,'OPEN') = 'OPEN' ";
                }

                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " AND j.no_sj LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                {
                    if (DateFrom != null)
                    {

                        filter += " AND TglSJ >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {

                        filter += " AND TglSJ <= @DateTo ";
                    }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND j.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;

                sql += " ORDER BY j.Last_Create_Date DESC ";


                var res = con.Query<SALES_SJ>(sql, param);

                return res;
            }
        }


        public static IEnumerable<SALES_SJ_D> GetDtlSJK(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT d.Kd_cabang,d.no_seq,s.TglSJ, d.no_sj,d.no_sp,d.Kd_stok,case when ISNULL(d.nama_barang,'')='' then b.Nama_Barang else d.nama_barang end as nama_barang,d.Kd_satuan,isnull(d.Qty_kirim,0) Qty_kirim,isnull(d.Qty_balik,0) Qty_balik,isnull(d.qty_out,0) qty_out  " +
                "from SALES.dbo.SALES_SJ_D d WITH (NOLOCK) left JOIN SALES.dbo.SALES_SJ s WITH (NOLOCK) on s.no_sj=d.no_sj LEFT JOIN SIF.dbo.SIF_Barang b WITH (NOLOCK) on d.Kd_stok = b.Kode_Barang " +
                "where  ISNULL(s.status_sj,'OPEN') = 'CLOSE' ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", cb);
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


                if (no_sj != string.Empty && no_sj != null)
                {
                   
                    filter += " AND s.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                { 
                if (DateFrom != null)
                {

                    filter += " AND s.TglSJ >= @DateFrom ";
                }

                if (DateTo != null)
                {

                    filter += " AND s.TglSJ <= @DateTo ";
                }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND d.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;
                sql += " order by d.no_sj,d.no_seq ASC ";

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res;
            }

        }

        public static IEnumerable<SALES_SJ_D> GetDtlSJ_del(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null, string jns_sj = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select d.Kd_cabang,d.no_seq,s.TglSJ, d.no_sj,d.no_sp,d.Kd_stok,b.Nama_Barang nama_barang,d.Kd_satuan,isnull(d.Qty_kirim,0) Qty_kirim,isnull(d.Qty_balik,0) Qty_balik,isnull(d.qty_out,0) qty_out " +
                "from SALES.dbo.SALES_SJ_D d WITH (NOLOCK) left JOIN SALES.dbo.SALES_SJ s WITH (NOLOCK) on s.no_sj=d.no_sj LEFT JOIN SIF.dbo.SIF_Barang b on d.Kd_stok = b.Kode_Barang ";
                

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", cb);
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);

                if (jns_sj == "SJK")
                {

                    filter += " where ISNULL(s.status_sj,'OPEN') = 'CLOSE' ";
                }
                else
                {
                    filter += " where ISNULL(s.status_sj,'OPEN') = 'OPEN' ";
                }


                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " AND s.no_sj LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                {
                    if (DateFrom != null)
                    {

                        filter += " AND s.TglSJ >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {

                        filter += " AND s.TglSJ <= @DateTo ";
                    }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND d.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;
                sql += " order by d.no_sj,d.no_seq ASC ";

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res;
            }

        }

        public static IEnumerable<SALES_SJ_D> GetAll_SJdtl(string no_sj = null, DateTime? DateFrom = null, DateTime? DateTo = null, string cb = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select  d.Kd_cabang,d.no_seq,s.TglSJ, d.no_sj,d.no_sp,d.Kd_stok,b.Nama_Barang nama_barang,d.Kd_satuan,isnull(d.Qty_kirim,0) Qty_kirim,isnull(d.Qty_balik,0) Qty_balik,isnull(d.qty_out,0) qty_out  " +
                "from SALES.dbo.SALES_SJ_D d WITH (NOLOCK) left JOIN SALES.dbo.SALES_SJ s WITH (NOLOCK) on s.no_sj=d.no_sj LEFT JOIN SIF.dbo.SIF_Barang b WITH (NOLOCK) on d.Kd_stok = b.Kode_Barang ";
                

                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_cabang", cb);
                param.Add("@no_sj", no_sj);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


                if (no_sj != string.Empty && no_sj != null)
                {

                    filter += " where s.no_sp LIKE CONCAT('%',@no_sj,'%') ";
                }
                else
                {
                    if (DateFrom != null)
                    {

                        filter += " where s.TglSJ >= @DateFrom ";
                    }

                    if (DateTo != null)
                    {

                        filter += " AND s.TglSJ <= @DateTo ";
                    }
                }
                if (cb != string.Empty && cb != null)
                {

                    filter += " AND d.Kd_Cabang=@kd_cabang ";
                }

                sql += filter;
                sql += " order by d.no_sj,d.no_seq ASC ";

                var res = con.Query<SALES_SJ_D>(sql, param);

                return res;
            }

        }
    }

}
