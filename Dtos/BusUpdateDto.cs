using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public partial class BusUpdateDto
    {
        [Required]
        [MaxLength(8)]
        public string BienSoXe { get; set; }
        public int MaNv { get; set; }

    }
}
