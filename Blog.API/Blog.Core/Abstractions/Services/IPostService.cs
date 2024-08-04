using Blog.Core.Abstractions;

namespace Blog.Application.Services
{
    public interface IPostService
    {
        Task<Guid> Create(IPostModel postModel);
        Task<List<IPostModel>> GetByAuthorId(int authorId);
        Task<Guid> Update(IPostModel postModel);
        Task<Guid> Delete(Guid id);
    }
}