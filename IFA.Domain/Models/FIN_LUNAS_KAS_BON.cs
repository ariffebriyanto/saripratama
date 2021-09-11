using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_LUNAS_KAS_BON
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }

        public string tipe_trans { get; set; }

        public DateTime? tgl_trans { get; set; }

        public string prev_nomor { get; set; }

        public decimal? jml_trans { get; set; }

        public string keterangan { get; set; }

        public string no_jur { get; set; }

        public DateTime? tgl_posting { get; set; }

        public string sts_hutang { get; set; }

        public decimal? sisa_hutang { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public int? jml_cetak { get; set; }

        public DateTime? tgl_cetak { get; set; }

        public string kd_kartu { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }
        public List<FIN_LUNAS_KAS_BON_D> detail { get; set; }

    }
}
