using Blog.Core.Models;
using Blog.DataAccess.Repositories;
using Blog.Infrastucture;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository,
            ILoginRepository loginRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _loginRepository = loginRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<IResult<UserModel, string>> GetUserById(int id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return Result.Failure<UserModel, string>("Пользователь не найден");
            }

            return Result.Success(user);
        }

        public async Task<IResult<UserModel, string>> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);

            if (user == null)
            {
                return Result.Failure<UserModel, string>("Пользователь не найден");
            }

            return Result.Success(user);
        }

        public async Task<IResult<string>> UpdateUser(string email, string password, UserModel userModel)
        {
            var userLogin = await _loginRepository.GetByEmail(email);
            var userName = await _userRepository.GetByUserName(userModel.UserName);

            if (userLogin != null && userLogin.Id != userModel.Id)
            {
                string error = "Данный Email уже используется другим пользователем";

                return Result.Failure<string>(error);
            }

            if (userName != null && userName.Id != userModel.Id)
            {
                return Result.Failure<string>("Данное имя пользователя уже занято");
            }

            string hashPassword = _passwordHasher.Generate(password);

            await _loginRepository.Update(userModel.Id, email, hashPassword);
            await _userRepository.Update(userModel.Id, userModel.UserName);

            return Result.Success("Изменения сохранены");
        }

        public async Task<IResult> DeleteUser(int id)
        {
            await _loginRepository.Delete(id);
            await _userRepository.Delete(id);

            return Result.Success();
        }
    }
}
