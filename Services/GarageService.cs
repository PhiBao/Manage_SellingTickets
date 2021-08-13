using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class GarageService : IGarageService
    {
        private readonly QLBVXKContext _context;

        public GarageService(QLBVXKContext context)
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

        public async Task DeleteGarageAsync(Nhaxe garage)
        {
            if (garage == null)
            {
                throw new ArgumentNullException(nameof(garage));
            }

            _context.Nhaxes.Remove(garage);
            await _context.SaveChangesAsync();
        }
    }
}