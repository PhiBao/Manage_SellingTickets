using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Nhaxe
    {
        public Nhaxe()
        {
            Tuyenxes = new HashSet<Tuyenxe>();
            Xes = new HashSet<Xe>();
        }

        public int MaNhaXe { get; set; }
        public string TenNhaXe { get; set; }

        public virtual ICollection<Tuyenxe> Tuyenxes { get; set; }
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
