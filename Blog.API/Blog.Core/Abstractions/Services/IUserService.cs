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

        Task<IResult> Subscribe(int userId, int authorId);

        Task<IResult> Unsubscribe(int userId, int authorId);

        Task<List<UserModel>?> GetFollowers(int userId);

        Task<List<UserModel>?> GetSubscriptions(int userId);

    }
}