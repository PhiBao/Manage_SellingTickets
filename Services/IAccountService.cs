using System.Threading.Tasks;
using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAccountService
    {
        Task CreateAccountAsync(Taikhoan account, byte role);
        Task DeleteAccountAsync(Taikhoan account);
        Task<Taikhoan> GetAccountByIdAsync(int id);
        Task UpdateAccountAsync(Taikhoan account);
    }
}