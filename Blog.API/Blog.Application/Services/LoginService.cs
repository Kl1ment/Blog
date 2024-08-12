using Blog.API.Models;
using Blog.Core.Models;
using Blog.DataAccess.Repositories;
using Blog.Infrastucture;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginService(
            ILoginRepository loginRepository,
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider)
        {
            _loginRepository = loginRepository;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<IResult<string>> Login(string email, string password)
        {
            var userFromDb = await _loginRepository.GetByEmail(email);

            if (userFromDb == null || !_passwordHasher.Verify(password, userFromDb.PasswordHash))
            {
                string error = "Неверный логин или пароль";

                return Result.Failure<string>(error);
            }

            var token = _jwtProvider.GenerateToken(userFromDb);

            return Result.Success(token);
        }

        public async Task<IResult<string>> Register(string userName, string email, string password)
        {
            if (await _loginRepository.GetByEmail(email) != null)
            {
                string error = "Данный Email уже используется другим пользователем";

                return Result.Failure<string>(error);
            }

            if (await _userRepository.GetByUserName(userName) != null)
            {
                string error = "Данное имя пользователя уже занято";

                return Result.Failure<string>(error);
            }

            string passwordHash = _passwordHasher.Generate(password);

            int id = await _loginRepository.Add(LoginModel.Create(email, passwordHash)); 
            await _userRepository.Add(UserModel.Create(id, userName));

            return Result.Success("Регистрация прошла успешно");
        }
    }
}
