using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class FIN_GIRO_D
    {
        public string Kd_Cabang { get; set; }

        public string nomor { get; set; }

        public string no_inv { get; set; }

        public decimal? jml_trans { get; set; }

        public DateTime? Last_Create_Date { get; set; }

        public string Last_Created_By { get; set; }

        public DateTime? Last_Update_Date { get; set; }

        public string Last_Updated_By { get; set; }

        public string Program_Name { get; set; }

    }
}
