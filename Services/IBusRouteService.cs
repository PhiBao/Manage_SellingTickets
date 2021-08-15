using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IBusRouteService
    {
        Task<IEnumerable<Tuyenxe>> GetBusRoutesAsync();
        Task<IEnumerable<Tuyenxe>> GetBusRoutesByGarageIdAsync(int garageId);
        Task<Tuyenxe> GetBusRouteByIdAsync(int id);
        Task CreateBusRouteAsync(Tuyenxe busRoute);
        Task DeleteBusRouteAsync(Tuyenxe busRoute);
        Task UpdateBusRouteAsync(Tuyenxe busRoute);
        Task<IEnumerable<Tuyenxe>> SearchBusRoutesByName(int depId, int destId);
    }
}