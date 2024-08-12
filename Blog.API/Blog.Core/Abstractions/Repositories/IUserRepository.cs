using Blog.Core.Models;
using CSharpFunctionalExtensions;

namespace Blog.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(UserModel user);
        
        Task<int> Delete(int id);
        
        Task<UserModel?> GetById(int id);
        
        Task<UserModel?> GetByUserName(string userName);
        
        Task<int> Update(int id, string userName);

        Task<IResult> Subscribe(int userId, int authorId);

        Task<IResult> Unsubscribe(int userId, int authorId);

        Task<List<UserModel>?> GetSubscriptions(int userId);

        Task<List<UserModel>?> GetFollowers(int userId);
    }
}