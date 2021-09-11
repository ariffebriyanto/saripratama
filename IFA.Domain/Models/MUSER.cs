using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class MUSER
    {
        public string kd_cabang { get; set; }

        public string userid { get; set; }

        public string nama { get; set; }

        public string passwd { get; set; }

        public DateTime? Last_create_date { get; set; }

        public string Last_created_by { get; set; }

        public DateTime? Last_update_date { get; set; }

        public string Last_updated_by { get; set; }

        public string Program_name { get; set; }

        public string NIP { get; set; }

        public string rec_stat { get; set; }
        public string oldPassword { get; set; }
        public string cabang_new { get; set; }

    }
}
