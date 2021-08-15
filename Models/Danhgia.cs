using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Danhgia
    {
        public int MaDanhGia { get; set; }
        public int MaVe { get; set; }
        public int MaNd { get; set; }
        public string NoiDungDanhGia { get; set; }
        public short Sao { get; set; }

        public virtual Nguoidung MaNdNavigation { get; set; }
        public virtual Vexe MaVeNavigation { get; set; }
    }
}
