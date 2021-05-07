using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class UserReadDto
    {
        public int MaNd { get; set; }
        public string TenNd { get; set; }
        public string Sdt { get; set; }
        public string Cmnd { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
    }
}