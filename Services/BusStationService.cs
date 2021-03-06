using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusStationService : IBusStationService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public BusStationService(d1h6lskf7s3bc0Context context)
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

        public async Task DeleteBusStationAsync(Benxe busStation)
        {
            if (busStation == null)
            {
                throw new ArgumentNullException(nameof(busStation));
            }

            _context.Benxes.Remove(busStation);
            await _context.SaveChangesAsync();
        }
    }
}