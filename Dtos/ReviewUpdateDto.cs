using System.ComponentModel.DataAnnotations;
namespace backend.Dtos
{
    public partial class ReviewUpdateDto
    {
        [Required]
        [MaxLength(1)]
        public byte Sao { get; set; }
        [Required]
        [MaxLength(255)]
        public string NoiDungDanhGia { get; set; }
    }
}