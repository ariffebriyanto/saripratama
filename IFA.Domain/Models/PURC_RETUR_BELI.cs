using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class PURC_RETUR_BELI
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_retur { get; set; }

        public DateTime? tanggal { get; set; }
        [Required]
        public string no_po { get; set; }

        public string no_ref { get; set; }

        public string no_ref1 { get; set; }
        [Required]
        public string kd_supplier { get; set; }

        public string keterangan { get; set; }

        public decimal? jml_rp_trans { get; set; }

        public string rec_stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public int? jumlah_cetak { get; set; }

        public string flag_ppn { get; set; }

        public string Bonus { get; set; }

        public string Nama_Supplier { get; set; }

        public List <PURC_RETUR_BELI_D> detail { get; set; }


    }

    public class PURC_RETUR_BELIVM
    {
        public string Kd_Cabang { get; set; }

        public string no_retur { get; set; }

    }
}
