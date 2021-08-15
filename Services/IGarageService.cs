using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IGarageService
    {
        Task<IEnumerable<Nhaxe>> GetGaragesAsync();
        Task<Nhaxe> GetGarageByIdAsync(int id);
        Task CreateGarageAsync(Nhaxe garage);
        Task DeleteGarageAsync(Nhaxe garage);
        Task UpdateGarageAsync(Nhaxe garage);
    }
}