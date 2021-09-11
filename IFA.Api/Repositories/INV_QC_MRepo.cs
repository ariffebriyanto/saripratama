using Dapper;
using ERP.Api.Utils;
using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Repositories
{
    public class INV_QC_MRepo
    {
       
        public static IEnumerable<INV_QC_M> GetQC(string no_trans,string kd_gudang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select  brg.lokasi as Kd_Cabang,m.no_trans,m.no_ref,m.sj_supplier,m.p_np,d.kd_stok,brg.Nama_Barang,d.keterangan,d.kd_satuan,d.kd_ukuran,d.no_seq,d.qty_order,pod.qty as qty_po,isnull(d.qty_qc_pass, 0) as qty_qc_pass, pod.Bonus,d.qty_qc_unpass,d.qty_sisa,isnull(d.harga, 0) as harga,d.rp_trans,'00000' as gudang_asal,@kd_gudang as gudang_tujuan, m.jml_rp_trans,brg.rek_persediaan as kd_buku_besar,d.kd_buku_biaya" +
                             " from INV.dbo.INV_QC_M as m WITH (NOLOCK) INNER JOIN INV.dbo.INV_QC as d WITH (NOLOCK) on m.no_trans = d.no_trans inner JOIN SIF.dbo.SIF_Barang as brg on d.kd_stok = brg.Kode_Barang left join PURC.dbo.PURC_PO_D pod on pod.no_po = d.no_ref and pod.kd_stok = d.kd_stok and pod.no_seq = d.no_seq where d.no_trans = @no_trans";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@kd_gudang", kd_gudang);

                //if (kdgudang != string.Empty && kdgudang != null)
                //{
                //    sql += " m.tgl_trans >= @DateFrom ";
                //}

                    var res = con.Query<INV_QC_M>(sql,param);

                return res;
            }
        }

        public static async Task<int> SaveQC_M(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_QC_M]   (Kd_Cabang,tipe_trans,no_trans,tgl_trans,trx_stat,no_ref,keterangan,jml_rp_trans,p_np,jml_ppn,blthn,sj_supplier,qty_total,Last_Create_Date, " +
                "Last_Created_By,Program_Name,penyerah,qty,cetak_ke) " +
                    "VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@tgl_trans,@trx_stat,@no_ref,@keterangan,@jml_rp_trans,@p_np,@jml_ppn,@blthn,@sj_supplier,@qty_total,@Last_Create_Date, " +
                    " @Last_Created_By,@Program_Name,@penyerah,@qty,@cetak_ke);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_trans);
            param.Add("@trx_stat", 0);
            param.Add("@no_ref", data.no_ref);
            param.Add("@keterangan", data.keterangan);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@p_np", data.flag_ppn);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@blthn", data.blthn);
            param.Add("@sj_supplier", data.sj_supplier);
            param.Add("@qty_total", data.qty_total);//qty_total
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@penyerah", data.Last_Created_By);
            param.Add("@qty", 0);
            param.Add("@cetak_ke", 0);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int UpdateQC_M(PURC_PO data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [INV].[dbo].[INV_QC_M]    SET Kd_Cabang=@Kd_Cabang,tipe_trans=@tipe_trans,no_trans=@no_trans,tgl_trans=@tgl_trans,trx_stat=@trx_stat,no_ref=@no_ref,keteranga@keterangan,jml_rp_trans=@jml_rp_trans,p_np=@p_np,jml_ppn=@jml_ppn,blthn=@blthn,sj_supplier=@sj_supplier,Last_Create_Date=@Last_Create_Date, " +
                "Last_Created_By=@Last_Created_By,Program_Name=@Program_Name,penyerah=@penyerah,qty,cetak_ke) ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@tgl_trans", data.tgl_po);
            param.Add("@trx_stat", data.no_pr);
            param.Add("@no_ref", data.no_po);
            param.Add("@keterangan", data.keterangan);
            param.Add("@jml_rp_trans", data.jml_rp_trans);
            param.Add("@p_np", data.flag_ppn);
            param.Add("@jml_ppn", data.jml_ppn);
            param.Add("@blthn", data.tgl_kirim);
            param.Add("@sj_supplier", data.sj_supplier);
            param.Add("@qty_total", data.qty_total);//qty_total
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@penyerah", data.Last_Created_By);
            param.Add("@qty", 0);
            param.Add("@cetak_ke", 0);
            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

    }
}
