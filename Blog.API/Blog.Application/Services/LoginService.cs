using Blog.API.Models;
using Blog.DataAccess.Repositories;
using Blog.Infrastucture;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginService(
            ILoginRepository loginRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _loginRepository = loginRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<List<LoginModel>> GetAllUsers()
        {
            return await _loginRepository.GetAllUsers();
        }

        public async Task<IResult<string>> Login(string email, string password)
        {
            var userFromDB = await _loginRepository.GetUserByEmail(email);

            if (userFromDB == null || !_passwordHasher.Verify(password, userFromDB.PasswordHash))
            {
                string error = "Неверный логин или пароль";

                return Result.Failure<string>(error);
            }

            var token = _jwtProvider.GenerateToken(userFromDB);

            return Result.Success(token);
        }

        public async Task<IResult<string>> Register(string email, string password)
        {
            if (await _loginRepository.GetUserByEmail(email) != null)
            {
                string error = "Данный Email уже используется другим пользователем";

                return Result.Failure<string>(error);
            }

            string passwordHash = _passwordHasher.Generate(password);

            var newUser = LoginModel.Register(email, passwordHash);

            await _loginRepository.CreateUser(newUser);

            return Result.Success("Регистрация прошла успешно");
        }

        public async Task<int> UpdateUser(int id, string email, string password)
        {
            string hashPassword = _passwordHasher.Generate(password);

            return await _loginRepository.UpdateUser(id, email, hashPassword);
        }

        public async Task<int> DeleteUser(int id)
        {
            return await _loginRepository.DeleteUser(id);
        }
    }
}
