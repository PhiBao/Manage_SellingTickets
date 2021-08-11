using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services
{
    public class ReviewService : IReviewService
    {
        private readonly QLBVXKContext _context;

        public ReviewService(QLBVXKContext context)
        {
            _context = context;
        }

        public async Task CreateReviewAsync(Danhgia review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }
            _context.Danhgias.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<Danhgia> GetReviewByIdAsync(int Id)
        {
            return await _context.Danhgias.FirstOrDefaultAsync(p => p.MaDanhGia == Id);
        }

        public async Task<IEnumerable<Danhgia>> GetReviewsByGarageIdAsync(int garageId)
        {
            return await _context.Danhgias.Where(p => p.MaNhaXe == garageId).OrderByDescending(p => p.MaDanhGia).ToListAsync();
        }

        public async Task<IEnumerable<Danhgia>> GetReviewsAsync()
        {
            return await _context.Danhgias.ToListAsync();
        }

        public async Task UpdateReviewAsync(Danhgia review)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(Danhgia review)
        {
            if (review == null)
            {
                throw new ArgumentNullException(nameof(review));
            }

            _context.Danhgias.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckAvailableAsync(int userId, int garageId)
        {
            Vexe ticket = await _context.Vexes.Where(p => p.MaKh == userId &&
                             p.MaChuyenXeNavigation.MaXeNavigation.MaNhaXe == garageId &&
                             p.TrangThai == true).FirstOrDefaultAsync();

           return ticket == null ? false : true;
        }
        
    }
}