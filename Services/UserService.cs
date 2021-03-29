using System;
using System.Collections.Generic;
using System.Linq;
using backend.Models;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly QLBVXKContext _context;

        public UserService(QLBVXKContext context)
        {
            _context = context;
        }

        public void CreateCustomer(Nguoidung customer)
        {
            if (customer == null) 
            {
                throw new ArgumentNullException(nameof(customer));
            }
            customer.Vaitro = 3;
            _context.Nguoidungs.Add(customer);
            _context.SaveChanges();
        }

        public void CreateStaff(Nguoidung staff)
        {
            if (staff == null) 
            {
                throw new ArgumentNullException(nameof(staff));
            }
            staff.Vaitro = 2;
            _context.Nguoidungs.Add(staff);
            _context.SaveChanges();
        }

        public Nguoidung GetCustomerById(int id)
        {
            return _context.Nguoidungs.Where(p => p.Vaitro == 3).FirstOrDefault(p => p.MaNd == id);
        }

        public IEnumerable<Nguoidung> GetCustomers()
        {
            return _context.Nguoidungs.Where(p => p.Vaitro == 3).ToList();
        }

        public Nguoidung GetStaffById(int id)
        {
            return _context.Nguoidungs.Where(p => p.Vaitro == 2).FirstOrDefault(p => p.MaNd == id);
        }

        public IEnumerable<Nguoidung> GetStaffs()
        {
            return _context.Nguoidungs.Where(p => p.Vaitro == 2).ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
            
        }

        public void UpdateUser(Nguoidung user)
        {
        }
    }
}