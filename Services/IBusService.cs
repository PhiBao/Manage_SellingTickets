using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IBusService
    {
        Task<IEnumerable<Xe>> GetBusesAsync();
        Task CreateBusAsync(Xe bus);
        Task DeleteBusAsync(Xe bus);
        Task<Xe> GetBusByIdAsync(int id);
        Task UpdateBusAsync(Xe bus);

    }
}