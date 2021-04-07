using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IBusTripService
    {
        Task<IEnumerable<Chuyenxe>> GetBusTripsAsync();
        Task<Chuyenxe> GetBusTripByIdAsync(int id);
        Task<IEnumerable<Chuyenxe>> GetBusTripByConditionAsync(int maBxDi, int maBxDen, string date);
        Task<IEnumerable<RevenueHelperDto>> GetRevenueByDayAsync(DateTime date);
        Task CreateBusTripAsync(Chuyenxe busTrip);
        Task UpdateBusTripAsync(Chuyenxe busTrip);
        Task DeleteBusTripAsync(Chuyenxe busTrip);

    }
}