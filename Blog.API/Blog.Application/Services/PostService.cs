using Blog.Core.Abstractions;
using Blog.DataAccess.Repositories;
using CSharpFunctionalExtensions;

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

        public async Task<IResult<string>> Update(int userId, IPostModel postModel)
        {
            var postOfUser = await IsPostOfUser(userId, postModel.Id);

            if (postOfUser.IsFailure)
                return Result.Failure<string>(postOfUser.Error);

            await _postRepository.Update(postModel);

            return Result.Success<string>($"Пост '{postModel.Id}' изменен");
        }

        public async Task<IResult<string>> Delete(int userId, Guid id)
        {
            var postOfUser = await IsPostOfUser(userId, id);

            if (postOfUser.IsFailure)
                return Result.Failure<string>(postOfUser.Error);

            await _postRepository.Delete(id);

            return Result.Success<string>($"Пост '{id}' удален");
        }

        private async Task<Result> IsPostOfUser(int userId, Guid id)
        {
            var post = await _postRepository.GetPost(id);

            if (post.IsFailure)
                return Result.Failure(post.Error);

            if (post.Value.AuthorId != userId)
                return Result.Failure("Пост принадлежит другому автору");

            return Result.Success();
        }

    }
}
