using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_KAS_BON_H
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }

        public string tipe_trans { get; set; }

        public string keterangan { get; set; }

        public DateTime? tgl_trans { get; set; }

        public decimal? total_trans { get; set; }

        public string rec_stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public int? jml_cetak { get; set; }

        public DateTime? tgl_cetak { get; set; }
        public List<FIN_KAS_BON> detail { get; set; }

        public string alamat { get; set; }

        public string nama { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }

    }

    
}
