using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_HUTANG_USAHA
    {
    }

    public class FIN_HUTANG_USAHA_Header
    {
        public int nomer { get; set; }
        public string kd_supplier { get; set; }
        public decimal sisa { get; set; }
        public string nama_supplier { get; set; }

    }

    public class FIN_HUTANG_USAHA_Filter
    {
        public DateTime tanggal { get; set; }
        public string kd_cust { get; set; }
        public string no_trans { get; set; }
        public string tipe { get; set; }

    }

    public class FIN_HUTANG_USAHA_Detail
    {
        public string sts_ppn { get; set; }
        public string no_inv { get; set; }
        public DateTime? tgl_inv { get; set; }
        public string no_jurnal { get; set; }
        public DateTime? tgl_posting { get; set; }
        public string keterangan { get; set; }
        public decimal? jml_tagihan { get; set; }
        public decimal? jml_bayar { get; set; }
        public decimal? jml_akhir { get; set; }
        public string kd_cust { get; set; }
        public decimal? jml_bayar_pending { get; set; }
        public DateTime? tgl_jth_tempo { get; set; }
        public string no_ref1 { get; set; }

    }

    public class FIN_HUTANG_USAHA_Penjualan
    {
        public string no_inv { get; set; }

        public string no_sp { get; set; }

        public string kd_stok { get; set; }

        public string nama_barang { get; set; }

        public decimal? qty { get; set; }

        public decimal? harga { get; set; }

        public decimal? total { get; set; }
        public decimal? jml_ppn { get; set; }

    }
}
