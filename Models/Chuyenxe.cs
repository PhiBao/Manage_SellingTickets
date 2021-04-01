using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Chuyenxe
    {
        public Chuyenxe()
        {
            Chongois = new HashSet<Chongoi>();
            Vexes = new HashSet<Vexe>();
        }

        public int MaChuyenXe { get; set; }
        public int MaTuyenXe { get; set; }
        public int MaXe { get; set; }
        public DateTime? NgayXuatBen { get; set; }
        public int? SoChoDaDat { get; set; }
        public int? SoChoTrong { get; set; }
        public double? DonGia { get; set; }

        public virtual Tuyenxe MaTuyenXeNavigation { get; set; }
        public virtual Xe MaXeNavigation { get; set; }
        public virtual ICollection<Chongoi> Chongois { get; set; }
        public virtual ICollection<Vexe> Vexes { get; set; }
    }
}
