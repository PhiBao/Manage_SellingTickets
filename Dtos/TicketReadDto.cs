using System;

namespace backend.Dtos
{
    public partial class TicketReadDto
    {
        public int MaVe { get; set; }
        public string TenKh { get; set; }
        public string Sdt { get; set; }
        public int MaChoNgoi { get; set; }
        public string TenTuyenXe { get; set; }
        public string BienSoXe { get; set; }
        public DateTime NgayDi { get; set; }
        public float DonGia { get; set; }
        public string GhiChu { get; set; }
        public bool TrangThai { get; set; }
    }
}