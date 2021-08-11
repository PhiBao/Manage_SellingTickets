using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Lichsutimkiem
    {
        public int MaLichSu { get; set; }
        public int MaNd { get; set; }
        public string NoiDen { get; set; }
        public string NoiDi { get; set; }
        public string NgayDi { get; set; }

        public virtual Nguoidung MaNdNavigation { get; set; }
    }
}
