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

        public async Task<List<LoginModel>> GetAllUsers()
        {
            return await _loginRepository.GetAllUsers();
        }

        public async Task<string> Login(string email, string password)
        {
            string error = string.Empty;

            var userFromDB = await _loginRepository.GetUser(email);

            if (userFromDB == null)
            {
                error = "Пользователь не найден";

                return error;
            }
            
            if (password.GetHashCode() != userFromDB.HashPassword)
                error = "Неверный пароль";

            return error;
        }

        public async Task<string> Register(LoginModel loginModel)
        {
            string error = string.Empty;

            if (await _loginRepository.GetUser(loginModel.Email) != null)
            {
                error = "Данный Email уже используется другим пользователем";

                return error;
            }

            await _loginRepository.CreateUser(loginModel);

            return error;
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
