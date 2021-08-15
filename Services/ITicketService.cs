using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Vexe>> GetTicketsAsync();
        Task<IEnumerable<Vexe>> GetTicketsByUserIdAsync(int userId);
        Task<IEnumerable<Vexe>> GetTicketsByBusTripIdAsync(int busTripId);
        Task<Vexe> GetTicketByIdAsync(int id);
        Task<IEnumerable<int>> GetSeatsByBusTripIdAsync(int busTripId, string date);
        Task CreateTicketAsync(IEnumerable<Vexe> ticket);
        Task DeleteTicketAsync(Vexe ticket);
        Task<bool?> CheckAvailableAsync(int busTripId, string date, int seatId);
        Task UpdateTicketAsync(Vexe ticket);
        Task<IEnumerable<RevenueByDay>> GetRevenueByDayAsync(string date);
    }
}