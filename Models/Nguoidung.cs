using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Nguoidung
    {
        public Nguoidung()
        {
            Danhgias = new HashSet<Danhgia>();
            Lichsutimkiems = new HashSet<Lichsutimkiem>();
            Vexes = new HashSet<Vexe>();
            Xes = new HashSet<Xe>();
        }

        public int MaNd { get; set; }
        public string TenNd { get; set; }
        public string Sdt { get; set; }
        public string Cmnd { get; set; }
        public string DiaChi { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string ImageUrl { get; set; }
        public short? Vaitro { get; set; }

        public virtual Taikhoan MaNdNavigation { get; set; }
        public virtual ICollection<Danhgia> Danhgias { get; set; }
        public virtual ICollection<Lichsutimkiem> Lichsutimkiems { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
        public virtual ICollection<Xe> Xes { get; set; }
    }
}
