using Blog.Core.Abstractions;

namespace Blog.Application.Services
{
    public interface IPostService
    {
        Task<Guid> Create(IPostModel postModel);
        Task<List<IPostModel>> GetByAuthorId(int authorId, int page);
        Task<List<IPostModel>> GetNewsFeed(int userId, int page);
        Task<Guid> Update(IPostModel postModel);
        Task<Guid> Delete(Guid id);
    }
}