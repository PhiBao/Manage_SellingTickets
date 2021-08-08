using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class RevenueService : IRevenueService
    {
        private readonly QLBVXKContext _context;

        public RevenueService(QLBVXKContext context)
        {
            _context = context;
        }
        public async Task CreateRevenueAsync(Doanhthungay dayliRevenue)
        {
            if (dayliRevenue == null)
            {
                throw new ArgumentNullException(nameof(dayliRevenue));
            }

            _context.Doanhthungays.Add(dayliRevenue);
            await _context.SaveChangesAsync();
        }

        public async Task<Doanhthungay> GetRevenueByIdAsync(int id)
        {
            return await _context.Doanhthungays.Where(p => p.MaDoanhThuNgay == id).FirstOrDefaultAsync();
        }

        public async Task<Doanhthungay> GetRevenueByDayAsync(string date)
        {
             DateTime myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            return await _context.Doanhthungays.Where(p => p.Ngay.Date.Equals(myDate.Date)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Doanhthungay>> GetRevenuesInMonthAsync(string month)
        {
            string[] dateWithoutDay = month.Split('-');
            var list = await _context.Doanhthungays.Where(p => p.Ngay.Year == Int32.Parse(dateWithoutDay[0])
                        && p.Ngay.Month == Int32.Parse(dateWithoutDay[1])).ToListAsync();
            return list;
        }

        public async Task<IEnumerable<RevenueHelper>> GetRevenuesInYearAsync(string year)
        {
            List<RevenueHelper> list = new List<RevenueHelper>();
            int month = DateTime.Now.Month;
            int curYear = DateTime.Now.Year;
            if (Int32.Parse(year) < curYear) { month = 12; }

            for (int i = 1; i <= month; i++)
            {
                RevenueHelper item = new RevenueHelper();
                item.Thang = year + "-" + i.ToString();
                var dummies = await _context.Doanhthungays.Where(p => p.Ngay.Year == Int32.Parse(year)
                        && p.Ngay.Month == i).ToListAsync();

                foreach (var dummy in dummies)
                {
                    item.DoanhThu += dummy.TongDoanhThu.GetValueOrDefault();
                    item.SoVe += dummy.SoVe;
                }
                list.Add(item);
            }

            return list;
        }
        public async Task<IEnumerable<Doanhthungay>> GetRevenuesAsync()
        {
            return await _context.Doanhthungays.ToListAsync();
        }
    }
}