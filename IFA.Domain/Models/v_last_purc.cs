using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class v_last_purc
    {
        public string no_po { get; set; }

        public DateTime? tgl_po { get; set; }

        public string Kode_Supplier { get; set; }

        public string Nama_Supplier { get; set; }

        public string Nama_Jenis { get; set; }

        public string Kode_Barang { get; set; }

        public string Nama_Barang { get; set; }

        public decimal qty { get; set; }

        public decimal harga { get; set; }

        public decimal? total { get; set; }

        public decimal? qty_kirim { get; set; }

        public string sj_supplier { get; set; }

        public DateTime? tgl_po_asli { get; set; }

        public string no_trans { get; set; }

        public decimal harga_new { get; set; }

        public decimal? total_new { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal? jml_diskon_new { get; set; }

        public decimal? ppn { get; set; }

        public decimal? totalppn { get; set; }

        public string status_po { get; set; }
        public string keterangan { get; set; }
        public DateTime? tgl_kirim { get; set; }
        public decimal? qty_sisa { get; set; }

    }

    public class v_last_purcdownload
    {
        public string no_po { get; set; }

        public DateTime? tgl_po { get; set; }

        public string Kode_Supplier { get; set; }

        public string Nama_Supplier { get; set; }

        public string Nama_Jenis { get; set; }

        public string Kode_Barang { get; set; }

        public string Nama_Barang { get; set; }

        public decimal qty { get; set; }
        public decimal? qty_kirim { get; set; }

        public decimal harga { get; set; }

        public decimal? total { get; set; }
        public decimal? jml_diskon { get; set; }
        public decimal? jml_diskon_new { get; set; }
        public decimal? ppn { get; set; }
        public decimal? totalppn { get; set; }
        public string status_po { get; set; }
        public string keterangan { get; set; }
        public DateTime? tgl_kirim { get; set; }
        public decimal? qty_sisa { get; set; }

    }
}
