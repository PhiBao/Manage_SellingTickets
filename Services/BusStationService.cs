using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusStationService : IBusStationService
    {
        private readonly QLBVXKContext _context;

        public BusStationService(QLBVXKContext context)
        {
            _context = context;
        }

        public async Task CreateBusStationAsync(Benxe busStation)
        {
            if (busStation == null)
            {
                throw new ArgumentNullException(nameof(busStation));
            }
            _context.Benxes.Add(busStation);
            await _context.SaveChangesAsync();
        }

        public async Task<Benxe> GetBusStationByIdAsync(int Id)
        {
            return await _context.Benxes.FirstOrDefaultAsync(p => p.MaBx == Id);
        }

        public async Task<IEnumerable<Benxe>> GetBusStationsAsync()
        {
            return await _context.Benxes.ToListAsync();
        }
    }
}