using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_LUNAS_KAS_BON_D
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }

        public int no_seq { get; set; }

        public string tipe_trans { get; set; }

        public string prev_nomor { get; set; }

        public string rekening { get; set; }

        public string pusat_biaya { get; set; }

        public string keterangan { get; set; }

        public decimal? jml_trans { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Programe_Name { get; set; }

        public string kd_kartu { get; set; }

        public string no_jur { get; set; }

    }
}
