using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly QLBVXKContext _context;

        public HistoryService(QLBVXKContext context)
        {
            _context = context;
        }
        public async Task CreateHistoryAsync(Lichsutimkiem history)
        {
            if (history == null) 
            {
                throw new ArgumentNullException(nameof(history));
            }
            _context.Lichsutimkiems.Add(history);

            var count = await _context.Lichsutimkiems.CountAsync();
            if (count >= 5) 
            {
                var oldestHistory = await _context.Lichsutimkiems.FirstOrDefaultAsync();
                _context.Lichsutimkiems.Remove(oldestHistory);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lichsutimkiem>> GetHistoriesAsync()
        {
            return await _context.Lichsutimkiems.ToListAsync();
        }
    }
}