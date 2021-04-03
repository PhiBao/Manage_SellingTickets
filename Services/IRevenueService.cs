using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IRevenueService
    {
        Task CreateRevenueAsync(Doanhthungay dayliRevenue);
        Task UpdateRevenueAsync(Doanhthungay dayliRevenue);
    }
}