using Blog.Core.Abstractions;

namespace Blog.DataAccess.Repositories
{
    public interface IPostRepository
    {
        Task<Guid> Create(IPostModel postModel);
        Task<List<IPostModel>> GetByAuthorId(int authorId);
        Task<Guid> Update(IPostModel postModel);
        Task<Guid> Delete(Guid id);
    }
}