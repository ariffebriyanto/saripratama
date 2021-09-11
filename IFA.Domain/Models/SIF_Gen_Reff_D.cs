using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Gen_Reff_D
    {
        public string Kd_Cabang { get; set; }

        public string Ref_Role { get; set; }

        public string Id_Ref_File { get; set; }

        public string Id_Ref_Data { get; set; }

        public string Id_Data { get; set; }

        public string Desc_Data { get; set; }

        public string Val_kode1 { get; set; }

        public string Val_kode2 { get; set; }

        public string Keterangan { get; set; }

        public string Rec_Stat { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

        public string kd_departemen { get; set; }

        public string kd_jns_giro { get; set; }

        public string jns_giro { get; set; }
    }
}
