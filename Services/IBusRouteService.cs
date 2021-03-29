using System.Collections.Generic;
using backend.Models;

namespace backend.Services
{
    public interface IBusRouteService
    {
        public IEnumerable<Tuyenxe> GetBusRoutes();
        public Tuyenxe GetBusRouteById(int id);
        public void CreateBusRoute(Tuyenxe busRoute);
    }
}