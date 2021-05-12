using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class TicketService : ITicketService
    {
        private readonly QLBVXKContext _context;

        public TicketService(QLBVXKContext context)
        {
            _context = context;
        }

        public async Task CreateTicketAsync(IEnumerable<Vexe> ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            _context.Vexes.AddRange(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<Vexe> GetTicketByIdAsync(int id)
        {
            return await _context.Vexes.FirstOrDefaultAsync(p => p.MaVe == id);
        }

        public async Task<IEnumerable<Vexe>> GetTicketsAsync()
        {
            return await _context.Vexes.ToListAsync();
        }

        public async Task DeleteTicketAsync(Vexe ticket)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(nameof(ticket));
            }

            _context.Vexes.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Vexe>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Vexes.Where(p => p.MaKh == userId).ToListAsync();
        }

        public async Task<IEnumerable<Vexe>> GetTicketsByBusTripIdAsync(int busTripId)
        {
            return await _context.Vexes.Where(p => p.MaChuyenXe == busTripId).ToListAsync();
        }
    }
}