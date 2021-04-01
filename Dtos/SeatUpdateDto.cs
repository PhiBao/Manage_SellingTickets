using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class SeatUpdateDto
    {
        [Required]
        public bool TinhTrangChoNgoi { get; set; }
    }
}