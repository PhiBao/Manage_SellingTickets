using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class SeatService : ISeatService
    {
        private readonly QLBVXKContext _context;

        public SeatService(QLBVXKContext context) 
        {
            _context = context;
        }

        public async Task<Chongoi> GetSeatByIdAsync(int Id)
        {
            return await _context.Chongois.FirstOrDefaultAsync(p => p.MaChoNgoi == Id);
        }

        public async Task<IEnumerable<Chongoi>> GetSeatsAsync()
        {
            return await _context.Chongois.ToListAsync();
        }

        public async Task CreateSeatAsync(Chongoi seat)
        {
            if (seat == null) 
            {
                throw new ArgumentNullException(nameof(seat));
            }

            _context.Chongois.Add(seat);
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateSeatAsync(Chongoi seat) 
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chongoi>> GetSeatByBusTripIdAsync(int busTripId)
        {
            return await _context.Chongois.Where(p => p.MaChuyenXe == busTripId).ToListAsync();
        }
    }
}