using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Cabang
    {
        public string kd_cabang { get; set; }

        public string nama { get; set; }

        public string alamat { get; set; }

        public string kota { get; set; }

        public string propinsi { get; set; }

        public string telp1 { get; set; }

        public string telp2 { get; set; }

        public string fax1 { get; set; }

        public string fax2 { get; set; }

        public string email { get; set; }

        public string website { get; set; }

        public string no_npwp { get; set; }

        public byte[] logo { get; set; }

        public string motto { get; set; }

        public string keterangan { get; set; }

        public string rec_stat { get; set; }

        public DateTime? last_create_date { get; set; }

        public string last_created_by { get; set; }

        public DateTime? last_update_date { get; set; }

        public string last_updated_by { get; set; }

        public string program_name { get; set; }

        public string nama_induk { get; set; }

        public string nama_app { get; set; }
        public string url_logo { get; set; }
        

    }

}
