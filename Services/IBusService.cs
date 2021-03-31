using System.Collections.Generic;
using backend.Models;

namespace backend.Services
{
    public interface IBusService
    {
        public IEnumerable<Xe> GetBuses();
        public void CreateBus(Xe bus);
        public void DeleteBus(Xe bus);
        public Xe GetBusById(int id);
        public void UpdateBus(Xe bus);
        public bool SaveChanges();
    }
}