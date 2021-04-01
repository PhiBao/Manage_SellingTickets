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