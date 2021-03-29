using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Doanhthungay
    {
        public int MaDoanhThuNgay { get; set; }
        public DateTime Ngay { get; set; }
        public int SoVe { get; set; }
        public double? TongDoanhThu { get; set; }
    }
}
