using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class INV_OPNAME
    {
        [Required]
        public string Kd_Cabang { get; set; }

        public DateTime tgl_trans { get; set; }
        [Required]
        public string no_trans { get; set; }
        [Required]
        public string bultah { get; set; }

        public string keterangan { get; set; }

        public string no_jurnal { get; set; }

        public string no_posting { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public List<INV_OPNAME_DTL> opnamedtl { get; set; }

        public string tgl_transdesc { get; set; }
        public string gudang { get; set; }

        public string tipe_trans { get; set; }
        public string blthn { get; set; }

    }

}
