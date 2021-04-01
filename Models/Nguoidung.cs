using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Nguoidung
    {
        public Nguoidung()
        {
            Vexes = new HashSet<Vexe>();
            Xes = new HashSet<Xe>();
        }

        public int MaNd { get; set; }
        public string TenNd { get; set; }
        public string Sdt { get; set; }
        public string Cmnd { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public byte? Vaitro { get; set; }

        public virtual Taikhoan MaNdNavigation { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
