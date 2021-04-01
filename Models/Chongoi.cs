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
        public bool? TinhTrangChoNgoi { get; set; }
        public int MaXe { get; set; }

        public virtual Xe MaXeNavigation { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
    }
}
