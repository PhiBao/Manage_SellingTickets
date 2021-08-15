using System;

namespace backend.Dtos
{
    public partial class TicketCreateDto
    {
        public int MaChuyenXe { get; set; }
        public int MaKh { get; set; }
        public int[] MaChoNgoi { get; set; }
        public string NgayDi { get; set; }
        public string GhiChu { get; set; }
        public double? DaThanhToan { get; set; }
    }
}