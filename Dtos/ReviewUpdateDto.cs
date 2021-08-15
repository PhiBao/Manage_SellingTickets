
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class ReviewUpdateDto
    {
        [Required]
        [MaxLength(1)]
        public string Sao { get; set; }
        [Required]
        [MaxLength(255)]
        public string NoiDungDanhGia { get; set; }
    }
}