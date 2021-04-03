using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IBusTripService
    {
        Task<IEnumerable<Chuyenxe>> GetBusTripsAsync();
        Task<Chuyenxe> GetBusTripByIdAsync(int id);
        Task<IEnumerable<int>> GetBusTripIdByBusRouteIdAsync(int id);
        Task<IEnumerable<int>> GetBusTripIdByBusIdAsync(int id);
        Task<IEnumerable<Chuyenxe>> GetBusTripByConditionAsync(int maBxDi, int maBxDen);
        Task<IEnumerable<int>> GetBusTripIdByStaffIdAsync(int staffId);
        Task CreateBusTripAsync(Chuyenxe busTrip);
        Task UpdateBusTripAsync(Chuyenxe busTrip);
        Task DeleteBusTripAsync(Chuyenxe busTrip);

    }
}