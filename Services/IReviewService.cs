using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Danhgia>> GetReviewsAsync();
        Task<Danhgia> GetReviewByIdAsync(int id);
        Task<Danhgia> GetReviewByTicketIdAsync(int ticketId);
        Task<IEnumerable<Danhgia>> GetReviewsByGarageIdAsync(int garageId);
        Task CreateReviewAsync(Danhgia review);
        Task UpdateReviewAsync(Danhgia review);
        Task DeleteReviewAsync(Danhgia review);
        Task<bool> CheckAvailableAsync(int userId, int garageId);
    }
}