using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Vexe
    {
        public Vexe()
        {
            Danhgias = new HashSet<Danhgia>();
        }

        public int MaVe { get; set; }
        public int MaChuyenXe { get; set; }
        public int MaKh { get; set; }
        public int MaChoNgoi { get; set; }
        public string NgayDi { get; set; }
        public string GhiChu { get; set; }
        public double? DaThanhToan { get; set; }
        public bool? TrangThai { get; set; }

        public virtual Chuyenxe MaChuyenXeNavigation { get; set; }
        public virtual Nguoidung MaKhNavigation { get; set; }
        public virtual ICollection<Danhgia> Danhgias { get; set; }
    }
}
