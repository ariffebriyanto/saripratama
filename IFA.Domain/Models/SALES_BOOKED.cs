using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IFA.Domain.Models
{
    public class SALES_BOOKED
    {
        public Guid id { get; set; }
        public Guid idDisplay { get; set; }
        public string no_sp { get; set; }
        public string Nama_Customer { get; set; }

        public string Kd_Cabang { get; set; }

        public DateTime tgl_inden { get; set; }

        public string Kd_sales { get; set; }

        public string Kd_Customer { get; set; }
        [Required]
        public string Kd_Stok { get; set; }
        public string Nama_Barang { get; set; }

        public decimal Qty { get; set; }
        public decimal? harga { get; set; }
        public decimal? total { get; set; }

        public string Kd_satuan { get; set; }

        public string Keterangan { get; set; }

        public string Status { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }
        public string tgl_indendesc { get; set; }
        public decimal? diskon { get; set; }
        public string Nama_Sales { get; set; }  
        public decimal? berat { get; set; }
        public decimal? TotalBerat { get; set; }
        public decimal? TotalBeratInden { get; set; }
        public string alamat { get; set; }  //alamat
        public string nama { get; set; } 
        public string telp { get; set; }
        public string wa { get; set; }
        public decimal dp_inden { get; set; }
        public string alokasiLevel { get; set; }
        public decimal qty_Alokasi { get; set; }

    }

}
