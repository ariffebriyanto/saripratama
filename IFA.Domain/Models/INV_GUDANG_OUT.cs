using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_GUDANG_OUT
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_trans { get; set; }

        public string tipe_trans { get; set; }

        public DateTime tgl_trans { get; set; }

        public string no_ref { get; set; }

        public DateTime? tgl_ref { get; set; }

        public string kd_kegiatan { get; set; }

        public string no_rph { get; set; }

        public decimal jml_rp_trans { get; set; }

        public decimal? jml_ppn { get; set; }

        public string penerima { get; set; }

        public string blthn { get; set; }

        public string no_posting { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string keterangan { get; set; }

        public string no_jurnal { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public bool? sudah_sj { get; set; }

        public int? cetak_ke { get; set; }

        public string cetak_ulang { get; set; }
        public List<INV_GUDANG_OUT_D> detail { get; set; }
        public string gudang { get; set; }
        public string tgl_transdesc { get; set; }
        public string supir { get; set; }
        public string Nama_Kendaraan { get; set; }
        public string gudang_asal { get; set; }

        public string gudang_tujuan { get; set; }
        public string nama_gdtujuan { get; set; }
        public string nama_gdasal { get; set; } 
        public string fax1 { get; set; } //alamat
        public string fax2 { get; set; } //alamat
        public string alamat { get; set; } //alamat
        public string stat { get; set; } 

    }
}
