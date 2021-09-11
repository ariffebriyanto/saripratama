using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
   public class INV_MUTASI_IN_D
    {
        [Required]
        public string Kd_Cabang { get; set; }

        public string tipe_trans { get; set; }
        [Required]
        public string no_mutasi { get; set; }

        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string kd_satuan { get; set; }

        public int? qty { get; set; }

        public string kode_gudang1 { get; set; }

        public string kode_gudang2 { get; set; }

        public string keterangan { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }
    }
}
