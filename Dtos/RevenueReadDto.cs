using System;

namespace backend.Dtos
{
    public partial class RevenueReadDto
    {
        public DateTime Ngay { get; set; }
        public double? TongDoanhThu { get; set; }
        public string GhiChu { get; set; }
    }
}