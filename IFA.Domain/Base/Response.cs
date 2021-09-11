using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Base
{
    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}
