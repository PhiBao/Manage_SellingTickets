namespace backend.Dtos
{
    public partial class BusTripReadDto
    {
        public int MaChuyenXe { get; set; }
        public string TenBxDi { get; set; }
        public string DiaChiBxDi { get; set; }
        public string TenBxDen { get; set; }
        public string DiaChiBxDen { get; set; }
        public int MaNhaXe { get; set; }
        public string TenNhaXe { get; set; }
        public int SoChoNgoi { get; set; }
        public string[] NgayXuatBen { get; set; }
        public int ThoiGianDiChuyen { get; set; }
        public float DonGia { get; set; }
    }
}
