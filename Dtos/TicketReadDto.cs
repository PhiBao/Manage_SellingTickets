using System;
using System.ComponentModel.DataAnnotations;

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
        public DateTime NgayXuatBen { get; set; }
        public float DonGia { get; set; }
        public string GhiChu { get; set; }
    }
}