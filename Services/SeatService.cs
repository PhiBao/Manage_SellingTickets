using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class SeatService : ISeatService
    {
        private readonly QLBVXKContext _context;

        public SeatService(QLBVXKContext context) 
        {
            _context = context;
        }

        public Chongoi GetSeatById(int Id)
        {
            return _context.Chongois.FirstOrDefault(p => p.MaChoNgoi == Id);
        }

        public IEnumerable<Chongoi> GetSeats()
        {
            return _context.Chongois.ToList();
        }

        public void CreateSeat(Chongoi seat)
        {
            if (seat == null) 
            {
                throw new ArgumentNullException(nameof(seat));
            }

            _context.Chongois.Add(seat);
            _context.SaveChanges();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
            
        }
        
        public void UpdateSeat(Chongoi seat) {}

    }
}