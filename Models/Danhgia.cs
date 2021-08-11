using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Danhgia
    {
        public int MaDanhGia { get; set; }
        public int MaNhaXe { get; set; }
        public int MaNd { get; set; }
        public string NoiDungDanhGia { get; set; }
        public byte Sao { get; set; }

        public virtual Nguoidung MaNdNavigation { get; set; }
        public virtual Nhaxe MaNhaXeNavigation { get; set; }
    }
}
