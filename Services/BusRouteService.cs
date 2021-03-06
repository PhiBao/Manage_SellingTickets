using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusRouteService : IBusRouteService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public BusRouteService(d1h6lskf7s3bc0Context context)
        {
            _context = context;
        }
        public async Task CreateBusRouteAsync(Tuyenxe busRoute)
        {
            if (busRoute == null)
            {
                throw new ArgumentNullException(nameof(busRoute));
            }
            var check = await _context.Tuyenxes.Where(p => p.MaBxden == busRoute.MaBxden && p.MaBxdi == busRoute.MaBxdi && p.MaNhaXe == busRoute.MaNhaXe)
                            .Select(p => p.MaTuyenXe).FirstOrDefaultAsync();
            if (check == 0)
            {
                _context.Tuyenxes.Add(busRoute);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Tuyenxe> GetBusRouteByIdAsync(int id)
        {
            return await _context.Tuyenxes.FirstOrDefaultAsync(p => p.MaTuyenXe == id);
        }

        public async Task<IEnumerable<Tuyenxe>> GetBusRoutesAsync()
        {
            return await _context.Tuyenxes.ToListAsync();
        }

        public async Task<IEnumerable<Tuyenxe>> GetBusRoutesByGarageIdAsync(int garageId)
        {
            return await _context.Tuyenxes.Where(p => p.MaNhaXe == garageId).ToListAsync();
        }

        public async Task DeleteBusRouteAsync(Tuyenxe busRoute)
        {
            if (busRoute == null)
            {
                throw new ArgumentNullException(nameof(busRoute));
            }
            // Find all trips of this route
            var busTrips = await _context.Chuyenxes.Where(p => p.MaTuyenXe == busRoute.MaTuyenXe).ToListAsync();

            // Find and delete all tickets, seats that relations with those bus trips
            foreach (var busTrip in busTrips)
            {
                var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
                _context.Vexes.RemoveRange(ticketsByBusTrip);
            }

            // Delete all trips that relations with this route
            _context.Chuyenxes.RemoveRange(busTrips);

            // Delete this route
            _context.Tuyenxes.Remove(busRoute);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBusRouteAsync(Tuyenxe busRoute)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tuyenxe>> SearchBusRoutesByName(int depId, int destId)
        {
            return await _context.Tuyenxes.Where(p => p.MaBxden == destId && p.MaBxdi == depId).ToListAsync();
        }
    }
}