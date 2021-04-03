using System;
using System.Collections;
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
        Task CreateTicketAsync(Vexe ticket);
        Task DeleteTicketAsync(Vexe ticket);

    }
}