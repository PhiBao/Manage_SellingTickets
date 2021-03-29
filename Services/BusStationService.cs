using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class BusStationService : IBusStationService
    {
        private readonly QLBVXKContext _context;

        public BusStationService(QLBVXKContext context) 
        {
            _context = context;
        }

        public void CreateBusStation(Benxe busStation)
        {
            if (busStation == null) 
            {
                throw new ArgumentNullException(nameof(busStation));
            }
            _context.Benxes.Add(busStation);
            _context.SaveChanges();
        }

        public Benxe GetBusStationById(int Id)
        {
            return _context.Benxes.FirstOrDefault(p => p.MaBx == Id);
        }

        public IEnumerable<Benxe> GetBusStations()
        {
            return _context.Benxes.ToList();
        }
    }
}