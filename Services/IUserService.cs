using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Nguoidung>> GetCustomersAsync();
        Task<Nguoidung> GetCustomerByIdAsync(int id);
        Task<IEnumerable<Nguoidung>> GetStaffsAsync();
        Task<Nguoidung> GetUserByIdAsync(int id);
        Task<Nguoidung> GetStaffByIdAsync(int id);
        Task UpdateUserAsync(Nguoidung user);
        Task<IEnumerable<Nguoidung>> SearchStaffsByName(string name);
    }
}