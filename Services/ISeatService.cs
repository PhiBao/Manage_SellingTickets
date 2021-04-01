using System.Collections.Generic;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface ISeatService 
    {
        public IEnumerable<Chongoi> GetSeats();
        public Chongoi GetSeatById(int id);
        public void CreateSeat(Chongoi seat);
        public void UpdateSeat(Chongoi seat);
        public bool SaveChanges();
    }
}