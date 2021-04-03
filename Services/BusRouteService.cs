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
        private readonly QLBVXKContext _context;

        public BusRouteService(QLBVXKContext context)
        {
            _context = context;
        }
        public async Task CreateBusRouteAsync(Tuyenxe busRoute)
        {
            if (busRoute == null)
            {
                throw new ArgumentNullException(nameof(busRoute));
            }
            _context.Tuyenxes.Add(busRoute);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuyenxe> GetBusRouteByIdAsync(int id)
        {
            return await _context.Tuyenxes.FirstOrDefaultAsync(p => p.MaTuyenXe == id);
        }

        public async Task<IEnumerable<Tuyenxe>> GetBusRoutesAsync()
        {
            return await _context.Tuyenxes.ToListAsync();
        }

        public async Task DeleteBusRouteAsync(Tuyenxe busRoute)
        {
            if (busRoute == null)
            {
                throw new ArgumentNullException(nameof(busRoute));
            }

            _context.Tuyenxes.Remove(busRoute);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBusRouteAsync(Tuyenxe busRoute)
        {
            await _context.SaveChangesAsync();
        }
    }
}