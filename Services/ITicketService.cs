using System;
using System.Collections;
using System.Collections.Generic;
using backend.Models;

namespace backend.Services
{
    public interface ITicketService 
    {
        public IEnumerable<Vexe> GetTickets();
        public Vexe GetTicketById(int id);
        public void CreateTicket(Vexe ticket);
    }
}