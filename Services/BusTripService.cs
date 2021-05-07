using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos;
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
                    p.MaTuyenXe == busRoute && p.NgayXuatBen.Date == myDate).ToListAsync();

            return busTrips;
        }

        public async Task<IEnumerable<RevenueHelperDto>> GetRevenueByDayAsync(DateTime date)
        {
            List<RevenueHelperDto> result = new List<RevenueHelperDto>();
            // string day = date.ToString("d");
            var busTrips = await _context.Chuyenxes.Where(p =>
                        p.NgayXuatBen.Date.Equals(date.Date))
                        .Select(p => new { p.MaXe, p.DonGia, p.SoChoDaDat }).ToListAsync();
            foreach (var busTrip in busTrips)
            {
                var license = await _context.Xes.Where(p => p.MaXe == busTrip.MaXe).Select(p => p.BienSoXe).FirstOrDefaultAsync();
                var item = new RevenueHelperDto
                {
                    BienSoXe = license,
                    VeDaBan = busTrip.SoChoDaDat.GetValueOrDefault(),
                    DoanhThu = busTrip.SoChoDaDat.GetValueOrDefault() * busTrip.DonGia.GetValueOrDefault()
                };

                result.Add(item);
            }

            return result;
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