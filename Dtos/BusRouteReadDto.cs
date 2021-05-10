using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class BusRouteReadDto
    {
        public int MaTuyenXe { get; set; }
        public string TenTuyenXe { get; set; }
        public string DiaChiBxDi { get; set; }
        public string DiaChiBxDen { get; set; }
        public int? ThoiGianDiChuyen { get; set; }
        
    }
}