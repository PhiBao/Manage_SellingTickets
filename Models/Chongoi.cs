using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Chongoi
    {
        public Chongoi()
        {
            Vexes = new HashSet<Vexe>();
        }

        public int MaChoNgoi { get; set; }
        public int MaChuyenXe { get; set; }
        public bool? TinhTrangChoNgoi { get; set; }

        public virtual Chuyenxe MaChuyenXeNavigation { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
    }
}
