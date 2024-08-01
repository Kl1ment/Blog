using Blog.API.Models;

namespace Blog.DataAccess.Repositories
{
    public interface IUserLoginRepository
    {
        Task<string?> Create(UserLogin user);
        Task<string> Delete(string email);
        Task<UserLogin?> GetUser(string email);
        Task<string> Update(string email, string password);
    }
}