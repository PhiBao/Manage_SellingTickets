using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IBusStationService 
    {
        Task<IEnumerable<Benxe>> GetBusStationsAsync();
        Task<Benxe> GetBusStationByIdAsync(int id);
        Task CreateBusStationAsync(Benxe busStation);
    }
}