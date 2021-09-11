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
    public class INV_QCRepo
    {

        public static List<INV_QC> GetNotransCbo( string kd_cabang = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string filter = "";
                string sql = "select DISTINCT(qcm.no_trans),qcm.Last_Create_Date,sup.Nama_Supplier,po.flag_ppn as p_np,po.no_po from INV.dbo.INV_QC qc" +
                             " LEFT JOIN INV.dbo.INV_QC_M qcm on qcm.no_trans = qc.no_trans" +
                             " left join purc.dbo.PURC_PO po on po.no_po = qcm.no_ref" +
                             " left join PURC.dbo.PURC_DPM as dpm on dpm.No_DPM = po.no_po" +
                             " left join sif.dbo.SIF_SUPPLIER sup on sup.Kode_Supplier = po.kd_supplier " +
                             " WHERE  qcm.trx_stat = 0 " +
                             " order by qcm.Last_Create_Date asc ";
                           

                DynamicParameters param = new DynamicParameters();
                //param.Add("@kd_cabang", kd_cabang);
                //if (kd_cabang != string.Empty && kd_cabang != null)
                //{
                //    if (filter == "")
                //    {
                //        filter += " ";
                //    }
                //    else
                //    {
                //        filter += " AND ";
                //    }
                //    filter += " WHERE  qcm.trx_stat = 0"; //qcm.Kd_Cabang =@kd_cabang AND
                //}
                //sql += filter;

                var res = con.Query<INV_QC>(sql, param);

                return res.ToList();
            }
        }

        public static List<INV_QC> GetListPOCbo(string kd_cabang= null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select distinct po.no_po,sup.Nama_Supplier from PURC.dbo.PURC_PO po INNER JOIN PURC.dbo.PURC_PO_D pod on pod.no_po=po.no_po " +
                    "INNER JOIN SIF.dbo.SIF_Supplier sup on sup.Kode_Supplier=po.kd_supplier " +
                    "LEFT JOIN INV.dbo.v_saldo_qc_pass qc ON  qc.no_po= pod.no_po and qc.kd_stok=pod.kd_stok and qc.no_seq=pod.no_seq " +
                    "WHERE isnull(po.rec_stat,'')='APPROVE' and isnull(po.status_po,'')<>'BATAL'  and isnull(po.status_po,'')<>'CLOSE' " +
                    "and isnull(po.isClosed,'T')<>'Y' and isnull(qc.qty_order,0)>0 order by po.no_po"; //and po.kd_cabang=@kd_cabang


                DynamicParameters param = new DynamicParameters();
                //param.Add("@kd_cabang", kd_cabang);
                var res = con.Query<INV_QC>(sql, param);

                return res.ToList();
            }
        }

        public static IEnumerable<PURC_PO_D> GetPO(string no_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "select a.* from(select po.kd_supplier,po.no_po,brg.Nama_Barang ,po.flag_ppn,pod.no_seq,pod.kd_stok,pod.keterangan,pod.kd_satuan,pod.harga,po.Last_Create_Date,isnull((select qty_order from INV.dbo.v_saldo_qc_pass where no_po= po.no_po and kd_stok=pod.kd_stok and no_seq=pod.no_seq),0)as qty_ordered, pod.qty,pod.qty_sisa, brg.kd_ukuran,brg.lokasi,brg.rek_persediaan,brg.rek_biaya, 0 as qty_qc_pass, 0 as qty_qc_unpass from PURC.DBO.PURC_PO_D pod inner join PURC.DBO.PURC_PO po on pod.no_po=po.no_po inner join SIF.dbo.SIF_Barang brg on pod.kd_stok=brg.Kode_Barang where pod.no_po=@no_po )a where a.qty_ordered is not null ORDER BY a.no_seq ASC";
                string sql = "select a.* from(select po.kd_supplier,po.no_po,sup.Nama_Supplier,brg.Nama_Barang ,po.flag_ppn,pod.no_seq,pod.kd_stok,pod.keterangan,pod.kd_satuan,(cast(pod.total as real)/cast(pod.qty as real)) as harga,po.Last_Create_Date,isnull((select qty_order from INV.dbo.v_saldo_qc_pass where no_po= po.no_po and kd_stok=pod.kd_stok and no_seq=pod.no_seq),0)as qty_order, pod.qty,isnull((select qty_order from INV.dbo.v_saldo_qc_pass where no_po= po.no_po and kd_stok=pod.kd_stok and no_seq=pod.no_seq),0)as qty_qc_pass, brg.kd_ukuran,'00000' as lokasi,brg.rek_persediaan as kd_buku_besar,brg.rek_biaya,  0 as qty_qc_unpass,0 as qty_sisa, G.Nama_Gudang from PURC.DBO.PURC_PO_D pod inner join PURC.DBO.PURC_PO po on pod.no_po=po.no_po inner join SIF.dbo.SIF_Supplier sup on sup.Kode_Supplier=po.kd_supplier inner join SIF.dbo.SIF_Barang brg on pod.kd_stok=brg.Kode_Barang INNER JOIN SIF.dbo.SIF_GUDANG G ON G.Kode_Gudang='00000' where pod.no_po=@no_po )a where a.qty_order is not null ORDER BY a.no_seq ASC";
               

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_po", no_po);

                var res = con.Query<PURC_PO_D>(sql, param);

                return res;
            }
        }

        public static async Task<IEnumerable<INV_QC>> GetDetailQC(string no_trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select  brg.lokasi as Kd_Cabang,m.no_trans,m.no_ref,m.sj_supplier,m.p_np,d.kd_stok,brg.Nama_Barang,m.keterangan,d.kd_satuan,d.kd_ukuran,d.no_seq,d.qty_order,pod.qty as qty_po,isnull(d.qty_qc_pass, 0) as qty_qc_pass, pod.Bonus,d.qty_qc_unpass,d.qty_sisa,isnull(d.harga, 0) as harga,d.rp_trans,d.gudang_asal,m.jml_rp_trans,d.kd_buku_besar,d.kd_buku_biaya" +
                             " from INV.dbo.INV_QC_M as m INNER JOIN INV.dbo.INV_QC as d on m.no_trans = d.no_trans inner JOIN SIF.dbo.SIF_Barang as brg on d.kd_stok = brg.Kode_Barang left join PURC.dbo.PURC_PO_D pod on pod.no_po = d.no_ref and pod.kd_stok = d.kd_stok and pod.no_seq = d.no_seq where d.no_trans = @no_trans";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                var res = con.Query<INV_QC>(sql, param);
                return res;
            }
        }

        public static IEnumerable<INV_QC> GetSavedQC(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select  m.no_trans,m.no_ref,m.sj_supplier,po.kd_supplier,sp.Nama_Supplier,m.p_np,g.Nama_Gudang,d.kd_stok,brg.Nama_Barang,m.keterangan,d.kd_satuan,d.kd_ukuran,d.no_seq,d.qty_order as qty,pod.qty as qty_po,isnull(d.qty_qc_pass, 0) as qty_qc_pass, pod.Bonus,d.qty_qc_unpass,d.qty_sisa,isnull(d.harga, 0) as harga,d.rp_trans,d.gudang_asal,d.gudang_tujuan,m.jml_rp_trans,d.kd_buku_besar,d.kd_buku_biaya " +
"from INV.dbo.INV_QC_M as m INNER JOIN INV.dbo.INV_QC as d on m.no_trans = d.no_trans inner JOIN SIF.dbo.SIF_Barang as brg on d.kd_stok = brg.Kode_Barang left join PURC.dbo.PURC_PO_D pod on pod.no_po = d.no_ref and pod.kd_stok = d.kd_stok and pod.no_seq = d.no_seq " +
"inner join SIF.dbo.SIF_Gudang g on g.Kode_Gudang = d.gudang_tujuan inner join PURC.dbo.PURC_PO po on po.no_po = m.no_ref inner join SIF.dbo.SIF_Supplier sp on sp.Kode_Supplier = po.kd_supplier where m.no_trans = @no_trans";
                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);

                if (no_trans != string.Empty && no_trans != null)
                {
                    if (filter == "")
                    {
                        filter += " AND ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    //filter += " m.no_trans LIKE CONCAT('%',@no_trans,'%') ";
                    filter += " m.no_trans = @no_trans ";
                }
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
                    filter += " m.tgl_trans >= @DateFrom ";
                }

                if (DateTo != null)
                {
                    if (filter == "")
                    {
                        filter += " AND ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " m.tgl_trans <= @DateTo ";
                }


                sql += filter;

                var res = con.Query<INV_QC>(sql, param);

                return res;

                //var res = con.Query<INV_QC>(sql, param);
                //return res;
            }
        }

        public static IEnumerable<INV_QC> GetMonitoringQC(string kd_stok = null, string DateFrom = null, string DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "select m.Last_Create_Date, case when m.trx_stat = 1 then 'Stocked' else 'QC' END as doc_status,m.no_trans,m.no_ref,m.sj_supplier,po.kd_supplier,sp.Nama_Supplier,m.p_np,g.Nama_Gudang,d.kd_stok,brg.Nama_Barang,m.keterangan,d.kd_satuan,d.kd_ukuran,d.no_seq,d.qty_order as qty,pod.qty as qty_po,isnull(d.qty_qc_pass, 0) as qty_qc_pass, pod.Bonus,d.qty_qc_unpass,d.qty_sisa,isnull(d.harga, 0) as harga,d.rp_trans,d.gudang_asal,d.gudang_tujuan,m.jml_rp_trans,d.kd_buku_besar,d.kd_buku_biaya " +
"from INV.dbo.INV_QC_M as m WITH (NOLOCK) INNER JOIN INV.dbo.INV_QC as d WITH (NOLOCK) on m.no_trans = d.no_trans inner JOIN SIF.dbo.SIF_Barang as brg WITH (NOLOCK) on d.kd_stok = brg.Kode_Barang left join PURC.dbo.PURC_PO_D pod WITH (NOLOCK) on pod.no_po = d.no_ref and pod.kd_stok = d.kd_stok and pod.no_seq = d.no_seq " +
"inner join SIF.dbo.SIF_Gudang g WITH (NOLOCK) on g.Kode_Gudang = d.gudang_tujuan inner join PURC.dbo.PURC_PO po WITH (NOLOCK) on po.no_po = m.no_ref inner join SIF.dbo.SIF_Supplier sp WITH (NOLOCK) on sp.Kode_Supplier = po.kd_supplier ";
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
                //param.Add("@DateFrom", DateFrom);
                //param.Add("@DateTo", DateTo);

                if (kd_stok != string.Empty && kd_stok != null)
                {
                    if (filter == "")
                    {
                        filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " d.kd_stok LIKE CONCAT('%',@kd_stok,'%') ";
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
                    filter += " m.tgl_trans >= convert(date,'"+ DateFrom +"',103)  ";
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
                    filter += " m.tgl_trans <= convert(date,'" + DateTo + "',103) ";
                }


                sql += filter;
                sql += " order by m.Last_Create_Date desc ";

                var res = con.Query<INV_QC>(sql);

                return res;

                //var res = con.Query<INV_QC>(sql, param);
                //return res;
            }
        }

        public static int DeleteQCDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [INV].[dbo].[INV_QC]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<int> SaveQCDetail(PURC_PO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_QC]   (Kd_Cabang,tipe_trans,no_trans,no_ref,no_seq,kd_stok,spek_brg,kd_satuan," +
                    " qty_order,harga,qty_qc_pass,qty_qc_unpass,qty_sisa,blthn,kd_buku_besar,kd_buku_biaya,keterangan,gudang_tujuan," +
                    " Last_Create_Date, Last_Created_By,Program_Name) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_ref,@no_seq,@kd_stok,@spek_brg,@kd_satuan, " +
                    " @qty_order,@harga,@qty_qc_pass,@qty_qc_unpass,@qty_sisa,@blthn,@kd_buku_besar,@kd_buku_biaya,@keterangan,@gudang_tujuan, " +
                    " @Last_Create_Date, @Last_Created_By, @Program_Name);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_ref", data.no_po);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@kd_satuan", data.kd_satuan);
            
            param.Add("@qty_order", data.qty_order);
            param.Add("@harga", data.harga);
            param.Add("@qty_qc_pass", data.qty_qc_pass);
            param.Add("@qty_qc_unpass", data.qty_qc_unpass);
            param.Add("@qty_sisa", data.qty_sisa);
           
            param.Add("@blthn", data.blthn);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@keterangan", data.keterangan);
            param.Add("@gudang_tujuan", data.gudang_tujuan);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int UpdateQCDetail(PURC_PO_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "UPDATE [INV].[dbo].[INV_QC]   SET Kd_Cabang=@Kd_Cabang,tipe_tran@tipe_trans,no_trans=@no_trans,no_seq=@no_seq,kd_stok=@kd_stok,spek_brg=@spek_brg,kd_satuan=@kd_satuan," +
                    " qty_order=@qty,harga=@harga,qty_qc_pass=@qty_qc_pass,qty_qc_unpass=@qty_qc_unpass,qty_sisa=@qty_sisa,hold=@hold,hold_po=@hold_po,status_hold=@status_hold,blthn=@blthn,kd_buku_besar=@kd_buku_besar,kd_buku_biaya=@kd_buku_biaya,keterangan=@keterangan,Last_Create_Date=@Last_Create_Date," +
                    " Last_Created_By=@Last_Created_By,Program_Name=@Program_Name ";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@spek_brg", data.spek_brg);
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@qty_order", data.qty);
            param.Add("@harga", data.harga);
            param.Add("@qty_qc_pass", data.qty_qc_pass);
            param.Add("@qty_qc_unpass", data.qty_qc_pass);
            param.Add("@qty_sisa", data.qty_sisa);
            param.Add("@blthn", data.blthn);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@keterangan", data.tgl_kirim);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static int UpdateStatPO(PURC_PO_D data, SqlTransaction trans, SqlConnection con)
        {
            int res = 0;
            string Query = "";
            string tutupPO = "";
            IEnumerable<PURC_PO_D> list = new List<PURC_PO_D>();
             DynamicParameters param1 = new DynamicParameters();

            DynamicParameters param = new DynamicParameters();
            string sql = "select sum(qty) as t_po, sum(qty_qc_pass) as t_qc, case when sum(qty)=sum(qty_qc_pass) then 'Y' else 'T' END as blthn from purc.dbo.purc_po_d po WITH (NOLOCK) join inv.dbo.INV_QC q WITH (NOLOCK) on po.no_po=q.no_ref " +
                 "where q.no_trans = @no_trans and po.no_po = @no_po";

            param1 = new DynamicParameters();
            param1.Add("@no_trans", data.no_trans);
            param1.Add("@no_po", data.no_po);
             list = con.Query<PURC_PO_D>(sql, param);
            if (list.FirstOrDefault().blthn == "Y")
            {
                Query = "update purc.dbo.purc_po set isClosed = '" + tutupPO + "' where no_po = @no_po";
                param = new DynamicParameters();
                param.Add("@no_po", data.no_po);
                res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();
            }
            return res;
        }

        public static async Task<int> SPStatusPO(string notrans, string no_po, SqlTransaction trans, SqlConnection conn)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@xno_po", no_po);
                param.Add("@no_qc", notrans);

                return conn.Execute("[PURC].[dbo].[proses_isClosed]", param, trans, null, CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw;
            }
        }

        public static List<V_StokGudang> GetKartuStok(string kd_stok, string blth,string kd_cabang, bool filterTahun)
        {
            using (var con = DataAccess.GetConnection())
            {
                //string sql = "select * from AMERICAN_REPORT.DBO.vy_stokawal where kode_barang=@kd_stok and bultah=@blth ";
                string sql = "select s.kd_stok as Kode_Barang,brg.Nama_Barang,s.bultah,s.awal_qty_onstok from INV.dbo.INV_STOK_SALDO s WITH (NOLOCK) " +
                            "INNER JOIN SIF.dbo.SIF_Barang brg WITH (NOLOCK) on brg.Kode_Barang = s.kd_stok " +
                            " where s.kd_stok=@kd_stok and s.bultah=@blth and s.Kd_Cabang=@kd_cabang";



                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
                param.Add("@kd_cabang", kd_cabang);
                if (filterTahun == true)
                {
                    param.Add("@blth",  blth + "01");
                }
                else
                {
                    param.Add("@blth", blth);
                }

                var res = con.Query<V_StokGudang>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static List<vy_saldocard> GetKartuStokDetail(string kd_stok, string blth, bool filterTahun)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from AMERICAN_REPORT.DBO.vy_saldocard where kd_stok=@kd_stok ";
                if (filterTahun == true)
                {
                    sql += " and SUBSTRING(bultah,1,4)=@blth  order by sorttanggal";
                }
                else
                {
                    sql += " and bultah=@blth  order by sorttanggal";
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
                param.Add("@blth", blth);

                var res = con.Query<vy_saldocard>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }
        public static List<vy_saldocard> GetKartuStokDetailGudang(string kd_stok, string blth, bool filterTahun, string gudang)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "select * from AMERICAN_REPORT.DBO.[vy_saldocard] where kd_stok=@kd_stok and gudang=@gudang ";
                if (filterTahun == true)
                {
                    sql += " and SUBSTRING(bultah,1,4)=@blth  order by sorttanggal";
                }
                else
                {
                    sql += " and bultah=@blth  order by sorttanggal";
                }
                DynamicParameters param = new DynamicParameters();
                param.Add("@kd_stok", kd_stok);
                param.Add("@blth", blth);
                param.Add("@gudang", gudang);

                var res = con.Query<vy_saldocard>(sql, param, null, true, 36000);

                return res.ToList();
            }
        }

        public static PrintQC GetPrintQC(string no_qc = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT qcm.tgl_trans, qc.no_trans,po.no_po, qc.no_ref, qcm.sj_supplier,qcm.cetak_ke as cetak_ke, p.Nama_Pegawai AS petugas, qc.no_seq, brg.Nama_Barang nama_barang ,  " +
                            "qc.spek_brg, qc.kd_satuan AS satuan, qc.qty_order, qc.hold, qc.qty_qc_pass, qc.qty_qc_unpass, qc.qty_sisa, sp.Nama_Supplier as NamaSupplier,sp.Alamat1 as AlamatPengiriman  " +
                            "FROM INV.dbo.INV_QC AS qc WITH (NOLOCK)  " +
                            "inner JOIN INV.dbo.INV_QC_M AS qcm WITH (NOLOCK) ON qc.no_trans = qcm.no_trans " +
                            "inner JOIN SIF.dbo.SIF_Barang AS brg WITH (NOLOCK)  ON qc.kd_stok = brg.Kode_Barang " +
                            "left JOIN SIF.dbo.MUSER AS m WITH (NOLOCK) ON m.userid = qc.Last_Created_By " +
                            "left JOIN SIF.dbo.SIF_Pegawai AS p WITH (NOLOCK) ON p.Kode_Pegawai = m.nama " +
                            "left join purc.dbo.PURC_PO po WITH (NOLOCK) on po.no_po = qcm.no_ref " +
                            "left join sif.dbo.SIF_SUPPLIER sp WITH (NOLOCK) on sp.Kode_Supplier = po.kd_supplier " +
                            " WHERE qcm.no_trans = @no_qc ";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_qc", no_qc);

                var res = con.Query<PrintQC>(sql, param);

                return res.FirstOrDefault();
            }
        }

        public static vy_profile getProfile()
        {
            using (var con = DataAccess.GetConnection())
            {
                string sql = "SELECT vyc_saldocard.nama, vyc_saldocard.alamat, vyc_saldocard.kota, vyc_saldocard.propinsi, vyc_saldocard.telp1, vyc_saldocard.fax1 " +
                            " FROM AMERICAN_REPORT.dbo.vyc_saldocard vyc_saldocard";

                DynamicParameters param = new DynamicParameters();
                

                var res = con.Query<vy_profile>(sql, param);

                return res.FirstOrDefault();
            }
        }
    }
}
