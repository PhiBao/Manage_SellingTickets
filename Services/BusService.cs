using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class BusService : IBusService
    {
        private readonly QLBVXKContext _context;

        public BusService(QLBVXKContext context) 
        {
            _context = context;
        }

        public void CreateBus(Xe bus)
        {
            if (bus == null) 
            {
                throw new ArgumentNullException(nameof(bus));
            }

            _context.Xes.Add(bus);
            _context.SaveChanges();
        }

        public void DeleteBus(Xe bus)
        {
            if (bus == null) 
            {
                throw new ArgumentNullException(nameof(bus));
            }
            _context.Xes.Remove(bus);
            _context.SaveChanges();
        }

        public Xe GetBusById(int Id)
        {
            return _context.Xes.FirstOrDefault(p => p.MaXe == Id);
        }

        public IEnumerable<Xe> GetBuses()
        {
            return _context.Xes.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateBus(Xe bus)
        {
        }
    }
}