using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Dtos
{
    public partial class UserReadDto
    {
        public string MaNd { get; set; }
        public string TenNd { get; set; }
    }
}