using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class vBarangPOTerpilih
    {
        public string no_po { get; set; }

        public string kd_stok { get; set; }

        public string kd_satuan { get; set; }

        public string Nama_Barang { get; set; }

        public decimal qty { get; set; }

        public decimal harga { get; set; }

        public decimal total { get; set; }

        public string kd_supplier { get; set; }

        public decimal? qty_total { get; set; }

        public string keterangan { get; set; }

        public decimal? prosen_diskon { get; set; }

        public decimal? diskon2 { get; set; }

        public decimal? diskon3 { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal? Diskon4 { get; set; }

        public int no_seq { get; set; }

        public decimal harga_new { get; set; }

        public decimal diskon1_new { get; set; }

        public decimal diskon2_new { get; set; }

        public decimal diskon3_new { get; set; }

        public decimal diskon4_new { get; set; }

        public decimal jml_diskon_new { get; set; }

        public decimal total_new { get; set; }

        public decimal qty_retur { get; set; }

        public decimal retur_total { get; set; }


    }
}
