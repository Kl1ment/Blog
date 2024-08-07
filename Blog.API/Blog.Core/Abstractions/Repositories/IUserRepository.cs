using Blog.Core.Models;

namespace Blog.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<int> Add(UserModel user);
        Task<int> Delete(int id);
        Task<UserModel?> GetById(int id);
        Task<UserModel?> GetByUserName(string userName);
        Task<int> Update(int id, string userName);
    }
}