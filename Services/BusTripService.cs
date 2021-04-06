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

            _context.Chuyenxes.Remove(busTrip);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chuyenxe>> GetBusTripByConditionAsync(int maBxDi, int maBxDen)
        {
            var busRoute = await _context.Tuyenxes.Where(p => p.MaBxden == maBxDen && p.MaBxdi == maBxDi)
                            .Select(p => p.MaTuyenXe).FirstOrDefaultAsync();

            var busTrips = await _context.Chuyenxes.Where(p => p.MaTuyenXe == busRoute).ToListAsync();

            return busTrips;
        }

        public async Task<IEnumerable<RevenueHelperDto>> GetRevenueByDayAsync(DateTime date)
        {
            List<RevenueHelperDto> result = new List<RevenueHelperDto>();
            var busTrips = await _context.Chuyenxes.Where(p => p.NgayXuatBen.Equals(date)).Select(
                                                p => new { p.MaXe, p.DonGia, p.SoChoDaDat }).ToListAsync();
            foreach (var busTrip in busTrips)
            {
                var license = await _context.Xes.Where(p => p.MaXe == busTrip.MaXe).Select(p => p.BienSoXe).FirstOrDefaultAsync();
                var item = new RevenueHelperDto
                {
                    BienSoXe = license,
                    VeDaBan = busTrip.SoChoDaDat.GetValueOrDefault(),
                    DoanhThu = busTrip.SoChoDaDat.GetValueOrDefault() * busTrip.DonGia.GetValueOrDefault()
                };
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