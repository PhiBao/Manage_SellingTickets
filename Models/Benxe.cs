using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Benxe
    {
        public Benxe()
        {
            TuyenxeMaBxdenNavigations = new HashSet<Tuyenxe>();
            TuyenxeMaBxdiNavigations = new HashSet<Tuyenxe>();
        }

        public int MaBx { get; set; }
        public string TenBx { get; set; }
        public string DiaChi { get; set; }

        public virtual ICollection<Tuyenxe> TuyenxeMaBxdenNavigations { get; set; }
        public virtual ICollection<Tuyenxe> TuyenxeMaBxdiNavigations { get; set; }
    }
}
