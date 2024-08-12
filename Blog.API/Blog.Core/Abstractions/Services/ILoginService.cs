using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public interface ILoginService
    {
        Task<IResult<string>> Login(string email, string password);
        Task<IResult<string>> Register(string userName, string email, string password);
    }
}