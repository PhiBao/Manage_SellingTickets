using System.Collections.Generic;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        public IEnumerable<Nguoidung> GetCustomers();
        public Nguoidung GetCustomerById(int id);
        public IEnumerable<Nguoidung> GetStaffs();
        public Nguoidung GetStaffById(int id);
        public void CreateStaff(Nguoidung staff);
        public void CreateCustomer(Nguoidung customer);
        public void UpdateUser(Nguoidung user);
        public bool SaveChanges();
    }
}