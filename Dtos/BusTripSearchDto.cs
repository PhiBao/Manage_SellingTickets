namespace backend.Dtos
{
    public partial class BusTripSearchDto
    {
        public int MaChuyenXe { get; set; }
        public string TenBxDi { get; set; }
        public string DiaChiBxDi { get; set; }
        public string TenBxDen { get; set; }
        public string DiaChiBxDen { get; set; }
        public string TenNhaXe { get; set; }
        public int? SoChoTrong { get; set; }
        public string GioXuatBen { get; set; }
        public int ThoiGianDiChuyen { get; set; }
        public float DonGia { get; set; }
    }
}
