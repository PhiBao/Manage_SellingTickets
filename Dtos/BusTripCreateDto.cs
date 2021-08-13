using System;

namespace backend.Dtos
{
    public partial class BusTripCreateDto
    {
        public int MaTuyenXe { get; set; }
        public int MaXe { get; set; }
        public String GioXuatBen { get; set; }
        public string LichTrinh { get; set; }
        public double? DonGia { get; set; }
    }
}