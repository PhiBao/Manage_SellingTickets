using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Xe
    {
        public Xe()
        {
            Chuyenxes = new HashSet<Chuyenxe>();
        }

        public int MaXe { get; set; }
        public string BienSoXe { get; set; }
        public int MaNhaXe { get; set; }
        public int MaNv { get; set; }
        public int? SoChoNgoi { get; set; }

        public virtual Nhaxe MaNhaXeNavigation { get; set; }
        public virtual Nguoidung MaNvNavigation { get; set; }
        public virtual ICollection<Chuyenxe> Chuyenxes { get; set; }
    }
}
