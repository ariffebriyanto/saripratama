using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class V_StokGudang
    {
        public string Kode_Barang { get; set; }

        public string Nama_Barang { get; set; }

        public string bultah { get; set; }

        public decimal awal_qty_onstok { get; set; }
        public List<vy_saldocard> ListSaldo { get; set; }
        public vy_profile profile { get; set; }
    }

    public class vy_saldocard
    {
        public string Kd_Cabang { get; set; } //Kd_Cabang
        public string tipe_trans { get; set; } //Kd_Cabang

        public string Atas_Nama { get; set; }

        public DateTime Tanggal { get; set; }

        public DateTime? sorttanggal { get; set; }

        public string kd_stok { get; set; }

        public int no_seq { get; set; }

        public string Nama_Barang { get; set; }

        public string Kd_Satuan { get; set; }

        public string bultah { get; set; }

        public string no_trx { get; set; }

        public string no_trans { get; set; }

        public string keterangan { get; set; }

        public decimal? awal_qty_onstok { get; set; }

        public decimal qty_in { get; set; }

        public decimal qty_out { get; set; }

        public DateTime? Last_Create_Date { get; set; }
        public decimal qty_sisa { get; set; }
        public string gudang { get; set; }
        public string userid { get; set; }

    }
    public class vy_profile
    {
        public string nama { get; set; }
        public string alamat { get; set; }
        public string kota { get; set; }
        public string propinsi { get; set; }
        public string telp1 { get; set; }
        public string fax1 { get; set; }

    }
}
