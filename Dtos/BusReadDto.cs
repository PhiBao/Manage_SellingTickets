using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class BusReadDto
    {
        public int MaXe { get; set; }
        public string BienSoXe { get; set; }
        public string TenNhaXe { get; set; }
        public string TenNv { get; set; }
        public string Sdt { get; set; }
        public int? SoChoNgoi { get; set; }

    }
}
