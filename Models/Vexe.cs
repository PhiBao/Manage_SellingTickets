﻿using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Vexe
    {
        public int MaVe { get; set; }
        public int MaChuyenXe { get; set; }
        public int MaKh { get; set; }
        public int MaChoNgoi { get; set; }

        public virtual Chongoi MaChoNgoiNavigation { get; set; }
        public virtual Chuyenxe MaChuyenXeNavigation { get; set; }
        public virtual Nguoidung MaKhNavigation { get; set; }
    }
}
