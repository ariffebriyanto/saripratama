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
    public class INV_GUDANG_IN_D_Repo
    {


        public static async Task<int> DeleteGudangDetail(string id, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "DELETE FROM [INV].[dbo].[INV_GUDANG_IN_D]   WHERE no_trans=@no_trans;";
            param = new DynamicParameters();
            param.Add("@no_trans", id);


            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<int> SaveGD_dtl_opn(INV_OPNAME_DTL data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_seq,kd_stok,keterangan,kd_satuan,kd_ukuran," +
                    " qty_sisa,Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn) " +
                    " VALUES(@Kd_Cabang,@kd_jenis,@no_trans,@no_seq,@kd_stok,@keterangan,@kd_satuan,@kd_ukuran," +
                    "@qty_sisa,@Last_Create_Date,@Last_Created_By,@Program_Name, @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@kd_jenis", data.kd_jenis);
            param.Add("@no_trans", data.no_trans);


            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@keterangan", "Adjustment Kartu vs Saldo");
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@qty_order", data.qty_opname);
            param.Add("@qty_in", data.qty_kartu);
            param.Add("@qty_sisa", data.qty_selisih);
            param.Add("@kd_buku_besar", data.rek_persediaan);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", "AdjustOpname");
            param.Add("@rp_trans", data.nilai_manual * data.qty_selisih);
            param.Add("@harga", data.nilai_rata);
            param.Add("@blthn", data.bultah);
            param.Add("@gudang_asal", data.kode_gudang);
            param.Add("@gudang_tujuan", data.kode_gudang);



            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }
        public static async Task<IEnumerable<INV_GUDANG_IN_D>> GetPODetail(string no_po = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT D.jml_diskon, D.keterangan,D.kd_stok, D.kd_satuan,D.no_po, D.no_seq, D.qty, D.harga, D.total, D.prosen_diskon, D.diskon2, D.diskon3, D.Diskon4, B.Nama_Barang AS nama_barang, S.Nama_Satuan AS satuan  " +
                            " FROM PURC.DBO.PURC_PO H WITH (NOLOCK) " +
                            " INNER JOIN PURC.DBO.PURC_PO_D D WITH (NOLOCK) ON D.no_po = H.no_po " +
                            " left outer  JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang " +
                            " left outer  JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan ";

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
                    filter += " H.no_po LIKE CONCAT('%',@no_po,'%') ";
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
                    filter += " H.tgl_po >= @DateFrom ";
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
                    filter += " H.tgl_po <= @DateTo ";
                }

                sql += filter;

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);

                return res;
            }
        }

        public static async Task<int> SaveGudangDetail(INV_GUDANG_IN_D data, SqlTransaction trans)
        {
            int res = 0;
            string Query = "";
            DynamicParameters param = new DynamicParameters();


            Query = "INSERT INTO [INV].[dbo].[INV_GUDANG_IN_D]   (Kd_Cabang,tipe_trans,no_trans,no_qc,no_seq,kd_stok,keterangan,kd_satuan,kd_ukuran," +
                    " qty_sisa,Last_Create_Date,Last_Created_By,Program_Name,qty_in,qty_order,harga,rp_trans,gudang_asal,gudang_tujuan,kd_buku_besar,kd_buku_biaya,blthn,no_ref) " +
                    " VALUES(@Kd_Cabang,@tipe_trans,@no_trans,@no_qc,@no_seq,@kd_stok,@keterangan,@kd_satuan,@kd_ukuran,@qty_sisa,@Last_Create_Date,@Last_Created_By,@Program_Name," +
                    " @qty_in,@qty_order,@harga,@rp_trans,@gudang_asal,@gudang_tujuan,@kd_buku_besar,@kd_buku_biaya,@blthn,@no_ref);";
            param = new DynamicParameters();
            param.Add("@Kd_Cabang", data.Kd_Cabang);
            param.Add("@tipe_trans", data.tipe_trans);
            param.Add("@no_trans", data.no_trans);
            param.Add("@no_qc", data.no_qc);
            param.Add("@no_seq", data.no_seq);
            param.Add("@kd_stok", data.kd_stok);
            param.Add("@keterangan", data.keterangan);
            param.Add("@kd_satuan", data.kd_satuan);
            param.Add("@kd_ukuran", data.kd_ukuran);
            param.Add("@qty_sisa", data.qty_sisa);
            param.Add("@Last_Create_Date", data.Last_Create_Date);
            param.Add("@Last_Created_By", data.Last_Created_By);
            param.Add("@Program_Name", data.Program_Name);
            param.Add("@qty_in", data.qty_in);
            param.Add("@qty_order", data.qty_order);
            param.Add("@harga", data.harga);
            param.Add("@rp_trans", data.rp_trans);
            param.Add("@gudang_asal", data.gudang_asal);
            param.Add("@gudang_tujuan", data.gudang_tujuan);
            param.Add("@kd_buku_besar", data.kd_buku_besar);
            param.Add("@kd_buku_biaya", data.kd_buku_biaya);
            param.Add("@blthn", data.blthn);
            param.Add("@no_ref", data.no_ref);

            res = trans.Connection.Query<int>(Query, param, transaction: trans).FirstOrDefault();

            return res;
        }

        public static async Task<IEnumerable<INV_GUDANG_IN_D>> GetDetGudangIn(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
 //               string sql = "SELECT D.Kd_Cabang,D.no_trans,D.no_qc,D.tipe_trans,D.kd_stok,B.Nama_Barang,D.kd_satuan,D.qty_order,D.qty_in,D.harga,D.rp_trans,D.blthn,D.gudang_asal,D.gudang_tujuan,G.Nama_Gudang as lokasi_simpan,GA.Nama_Gudang as nm_Gudang_asal,D.keterangan, B.berat *  qty_in as berat" +
 //                   " From [INV].[dbo].[INV_GUDANG_IN] GD"+
 //" left outer join[INV].[dbo].[INV_GUDANG_IN_D] D ON D.no_trans = GD.no_trans"+
 //                            " left outer JOIN SIF.dbo.SIF_Barang B ON D.kd_stok = B.Kode_Barang" +
 //                            " left outer JOIN SIF.dbo.SIF_Satuan S ON D.kd_satuan = S.Kode_Satuan" +
 //                            " left outer JOIN SIF.dbo.SIF_Gudang G ON D.gudang_tujuan = G.Kode_Gudang" +
 //                             " left outer JOIN SIF.dbo.SIF_Gudang GA ON D.gudang_asal = GA.Kode_Gudang" ;

                //ambil harga dari PO

                string sql = "SELECT D.Kd_Cabang,D.no_trans,D.no_qc,D.tipe_trans,D.kd_stok,B.Nama_Barang,D.kd_satuan,D.qty_order,D.qty_in,pod.diskon4,pod.total_new,pod.qty, pod.total,(cast(pod.total as real)/cast(pod.qty as real))as harga,D.rp_trans,D.blthn,g.Nama_Gudang AS gudang_asal,G1.Nama_Gudang as gudang_tujuan,D.keterangan, B.berat *  qty_in as berat " +
                "From[INV].[dbo].[INV_GUDANG_IN] GD WITH (NOLOCK) " +
                "LEFT OUTER JOIN[INV].[dbo].[INV_GUDANG_IN_D] D WITH (NOLOCK) ON D.no_trans = GD.no_trans " +
                "LEFT  JOIN PURC.dbo.PURC_PO_D pod WITH (NOLOCK) on GD.no_ref= pod.no_po and pod.no_seq= d.no_seq" +
                " LEFT OUTER JOIN SIF.dbo.SIF_Gudang G WITH(NOLOCK) ON G.Kode_Gudang=D.gudang_asal " +
                " LEFT OUTER JOIN SIF.dbo.SIF_Gudang G1 WITH(NOLOCK) ON G1.Kode_Gudang = D.gudang_tujuan " +
                "left outer JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang ";
            

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


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
                    filter += " D.no_trans LIKE CONCAT('%',@no_trans,'%') ";
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
                    filter += " GD.tgl_trans >= @DateFrom ";
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
                    filter += " GD.tgl_trans <= @DateTo ";
                }

                filter += " ORDER BY D.no_seq";
                sql += filter;

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);

                return res;
            }

        }


        public static async Task<IEnumerable<INV_GUDANG_IN_D>> GetDetGudangInView(string no_trans = null, DateTime? DateFrom = null, DateTime? DateTo = null, string status_po = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                //               string sql = "SELECT D.Kd_Cabang,D.no_trans,D.no_qc,D.tipe_trans,D.kd_stok,B.Nama_Barang,D.kd_satuan,D.qty_order,D.qty_in,D.harga,D.rp_trans,D.blthn,D.gudang_asal,D.gudang_tujuan,G.Nama_Gudang as lokasi_simpan,GA.Nama_Gudang as nm_Gudang_asal,D.keterangan, B.berat *  qty_in as berat" +
                //                   " From [INV].[dbo].[INV_GUDANG_IN] GD"+
                //" left outer join[INV].[dbo].[INV_GUDANG_IN_D] D ON D.no_trans = GD.no_trans"+
                //                            " left outer JOIN SIF.dbo.SIF_Barang B ON D.kd_stok = B.Kode_Barang" +
                //                            " left outer JOIN SIF.dbo.SIF_Satuan S ON D.kd_satuan = S.Kode_Satuan" +
                //                            " left outer JOIN SIF.dbo.SIF_Gudang G ON D.gudang_tujuan = G.Kode_Gudang" +
                //                             " left outer JOIN SIF.dbo.SIF_Gudang GA ON D.gudang_asal = GA.Kode_Gudang" ;

                //ambil harga dari PO

                string sql = "SELECT D.Kd_Cabang,D.no_trans,D.no_qc,D.tipe_trans,D.kd_stok,B.Nama_Barang,D.kd_satuan,D.qty_order,D.qty_in,pod.diskon4,pod.total_new,pod.qty, pod.total,(cast(pod.total as real)/cast(pod.qty as real))as harga,D.rp_trans,D.blthn,D.gudang_asal,D.gudang_tujuan,D.keterangan, B.berat *  qty_in as berat " +
                "From[INV].[dbo].[INV_GUDANG_IN] GD WITH (NOLOCK) " +
                "LEFT OUTER JOIN[INV].[dbo].[INV_GUDANG_IN_D] D WITH (NOLOCK) ON D.no_trans = GD.no_trans " +
                "LEFT  JOIN PURC.dbo.PURC_PO_D pod WITH (NOLOCK) on GD.no_ref= pod.no_po and pod.no_seq= d.no_seq " +
                "left outer JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang";

                DynamicParameters param = new DynamicParameters();
                param.Add("@no_trans", no_trans);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);


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
                    filter += " D.no_trans LIKE CONCAT('%',@no_trans,'%') ";
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
                    filter += " GD.tgl_trans >= @DateFrom ";
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
                    filter += " GD.tgl_trans <= @DateTo ";
                }


                sql += filter;

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);

                return res;
            }

        }

        public static async Task<int> Stprc_saldo(INV_GUDANG_IN_D data, SqlTransaction trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                int res = 0;
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@kd_cabang", data.Kd_Cabang, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@bultah", data.blthn, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_stok", data.kd_stok, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_satuan", data.kd_satuan, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@onstok_in", data.qty_in, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@vnilai", data.harga, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                var result = con.Execute("[INV].[dbo].[inv_onstok_saldo_in]", _params, null, null, CommandType.StoredProcedure);


                return res;
            }


        }

        public static async Task<int> Stprc_gudang(INV_GUDANG_IN_D data, SqlTransaction trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                int res = 0;
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@kd_cabang", data.Kd_Cabang, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@bultah", data.blthn, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_stok", data.kd_stok, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_satuan", data.kd_satuan, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kdgdng_awal", data.gudang_asal, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@kdgdng_akhir", data.gudang_tujuan, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@qty_in", data.qty_in, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@kd_ukuran", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@panjang", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@lebar", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@tinggi", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                var result = con.Execute("[INV].[dbo].[inv_stok_saldo_gudang]", _params, null, null, CommandType.StoredProcedure);


                return res;
            }


        }

        public static async Task<int> Stprc_gudangIn(INV_GUDANG_IN_D data, SqlTransaction trans)
        {
            using (var con = DataAccess.GetConnection())
            {
                int res = 0;
                DynamicParameters _params = new DynamicParameters();
                _params.Add("@kd_cabang", data.Kd_Cabang, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@bultah", data.blthn, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_stok", data.kd_stok, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kd_ukuran", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@kd_satuan", data.kd_satuan, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@kode_gudang", data.gudang_tujuan, dbType: DbType.String, direction: ParameterDirection.Input);
                _params.Add("@tinggi", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@panjang", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@lebar", 0.00, dbType: DbType.Decimal, direction: ParameterDirection.Input);
                _params.Add("@qty_in", data.qty_in, dbType: DbType.Decimal, direction: ParameterDirection.Input);

                var result = con.Execute("[INV].[dbo].[inv_stok_saldo_gudang_in]", _params, null, null, CommandType.StoredProcedure);


                return res;
            }


        }

        public static async Task<IEnumerable<INV_GUDANG_IN_D>> GetMonitoringMts(string kdcb,string no_trans=null, string kd_stok = null, DateTime? DateFrom = null, DateTime? DateTo = null)
        {
            using (var con = DataAccess.GetConnection())
            {
                string filter = "";
                string sql = "SELECT D.Kd_Cabang,D.no_seq,D.no_trans,D.no_qc,D.tipe_trans,D.kd_stok,B.Nama_Barang,D.kd_satuan,D.qty_order,D.qty_in,D.harga,D.rp_trans,D.blthn,D.gudang_asal,D.gudang_tujuan,G.Nama_Gudang as lokasi_simpan,D.keterangan,GD.Last_Created_By, G1.Nama_Gudang as Nama_Gudang  " +
                             " From [INV].[dbo].[INV_GUDANG_IN] GD WITH (NOLOCK)" +
                            " left outer join[INV].[dbo].[INV_GUDANG_IN_D] D WITH (NOLOCK) ON D.no_trans = GD.no_trans" +
                            " left outer JOIN SIF.dbo.SIF_Barang B WITH (NOLOCK) ON D.kd_stok = B.Kode_Barang" +
                            " left outer JOIN SIF.dbo.SIF_Satuan S WITH (NOLOCK) ON D.kd_satuan = S.Kode_Satuan" +
                            " left outer JOIN SIF.dbo.SIF_Gudang G WITH (NOLOCK) ON D.gudang_tujuan = G.Kode_Gudang " +
                            " left outer JOIN SIF.dbo.SIF_Gudang G1 WITH (NOLOCK) ON D.gudang_asal = G1.Kode_Gudang  " +
                            " where GD.no_trans LIKE '%BM%' and isnull(GD.rec_stat,'Y')='Y'  " +
                            " and GD.Kd_Cabang=@kdcb ";

                DynamicParameters param = new DynamicParameters();
           //     param.Add("@kd_stok", kd_stok);
                param.Add("@kdcb", kdcb);
                param.Add("@DateFrom", DateFrom);
                param.Add("@DateTo", DateTo);
                param.Add("@no_trans", no_trans);


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
                    filter += " GD.tgl_trans >= @DateFrom "; // convert(date,'" + DateFrom + "',103)  ";
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
                    filter += " GD.tgl_trans <= @DateTo ";// convert(date,'" + DateTo + "',103) ";
                }

                if (no_trans != null && no_trans !="")
                {
                    if (filter == "")
                    {
                        //filter += " WHERE ";
                    }
                    else
                    {
                        filter += " AND ";
                    }
                    filter += " GD.no_trans = @no_trans ";// convert(date,'" + DateTo + "',103) ";
                }

                filter += " ORDER BY GD.Last_Create_Date DESC ";

                sql += filter;

                var res = con.Query<INV_GUDANG_IN_D>(sql, param);
                //var res = con.Query<INV_GUDANG_IN_D>(sql, param);
                return res;
            }
        }
      
    }
}
