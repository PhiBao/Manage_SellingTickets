using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IHistoryService
    {
        Task CreateHistoryAsync(Lichsutimkiem history);
        Task<IEnumerable<Lichsutimkiem>> GetHistoriesByUserIdAsync(int userId);
        Task DeleteHistoriesByUserIdAsync(int userId);
    }
}