using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IRevenueService
    {
        Task CreateRevenueAsync(Doanhthungay dayliRevenue);
        Task<IEnumerable<Doanhthungay>> GetRevenuesInMonthAsync(string month);
        Task<IEnumerable<RevenueHelper>> GetRevenuesInYearAsync(string year);
        Task<IEnumerable<Doanhthungay>> GetRevenuesAsync();
        Task<Doanhthungay> GetRevenueByDayAsync(string date);
        Task<Doanhthungay> GetRevenueByIdAsync(int id);
    }
}