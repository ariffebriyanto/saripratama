using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_GUDANG_OUT_D
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_trans { get; set; }

        public string no_ref { get; set; }

        public string no_ref2 { get; set; }

        public DateTime? tgl_ref2 { get; set; }

        public string no_pol { get; set; }

        public string sopir { get; set; }

        public string no_sp_dtl { get; set; }

        public string seq_dpb { get; set; }

        public string tipe_trans { get; set; }
       
        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string kd_ukuran { get; set; }

        public string kd_satuan { get; set; }

        public decimal? panjang { get; set; }

        public decimal? lebar { get; set; }

        public decimal? tinggi { get; set; }

        public decimal? qty_order { get; set; }

        public decimal qty_out { get; set; }

        public decimal? qty_sisa { get; set; }

        public string gudang_asal { get; set; }

        public string gudang_tujuan { get; set; }

        public decimal? harga { get; set; }

        public decimal? rp_trans { get; set; }

        public string blthn { get; set; }

        public string kd_buku_besar { get; set; }

        public string kd_buku_biaya { get; set; }

        public string keterangan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public bool? siap_sj { get; set; }

        public decimal? qty_batal { get; set; }

        public string status_brg_batal { get; set; }

        public string kd_dept { get; set; }
        public string nama_Barang { get; set; }

        public decimal? qty_data { get; set; }
        public string nama_Gudang { get; set; }
        public string rek_persediaan { get; set; }
        public decimal? qty_in { get; set; }
        public string cb_asal { get; set; }

        //add siap kirim
        public string no_sp { get; set; }
        public decimal? jumlah { get; set; }
        public string Nama_Customer { get; set; }

    }
}
