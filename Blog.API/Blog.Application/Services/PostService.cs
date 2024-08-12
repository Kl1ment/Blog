using Blog.Core.Abstractions;
using Blog.DataAccess.Repositories;

namespace Blog.Application.Services
{
    public class PostService : IPostService
    {

        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<IPostModel>> GetByAuthorId(int authorId, int page)
        {
            return await _postRepository.GetByAuthorId(authorId, page);
        }

        public async Task<List<IPostModel>> GetNewsFeed(int userId, int page)
        {
            return await _postRepository.GetNewsFeed(userId, page);
        }

        public async Task<Guid> Create(IPostModel postModel)
        {
            return await _postRepository.Create(postModel);
        }

        public async Task<Guid> Update(IPostModel postModel)
        {
            return await _postRepository.Update(postModel);
        }

        public async Task<Guid> Delete(Guid id)
        {
            return await _postRepository.Delete(id);
        }

    }
}
