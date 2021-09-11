using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace IFA.Domain.Models
{
    public class MUSER_ROLE
    {
        [Required]
        public string IDUSER { get; set; }
        [Required]
        public string IDROLE { get; set; }

    }
}
