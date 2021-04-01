using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public class TicketService : ITicketService
    {
        private readonly QLBVXKContext _context;

        public TicketService(QLBVXKContext context)
        {
            _context = context;
        }

        public void CreateTicket(Vexe ticket)
        {
            if (ticket == null) 
            {
                throw new ArgumentNullException(nameof(ticket));
            }
            _context.Vexes.Add(ticket);
            _context.SaveChanges();
        }

        public Vexe GetTicketById(int id)
        {
            return _context.Vexes.FirstOrDefault(p => p.MaChuyenXe == id);
        }

        public IEnumerable<Vexe> GetTickets()
        {
            return _context.Vexes.ToList();
        }
    }
}