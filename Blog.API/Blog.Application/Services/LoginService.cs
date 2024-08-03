using Blog.API.Models;
using Blog.DataAccess.Repositories;

namespace Blog.Application.Services
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<bool> Login(string email, string password)
        {
            var userFromDB = await _loginRepository.GetUser(email);

            if (userFromDB == null)
            {
                return false;
            }

            return password.GetHashCode() == userFromDB.HashPassword;
        }

        public async Task<int> Register(LoginModel loginModel)
        {
            return await _loginRepository.CreateUser(loginModel);
        }

        public async Task<int> UpdateUser(int id, string email, string password)
        {
            int hashPassword = password.GetHashCode();

            return await _loginRepository.UpdateUser(id, email, hashPassword);
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _loginRepository.DeleteUser(id);
        }
    }
}
