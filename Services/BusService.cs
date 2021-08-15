using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusService : IBusService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public BusService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
        }

        public async Task CreateBusAsync(Xe bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus));
            }

            _context.Xes.Add(bus);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusAsync(Xe bus)
        {
            if (bus == null)
            {
                throw new ArgumentNullException(nameof(bus));
            }
            // Find all trips of this bus
            var busTrips = await _context.Chuyenxes.Where(p => p.MaXe == bus.MaXe).ToListAsync();

            // Find and delete all tickets, seats that relations with those bus trips
            foreach (var busTrip in busTrips)
            {
                var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                _context.Vexes.RemoveRange(ticketsByBusTrip);
            }

            // Delete all trips that relations with this bus
            _context.Chuyenxes.RemoveRange(busTrips);

            // Delete this bus
            _context.Xes.Remove(bus);

            await _context.SaveChangesAsync();
        }

        public async Task<Xe> GetBusByIdAsync(int Id)
        {
            return await _context.Xes.FirstOrDefaultAsync(p => p.MaXe == Id);
        }

        public async Task<IEnumerable<Xe>> GetBusesAsync()
        {
            return await _context.Xes.ToListAsync();
        }

        public async Task<IEnumerable<Xe>> GetBusesByBusRouteIdAsync(int id)
        {
            int garageId = await _context.Tuyenxes.Where(p => p.MaTuyenXe == id).Select(p => p.MaNhaXe).FirstOrDefaultAsync();
            return await _context.Xes.Where(p => p.MaNhaXe == garageId).ToListAsync();
        }

        public async Task<IEnumerable<Xe>> GetBusesByGarageIdAsync(int garageId)
        {
            return await _context.Xes.Where(p => p.MaNhaXe == garageId).ToListAsync();
        }

        public async Task UpdateBusAsync(Xe bus)
        {
            await _context.SaveChangesAsync();
        }
    }
}