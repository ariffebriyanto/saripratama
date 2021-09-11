using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_KAS_BON
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }
        public int no_seq { get; set; }

        public string tipe_trans { get; set; }

        public DateTime? tgl_trans { get; set; }

        public string kd_kartu { get; set; }

        public string kd_valuta { get; set; }

        public decimal? kurs_valuta { get; set; }

        public string keterangan { get; set; }

        public string kd_divisi { get; set; }

        public string no_jur { get; set; }

        public DateTime? tgl_posting { get; set; }

        public decimal jml_trans { get; set; }

        public decimal? jml_tambahan { get; set; }

        public decimal? jml_bayar { get; set; }

        public decimal? jml_akhir { get; set; }

        public string kd_tarif { get; set; }

        public string kd_buku_besar { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public int? jml_cetak { get; set; }

        public DateTime? tgl_cetak { get; set; }
   

    }
}
