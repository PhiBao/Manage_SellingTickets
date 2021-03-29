using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAccountService
    {
        public void createAccount(Taikhoan account);
        public void deleteAccount(int id);
        public Taikhoan GetAccountById(int id);
        public void UpdateAccount(Taikhoan account);
        public bool SaveChanges();
    }
}