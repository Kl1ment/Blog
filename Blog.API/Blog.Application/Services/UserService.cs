using Blog.Core.Models;
using Blog.DataAccess.Repositories;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

        public async Task<IResult> UpdateUser(UserModel user)
        {
            await _userRepository.Update(user.Id, user.UserName);

            return Result.Success(user);
        }

        public async Task<IResult> DeleteUser(int id)
        {
            await _userRepository.Delete(id);

            return Result.Success(id);
        }
    }
}
