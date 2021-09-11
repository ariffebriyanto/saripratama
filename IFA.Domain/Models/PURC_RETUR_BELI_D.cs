using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IFA.Domain.Models
{
    public class PURC_RETUR_BELI_D
    {
        [Required]
        public string Kd_Cabang { get; set; }
        [Required]
        public string no_retur { get; set; }
        [Required]
        public int no_seq { get; set; }
        [Required]
        public string kd_stok { get; set; }

        public string satuan { get; set; }
        [Required]
        public decimal? qty { get; set; }

        public decimal? qty_sisa { get; set; }

        public decimal harga { get; set; }

        public decimal? total { get; set; }

        public string keterangan { get; set; }

        public string rec_stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public decimal? Disk1 { get; set; }

        public decimal? Disk2 { get; set; }

        public decimal? Disk3 { get; set; }

        public decimal? Disk4 { get; set; }

        public decimal? jml_diskon { get; set; }

        public string Bonus { get; set; }

        public decimal qty_retur { get; set; }

        public decimal retur_total { get; set; }

        public string kd_satuan { get; set; }
        public string nama_barang { get; set; }
    }
}
