using System;

namespace backend.Dtos
{
    public partial class BusTripCreateDto
    {
        public int MaTuyenXe { get; set; }
        public int MaXe { get; set; }
        public DateTime NgayXuatBen { get; set; }
        public int? SoChoTrong { get; set; }
        public double? DonGia { get; set; }
    }
}