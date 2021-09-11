using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_GIRO
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }

        public string jns_trans { get; set; }

        public string divisi { get; set; }

        public string tipe_trans { get; set; }

        public string jns_giro { get; set; }

        public string kd_jns_giro { get; set; }

        public DateTime? tgl_trans { get; set; }

        public decimal kd_bank { get; set; }

        public string kartu { get; set; }

        public DateTime? tgl_jth_tempo { get; set; }

        public string kd_valuta { get; set; }

        public decimal? kurs_valuta { get; set; }

        public decimal? jml_trans { get; set; }

        public string keterangan { get; set; }

        public string status { get; set; }

        public string no_jur { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string bank_asal { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string telp { get; set; }
        public string wa { get; set; }
        public string Nama_Valuta { get; set; }
        public string jenis_giro { get; set; }
        public string nama_bank_asal { get; set; }
        public string nama_bank { get; set; }
        public string Nama_Departemen { get; set; }
        public string nama_customer { get; set; }
        public string Desc_Data { get; set; }
        public string jns_giro_desc { get; set; }

        public string no_ref{ get; set; }
        public int isSelect { get; set; }

        public string nama_departemen { get; set; }
        public List<FIN_GIRO_D> detail { get; set; }

    }

}
