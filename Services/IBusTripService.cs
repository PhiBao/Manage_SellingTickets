using System;
using System.Collections;
using System.Collections.Generic;
using backend.Models;

namespace backend.Services
{
    public interface IBusTripService 
    {
        public IEnumerable<Chuyenxe> GetBusTrips();
        public Chuyenxe GetBusTripById(int id);
        public IEnumerable<Chuyenxe> GetBusTripByCondition(int maBxDi, int maBxDen, DateTime ngayXuatBen);
    }
}