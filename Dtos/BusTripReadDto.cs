using System;

namespace backend.Dtos
{
    public partial class BusTripReadDto
    {
        public int MaChuyenXe { get; set; }
        public string TenBxDi { get; set; }
        public string DiaChiBxDi { get; set; }
        public string TenBxDen { get; set; }
        public string DiaChiBxDen { get; set; }
        public string NhaXe { get; set; }
        public int? SoChoTrong { get; set; }
        public DateTime NgayXuatBen { get; set; }
        public float DonGia { get; set; }
    }
}
