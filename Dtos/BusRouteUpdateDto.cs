using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class BusRouteUpdateDto
    {
        [Required]
        [MaxLength(10)]
        public int MaBxden { get; set; }
        [Required]
        [MaxLength(10)]
        public int MaBxdi { get; set; }
    }
}
