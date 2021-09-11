using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Models
{
    public class SIF_TIPE_TRANS
    {
        public string kd_cabang { get; set; }

        public string kd_subsis { get; set; }

        public string kd_jurnal { get; set; }

        public string tipe_trans { get; set; }

        public string tipe_desc { get; set; }

        public string accronim_desc { get; set; }

        public string auto_manual { get; set; }

        public string auto_posting { get; set; }

        public string flag_pajak { get; set; }

        public string flag_materai { get; set; }

        public string attribut1 { get; set; }

        public string attribut2 { get; set; }

        public string status { get; set; }

        public DateTime? last_create_date { get; set; }

        public string last_created_by { get; set; }

        public DateTime? last_update_date { get; set; }

        public string last_updated_by { get; set; }

        public string program_name { get; set; }

        public string kd_dept { get; set; }

    }
    public class SIF_TIPE_TRANSVM
    {
        public string mode { get; set; }
        public string updateby { get; set; }
        public DateTime updatedate { get; set; }

        public List<SIF_TIPE_TRANS> details { get; set; }
    }

    public class SIF_TIPE_TRANSVMON
    {
        public string tipe_trans { get; set; }

        public string tipe_desc { get; set; }

        public string Id_Data { get; set; }

        public string kd_jurnal { get; set; }

    }
}
