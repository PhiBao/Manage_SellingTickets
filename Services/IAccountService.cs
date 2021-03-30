using backend.Dtos;
using backend.Models;

namespace backend.Services
{
    public interface IAccountService
    {
        public void CreateAccount(Taikhoan account);
        public void DeleteAccount(Taikhoan account);
        public Taikhoan GetAccountById(int id);
        public void UpdateAccount(Taikhoan account);
        public bool SaveChanges();
    }
}