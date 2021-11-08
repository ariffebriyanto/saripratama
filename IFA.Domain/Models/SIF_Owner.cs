using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_Owner
    {
        public string Kd_Cabang { get; set; }

        public string Kd_Owner { get; set; }

        public string Nama_Owner { get; set; }

        public string Alamat_Owner { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }
    }
    public class filterOwnerPartial
    {

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public int pageSize { get; set; }
        public int page { get; set; }

    }
}
