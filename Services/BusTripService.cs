using System;
using System.Collections;
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

        public async Task<bool> CreateBusTripAsync(Chuyenxe busTrip)
        {
            if (busTrip == null)
            {
                throw new ArgumentNullException(nameof(busTrip));
            }
            
            var soChoNgoi = await _context.Xes.Where(p => p.MaXe == busTrip.MaXe).Select(p => p.SoChoNgoi).FirstOrDefaultAsync();
            if (busTrip.SoChoDaDat.GetValueOrDefault() > soChoNgoi.GetValueOrDefault()) return false;
            busTrip.SoChoTrong = soChoNgoi.GetValueOrDefault() - busTrip.SoChoDaDat.GetValueOrDefault();

            _context.Chuyenxes.Add(busTrip);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task DeleteBusTripAsync(Chuyenxe busTrip)
        {
            if (busTrip == null)
            {
                throw new ArgumentNullException(nameof(busTrip));
            }

            var ticketsByBusTrip = await _context.Vexes.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
            _context.Vexes.RemoveRange(ticketsByBusTrip);
            var seatsByBusTrip = await _context.Chongois.Where(p => p.MaChuyenXe == busTrip.MaChuyenXe).ToListAsync();
            _context.Chongois.RemoveRange(seatsByBusTrip);

            _context.Chuyenxes.Remove(busTrip);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chuyenxe>> GetBusTripByConditionAsync(int maBxDi, int maBxDen, string date)
        {
            var busRoute = await _context.Tuyenxes.Where(p => p.MaBxden == maBxDen && p.MaBxdi == maBxDi)
                            .Select(p => p.MaTuyenXe).FirstOrDefaultAsync();

            DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);

            var busTrips = await _context.Chuyenxes.Where(p =>
                    p.MaTuyenXe == busRoute && p.NgayXuatBen.Date.Equals(myDate.Date)).ToListAsync();

            return busTrips;
        }

        public async Task<IEnumerable<RevenueByDay>> GetRevenueByDayAsync(string date)
        {
            List<RevenueByDay> status;

            DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            status = await _context.Chuyenxes
                        .Where(p => p.NgayXuatBen.Date.Equals(myDate.Date))
                        .GroupBy(p => p.DonGia)
                        .Select(q => new RevenueByDay {                            
                            LoaiGia = q.Key.GetValueOrDefault(),
                            VeDaBan = q.Sum(p => p.SoChoDaDat.GetValueOrDefault())
                        }).ToListAsync();
            return status;
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
    }
}