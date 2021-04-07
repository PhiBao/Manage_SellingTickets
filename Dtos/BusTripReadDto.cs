using System;

namespace backend.Dtos
{
    public partial class BusTripReadDto
    {
        public int MaTuyenXe { get; set; }
        public DateTime NgayXuatBen { get; set; }
        public float DonGia { get; set; }
    }
}
