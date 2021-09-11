using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class SALES_SJ
    {
        [Required]
        public string Kd_cabang { get; set; }

        public string tipe_trans { get; set; }
        [Required]
        public string no_sj { get; set; }
        public string no_sj2 { get; set; }
      

        public string no_dpb { get; set; }

        public DateTime? Tgl_kirim { get; set; }

        public DateTime? Jam_kirim { get; set; }

        public DateTime? Tgl_terima { get; set; }

        public DateTime? Jam_terima { get; set; }

        public DateTime? Tgl_balik { get; set; }

        public DateTime? Jam_balik { get; set; }

        public string Kondisi_trm { get; set; }

        public string No_pol { get; set; }

        public string Kd_sopir { get; set; }

        public string kd_kenek { get; set; }

        public string kd_customer { get; set; }

        public string nama_agent { get; set; }

        public string Almt_agen { get; set; }

        public string Keterangan { get; set; }

        public string status { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public string No_sp { get; set; }

        public DateTime TglSJ { get; set; }
        public string tgl_transdesc { get; set; }

        public string No_Gudang_Out { get; set; }

        public string sts_nota { get; set; }

        public int? cetakke { get; set; }

        public string Alasan { get; set; }

        public DateTime? Tgl_Cetak { get; set; }

        public string isPrinted { get; set; }

        public decimal? harga { get; set; }

        public string KM_BERANGKAT { get; set; }

        public string KM_KEMBALI { get; set; }

        public string no_krm { get; set; }

        public string status_sj { get; set; }
        //opsioanal
       
        public string Jenis_sp { get; set; }
       
        public string nama_customer { get; set; } //alamat1
        public string alamat1 { get; set; } //alamat1

        public decimal? ongkir { get; set; }
        public string inc_ongkir { get; set; }
        public List<SALES_SJ_D> sjdetail { get; set; }

    }

}
