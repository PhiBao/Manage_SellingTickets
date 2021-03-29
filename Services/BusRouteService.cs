using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class BusRouteService : IBusRouteService
    {
        private readonly QLBVXKContext _context;

        public BusRouteService(QLBVXKContext context)
        {
            _context = context;
        }
        public void CreateBusRoute(Tuyenxe busRoute)
        {
            if (busRoute == null) 
            {
                throw new ArgumentNullException(nameof(busRoute));
            }
            _context.Tuyenxes.Add(busRoute);
            _context.SaveChanges();
        }

        public Tuyenxe GetBusRouteById(int id)
        {
            return _context.Tuyenxes.FirstOrDefault(p => p.MaTuyenXe == id);
        }

        public IEnumerable<Tuyenxe> GetBusRoutes()
        {
            return _context.Tuyenxes.ToList();
        }
    }
}