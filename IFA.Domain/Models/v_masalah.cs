using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
   public class v_masalah
    {
        public string no_inv { get; set; }

        public DateTime tgl_jatuh_tempo { get; set; }

        public int parameter_date { get; set; }

        public int hari_jatuh_tempo { get; set; }

        public int jml_akhir { get; set; }

       public int lama_hari { get; set; }
    }
}
