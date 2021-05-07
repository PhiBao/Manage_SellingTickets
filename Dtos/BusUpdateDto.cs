using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class BusUpdateDto
    {
        [Required]
        [MaxLength(8)]
        public string BienSoXe { get; set; }
        [Required]
        [MaxLength(10)]
        public string MaNv { get; set; }

    }
}
