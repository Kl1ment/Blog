using Blog.Core.Abstractions;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public interface IPostService
    {
        Task<Guid> Create(IPostModel postModel);
        Task<List<IPostModel>> GetByAuthorId(int authorId, int page);
        Task<List<IPostModel>> GetNewsFeed(int userId, int page);
        Task<IResult<string>> Update(int userId, IPostModel postModel);
        Task<IResult<string>> Delete(int userId, Guid id);
    }
}