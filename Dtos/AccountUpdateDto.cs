using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class AccountUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string MatKhauCu { get; set; }
        [Required]
        [MaxLength(50)]
        public string MatKhau { get; set; }
    }
}