using System;

namespace backend.Dtos
{
    public partial class RevenueCreateDto
    {
        public DateTime Ngay { get; set; }
        public int SoVe { get; set; }
        public double? TongDoanhThu { get; set; }
        public string GhiChu { get; set; }
    }
}