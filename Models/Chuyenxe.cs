using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Chuyenxe
    {
        public Chuyenxe()
        {
            Vexes = new HashSet<Vexe>();
        }

        public int MaChuyenXe { get; set; }
        public int MaTuyenXe { get; set; }
        public int MaXe { get; set; }
        public string GioXuatBen { get; set; }
        public string LichTrinh { get; set; }
        public double? DonGia { get; set; }

        public virtual Tuyenxe MaTuyenXeNavigation { get; set; }
        public virtual Xe MaXeNavigation { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
    }
}
