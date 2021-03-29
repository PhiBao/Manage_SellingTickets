using System.Collections.Generic;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IBusStationService 
    {
        public IEnumerable<Benxe> GetBusStations();
        public Benxe GetBusStationById(int id);
        public void CreateBusStation(Benxe busStation);
    }
}