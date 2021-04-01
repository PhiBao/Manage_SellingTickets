using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface ISeatService 
    {
        Task<IEnumerable<Chongoi>> GetSeatsAsync();
        Task<Chongoi> GetSeatByIdAsync(int id);
        Task CreateSeatAsync(Chongoi seat);
        Task UpdateSeatAsync(Chongoi seat);
        Task<IEnumerable<int>> GetSeatByBusTripIdAsync(int busTripId);
        
    }
}