using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly d1h6lskf7s3bc0Context _context;

        public HistoryService(d1h6lskf7s3bc0Context context)
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

            var count = await _context.Lichsutimkiems.Where(p => p.MaNd == history.MaNd).CountAsync();
            if (count >= 5) 
            {
                var oldestHistory = await _context.Lichsutimkiems.Where(p => p.MaNd == history.MaNd).FirstOrDefaultAsync();
                _context.Lichsutimkiems.Remove(oldestHistory);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Lichsutimkiem>> GetHistoriesByUserIdAsync(int userId)
        {
            return await _context.Lichsutimkiems.Where(p => p.MaNd == userId).ToListAsync();
        }

        public async Task DeleteHistoriesByUserIdAsync(int userId)
        {
            var histories = await _context.Lichsutimkiems.Where(p => p.MaNd == userId).ToListAsync();

            _context.Lichsutimkiems.RemoveRange(histories);
            await _context.SaveChangesAsync();
        }
    }
}