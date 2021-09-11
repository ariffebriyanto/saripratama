using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class V_MonStokDetail
    {
        public long? nomor { get; set; }

        public string Kode_Gudang { get; set; }

        public string Nama_Gudang { get; set; }

        public string kd_stok { get; set; }

        public string bultah { get; set; }

        public string Nama_Barang { get; set; }

        public decimal awal_qty { get; set; }

        public decimal? akhir_qty { get; set; }

        public decimal qty_in { get; set; }

        public decimal qty_out { get; set; }
        public decimal qty_akhir_expedisi { get; set; }

        public string Kd_Satuan { get; set; }

        public string kd_merk { get; set; }

        public string kd_tipe { get; set; }

        public string kd_sub_tipe { get; set; }

        public string kd_kain { get; set; }

        public string kd_ukuran { get; set; }
        

    }

}
