using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models
{
    public partial class Tuyenxe
    {
        public Tuyenxe()
        {
            Chuyenxes = new HashSet<Chuyenxe>();
        }

        public int MaTuyenXe { get; set; }
        public int MaBxden { get; set; }
        public int MaBxdi { get; set; }

        public virtual Benxe MaBxdenNavigation { get; set; }
        public virtual Benxe MaBxdiNavigation { get; set; }
        public virtual ICollection<Chuyenxe> Chuyenxes { get; set; }
    }
}
