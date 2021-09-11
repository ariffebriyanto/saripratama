using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_OPNAME_DTL
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_trans { get; set; }

        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string kd_satuan { get; set; }

        public string kode_gudang { get; set; }

        public string kd_ukuran { get; set; }

        public string kd_jns_persd { get; set; }

        public string kd_jenis { get; set; }

        public string spek_brg { get; set; }

        public decimal? qty_data { get; set; }

        public decimal? qty_opname { get; set; }

        public decimal? qty_selisih { get; set; }
        public decimal? qty_kartu { get; set; }

        public decimal? persen { get; set; }

        public decimal? nilai_rata { get; set; }

        public decimal? nilai_manual { get; set; }

        public string keterangan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string bultah { get; set; }
        public string kd_buku_besar { get; set; }
        public string kd_buku_biaya { get; set; }
        public string blthn { get; set; }
        public DateTime? tgl_trans { get; set; }
        public string rek_persediaan { get; set; }
        public decimal? total { get; set; }
        public string nama_barang { get; set; }


    }
}
