using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class UserUpdateDto
    {
        [Required]
        [MaxLength(50)]
        public string TenNd { get; set; }
        [Required]
        [MaxLength(11)]
        public string Sdt { get; set; }
        [Required]
        [MaxLength(10)]
        public string Cmnd { get; set; }
        [Required]
        [MaxLength(100)]
        public string DiaChi { get; set; }
        [Required]
        public DateTime? NgaySinh { get; set; }
    }
}