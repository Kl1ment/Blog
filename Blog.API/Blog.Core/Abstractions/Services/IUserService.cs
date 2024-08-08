using Blog.Core.Models;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public interface IUserService
    {
        Task<IResult> DeleteUser(int id);
        Task<IResult<UserModel, string>> GetUserById(int id);
        Task<IResult<UserModel, string>> GetUserByUserName(string userName);
        Task<IResult<string>> UpdateUser(string email, string password, UserModel userModel);
    }
}