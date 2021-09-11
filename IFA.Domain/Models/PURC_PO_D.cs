using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class PURC_PO_D
    {
      
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }
    
        public string no_po { get; set; }
       
        public string no_trans { get; set; }
       
        public int no_seq { get; set; }
      
        public string kd_stok { get; set; }

        public string spek_brg { get; set; }

        public string kd_satuan { get; set; }

        public string satuan_beli { get; set; }
       
        public decimal qty { get; set; }

        public decimal? harga { get; set; }

        public decimal? harga_raw { get; set; }

        public decimal? prosen_diskon { get; set; }

        public decimal? diskon2 { get; set; }

        public decimal? diskon3 { get; set; }

        public decimal? jml_diskon { get; set; }

        public decimal? total { get; set; }

        public string keterangan { get; set; }

        public DateTime? tgl_kirim { get; set; }
        public DateTime? tgl_trans { get; set; }

        public decimal? qty_kirim { get; set; }

        public decimal? qty_qc_pass { get; set; }

        public decimal? qty_qc_unpass { get; set; }
        public decimal? qty_order { get; set; }
        public string gudang_asal { get; set; }
        public string gudang_tujuan { get; set; }

        public decimal? hold { get; set; }

        public decimal? qty_sisa { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string jasa { get; set; }

        public int inv_stat { get; set; }

        public string flag_bonus { get; set; }

        public decimal? Diskon4 { get; set; }

        public string Bonus { get; set; }

        public decimal? harga_new { get; set; }

        public decimal? diskon1_new { get; set; }

        public decimal? diskon2_new { get; set; }

        public decimal? diskon3_new { get; set; }

        public decimal? diskon4_new { get; set; }

        public decimal? total_new { get; set; }

        public decimal? jml_diskon_new { get; set; }
        public string nama_barang { get; set; }
        public string satuan { get; set; }


        public string pdm { get; set; }

        public string blthn { get; set; } 
        public string status_hold { get; set; }
        public string kd_buku_besar { get; set; }
        public string kd_buku_biaya { get; set; }
        public string lokasi { get; set; }
        public string kd_supplier { get; set; }
        public string nama_Supplier { get; set; }
        public string nama_Gudang { get; set; }
        public decimal? last_price { get; set; }
        public string no_dpm { get; set; }

        public decimal? harga1 { get; set; }
        public decimal? harga4 { get; set; }

        public string flag_ppn { get; set; }

    }

}
