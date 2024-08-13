using Blog.Core.Abstractions;
using CSharpFunctionalExtensions;

namespace Blog.DataAccess.Repositories
{
    public interface IPostRepository
    {
        Task<IResult<IPostModel, string>> GetPost(Guid id);

        Task<Guid> Create(IPostModel postModel);
        
        Task<List<IPostModel>> GetByAuthorId(int authorId, int page);
        
        Task<List<IPostModel>> GetNewsFeed(int userId, int page);
        
        Task<Guid> Update(IPostModel postModel);
        
        Task<Guid> Delete(Guid id);
    }
}