using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class BusRouteUpdateDto
    {
        [Required]
        [MaxLength(10)]
        public string MaBxden { get; set; }
        [Required]
        [MaxLength(10)]
        public string MaBxdi { get; set; }
    }
}
