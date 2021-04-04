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
        private readonly QLBVXKContext _context;

        public BusService(QLBVXKContext context)
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
                var seatsByBusTrip = await _context.Chongois.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                _context.Chongois.RemoveRange(seatsByBusTrip);
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

        public async Task UpdateBusAsync(Xe bus)
        {
            await _context.SaveChangesAsync();
        }
    }
}