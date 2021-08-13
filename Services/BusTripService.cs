using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class BusTripService : IBusTripService
    {
        private readonly QLBVXKContext _context;

        public BusTripService(QLBVXKContext context)
        {
            _context = context;
        }

        public async Task CreateBusTripAsync(Chuyenxe busTrip)
        {
            if (busTrip == null)
            {
                throw new ArgumentNullException(nameof(busTrip));
            }
            
            busTrip.SoChoTrong = await _context.Xes.Where(p => p.MaXe == busTrip.MaXe).Select(p => p.SoChoNgoi).FirstOrDefaultAsync();
            _context.Chuyenxes.Add(busTrip);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBusTripAsync(Chuyenxe busTrip)
        {
            if (busTrip == null)
            {
                throw new ArgumentNullException(nameof(busTrip));
            }

            var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
            _context.Vexes.RemoveRange(ticketsByBusTrip);
            _context.Chuyenxes.Remove(busTrip);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chuyenxe>> GetBusTripByConditionAsync(int maBxDi, int maBxDen, string date)
        {
            var busRoutes = await _context.Tuyenxes.Where(p => p.MaBxden == maBxDen && p.MaBxdi == maBxDi)
                            .Select(p => p.MaTuyenXe).ToListAsync();

            List<Chuyenxe> busTrips = new List<Chuyenxe>();
            foreach (var busRoute in busRoutes) {
                var schedule = await _context.Chuyenxes.Where(p => p.MaTuyenXe == busRoute).ToListAsync();
                foreach (var busTrip in schedule) {
                    if (CheckSameDate(busTrip.LichTrinh, date)) { busTrips.Add(busTrip); }
                }
            }

            return busTrips;
        }

        public async Task<Chuyenxe> GetBusTripByIdAsync(int id)
        {
            return await _context.Chuyenxes.FirstOrDefaultAsync(p => p.MaChuyenXe == id);
        }

        public async Task<IEnumerable<Chuyenxe>> GetBusTripsAsync()
        {
            return await _context.Chuyenxes.ToListAsync();
        }

        public async Task UpdateBusTripAsync(Chuyenxe busTrip)
        {
            await _context.SaveChangesAsync();
        }

        private bool CheckSameDate(string schedule, string date) {
            var numbers = Array.ConvertAll(schedule.ToCharArray(), c => (int)Char.GetNumericValue(c));
            DateTime today = DateTime.Today;

            foreach (var number in numbers) {
                DateTime dayOfSchedule = (number > (int)today.DayOfWeek) ? today.AddDays(number - (int)today.DayOfWeek)
                                                                    : today.AddDays(7 + number - (int)today.DayOfWeek);                  
                if (dayOfSchedule.ToString("yyyy-MM-dd").Equals(date)) { return true; }
            }

            return false;
        }
    }
}