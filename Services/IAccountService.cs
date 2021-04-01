using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAccountService
    {
        Task CreateAccountAsync(Taikhoan account);
        Task DeleteAccountAsync(Taikhoan account);
        Task<Taikhoan> GetAccountByIdAsync(int id);
        Task UpdateAccountAsync(Taikhoan account);
    }
}