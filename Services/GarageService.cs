using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class GarageService : IGarageService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public GarageService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
        }

        public async Task CreateGarageAsync(Nhaxe garage)
        {
            if (garage == null)
            {
                throw new ArgumentNullException(nameof(garage));
            }
            _context.Nhaxes.Add(garage);
            await _context.SaveChangesAsync();
        }

        public async Task<Nhaxe> GetGarageByIdAsync(int Id)
        {
            return await _context.Nhaxes.FirstOrDefaultAsync(p => p.MaNhaXe == Id);
        }

        public async Task<IEnumerable<Nhaxe>> GetGaragesAsync()
        {
            return await _context.Nhaxes.ToListAsync();
        }

        public async Task UpdateGarageAsync(Nhaxe garage)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGarageAsync(Nhaxe garage)
        {
            if (garage == null)
            {
                throw new ArgumentNullException(nameof(garage));
            }
            // Find all routes of this route
            var busRoutes = await _context.Tuyenxes.Where(p => p.MaTuyenXe == garage.MaNhaXe).ToListAsync();
            // Find all buses of this route
            var buses = await _context.Xes.Where(p => p.MaNhaXe == garage.MaNhaXe).ToListAsync();

            // Find all trips of these routes
            foreach (var busRoute in busRoutes)
            {
                var busTrips = await _context.Chuyenxes.Where(p => p.MaTuyenXe == busRoute.MaTuyenXe).ToListAsync();
                // Find and delete all tickets, seats that relations with those bus trips
                foreach (var busTrip in busTrips)
                {
                    var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                    _context.Vexes.RemoveRange(ticketsByBusTrip);
                }
                // Delete all trips that relations with this route
                _context.Chuyenxes.RemoveRange(busTrips);
            }

            _context.Tuyenxes.RemoveRange(busRoutes);
            _context.Xes.RemoveRange(buses);
            _context.Nhaxes.Remove(garage);
            
            await _context.SaveChangesAsync();
        }
    }
}