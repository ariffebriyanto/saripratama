using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IFA.Domain.Models
{
    public class SIF_Pegawai
    {
        public string Kd_Cabang { get; set; }

        public string Kode_Pegawai { get; set; }

        public string NIP { get; set; }

        public string NABS { get; set; }

        public string no_ktp { get; set; }

        public string Nama_Pegawai { get; set; }



        public string Alamat_1 { get; set; }

        public string Alamat_2 { get; set; }

        public string Kode_Kota { get; set; }

        public string No_Telepon1 { get; set; }

        public string No_telepon2 { get; set; }

        public string Tmp_lahir { get; set; }

        public DateTime? Tgl_Lahir { get; set; }

        public string Jns_Kelamin { get; set; }

        public string Kode_Status { get; set; }

        public DateTime? tgl_kerja { get; set; }

        public string Kode_Status_Kerja { get; set; }

        public string Kode_Departemen { get; set; }

        public string Kode_bagian { get; set; }

        public string Kode_Jabatan { get; set; }

        public string Peg_NPWP { get; set; }

        public string No_Jamsostek { get; set; }

        public string Rek_bank { get; set; }

        public string Nama_Bank { get; set; }

        public byte[] Foto { get; set; }

        public string Keterangan { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string sts_kb { get; set; }
        public string stat { get; set; }
        public string nama { get; set; }
        public string kode { get; set; }
        public string Kode_Sales { get; set; }

        public string akses_penjualan { get; set; }

        public string userlogin { get; set; } //user Login

        public string idrole { get; set; } //user Login

    }

}
