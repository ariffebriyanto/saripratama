using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_QC
    {
        [Required]
        public string Kd_Cabang { get; set; }

        public DateTime? tgl_trans { get; set; }
        [Required]
        public string no_trans { get; set; }

        public string no_ref { get; set; }

        public string tipe_trans { get; set; }

        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string spek_brg { get; set; }

        public string kd_satuan { get; set; }

        public decimal? qty_order { get; set; }
        [Required]
        public decimal? qty { get; set; }

        public decimal? qty_datang { get; set; }

        public decimal? qty_qc_pass { get; set; }

        public decimal? qty_qc_unpass { get; set; }

        public decimal? qty_sisa { get; set; }

        public decimal? harga { get; set; }

        public decimal? rp_trans { get; set; }

        public decimal? hold { get; set; }

        public decimal? hold_po { get; set; }

        public string status_hold { get; set; }

        public DateTime? tgl_act_hold { get; set; }

        public string sudah_dpm { get; set; }

        public string no_dpm { get; set; }

        public decimal? qty_release { get; set; }

        public decimal? qty_reject_hold { get; set; }

        public string blthn { get; set; }

        public string kd_buku_besar { get; set; }

        public string kd_buku_biaya { get; set; }

        public string keterangan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string rec_stat { get; set; }

        public string gudang_asal { get; set; }

        public string gudang_tujuan { get; set; }

        public decimal? qty_qc_service { get; set; }

        public decimal? qty_qc_rusak { get; set; }

        public string kd_ukuran { get; set; }

        public string notrans_act { get; set; }

        public string user_hold { get; set; }

        public int? jml_cetak_hold { get; set; }

        public string Bonus { get; set; }
        public string no_po { get; set; }
        public string Nama_Supplier { get; set; }
        public string nama_barang { get; set; }
        public string Nama_Gudang { get; set; }
        public string sj_supplier { get; set; }

        public string p_np { get; set; }
        public string doc_status { get; set; }






    }

}
