using System;
using System.Collections.Generic;
using System.Text;

namespace IFA.Domain.Helpers
{
    public class DropDownListItem
    {
        public string Text { get; set; }

        public string Value { get; set; }

        public string Group { get; set; }
    }

    public class KalkulasiStokVM
    {
        public string kd_stok { get; set; }
        public string kd_cabang { get; set; }
        public string blthn { get; set; }
        public string kd_gudang { get; set; }
    }
}
