using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class SeatReadDto
    {
        public int MaChoNgoi { get; set; }
        public bool TinhTrangChoNgoi { get; set; }
    }
}