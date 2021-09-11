using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_CUSTOMER
    {
        public string Kd_Cabang { get; set; }

        public string Kd_Customer { get; set; }

        public string Nama_Customer { get; set; }

        public DateTime? Tgl_Lahir { get; set; }

        public string Alamat1 { get; set; }

        public string Kota1 { get; set; }

        public string No_Telepon1 { get; set; }

        public string nama_customer2 { get; set; }

        public string Alamat2 { get; set; }

        public string Kota2 { get; set; }

        public string No_Telepon2 { get; set; }

        public string No_Fax { get; set; }

        public string nm_npwp { get; set; }

        public string No_NPWP { get; set; }

        public string Email { get; set; }

        public string Jenis_Usaha { get; set; }

        public string Kontak_Person { get; set; }

        public string Grade { get; set; }

        public string Kd_Wilayah { get; set; }

        public string Kode_Area { get; set; }

        public string Keterangan { get; set; }

        public string Norek_Bank_Rupiah { get; set; }

        public string Norek_Bank_Valas { get; set; }

        public decimal? Limit_Piutang_Rupiah { get; set; }

        public decimal? Limit_Piutang_Valas { get; set; }

        public string Rec_Stat { get; set; }

        public string sts_group { get; set; }

        public string Attribute1 { get; set; }

        public string Attribute2 { get; set; }

        public string Attribute3 { get; set; }

        public string Attribute4 { get; set; }

        public string Attribute5 { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public decimal? jatuh_tempo { get; set; }

        public decimal? jatuh_tempo2 { get; set; }

        public string flag_overlimit { get; set; }

    }
}
