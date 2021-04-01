using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Taikhoan
    {
        public string Email { get; set; }
        public string MatKhau { get; set; }
        public int MaTk { get; set; }

        public virtual Nguoidung Nguoidung { get; set; }
    }
}
