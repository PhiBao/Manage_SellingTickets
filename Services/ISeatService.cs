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
        Task CreateSeatAsync(int SoChoTrong, int MaChuyenXe);
        Task UpdateSeatAsync(Chongoi seat);
        Task<IEnumerable<Chongoi>> GetSeatsByBusTripIdAsync(int busTripId);
        Task DeleteSeatAsync(Chongoi seat);

    }
}