using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_GUDANG_IN_D
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_trans { get; set; }

        public string no_qc { get; set; }
        public string no_ref { get; set; }

        public string tipe_trans { get; set; }
        [Required]
        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string kd_ukuran { get; set; }

        public string kd_satuan { get; set; }

        public decimal? qty_order { get; set; }
        [Required]
        public decimal qty_in { get; set; }

        public decimal? qty_sisa { get; set; }

        public decimal? harga { get; set; }

        public decimal? rp_trans { get; set; }

        public string blthn { get; set; }

        public string gudang_asal { get; set; }

        public string gudang_tujuan { get; set; }

        public string kd_buku_besar { get; set; }

        public string kd_buku_biaya { get; set; }

        public string keterangan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string stat_brg_retur { get; set; }


        public string Nama_Barang { get; set; }
        public decimal? qty_po { get; set; }
        public string lokasi_simpan { get; set; }
        public decimal qty_qc_pass { get; set; }

        public decimal? qty_data { get; set; }
       // public decimal? qty_order { get; set; }
        public string nama_Gudang { get; set; }
        public string nm_Gudang_asal { get; set; }
        public string rek_persediaan { get; set; }
       
        public string cb_asal { get; set; }
        public string penyerah { get; set; }

        public decimal berat { get; set; }

        public string alamat { get; set; }
        public string fax1 { get; set; }
        public string fax2 { get; set; }

    }
}
