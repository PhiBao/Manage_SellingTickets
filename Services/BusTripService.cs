using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public class BusTripService : IBusTripService
    {
        private readonly QLBVXKContext _context;

        public BusTripService(QLBVXKContext context)
        {
            _context = context;
        }

        public void CreateBusTrip(Chuyenxe busTrip)
        {
            if (busTrip == null) 
            {
                throw new ArgumentNullException(nameof(busTrip));
            }
            _context.Chuyenxes.Add(busTrip);
            _context.SaveChanges();
        }

        public IEnumerable<Chuyenxe> GetBusTripByCondition(int maBxDi, int maBxDen)
        {
            var busRoute = _context.Tuyenxes.Where(p => p.MaBxden == maBxDen && p.MaBxdi == maBxDi)
                            .Select(p => p.MaTuyenXe).FirstOrDefault();

            var busTrips = _context.Chuyenxes.Where(p => p.MaTuyenXe == busRoute).ToList();

            return busTrips;
        }

        public Chuyenxe GetBusTripById(int id)
        {
            return _context.Chuyenxes.FirstOrDefault(p => p.MaChuyenXe == id);
        }

        public IEnumerable<Chuyenxe> GetBusTrips()
        {
            return _context.Chuyenxes.ToList();
        }
    }
}