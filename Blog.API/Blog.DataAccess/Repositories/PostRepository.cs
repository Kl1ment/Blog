using Blog.Core.Abstractions;
using Blog.Core.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogDbContext _context;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<IPostModel>> GetByAuthorId(int authorId)
        {
            var postEntities = await _context.Post
                .AsNoTracking()
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();

            var posts = new List<IPostModel>();

            posts.AddRange(postEntities
                .Select(b => new PostResponse(b.Id, b.AuthorId, b.Title, b.TextData, b.CreatedDate, b.Views))
                .ToList());

            return posts;
        }

        public async Task<Guid> Create(IPostModel postModel)
        {
            var postEntity = new PostEntity
            {
                Id = postModel.Id,
                AuthorId = postModel.AuthorId,
                Title = postModel.Title,
                TextData = postModel.TextData,
                CreatedDate = postModel.CreatedDate,
                Views = postModel.Views,
            };

            await _context.Post.AddAsync(postEntity);
            await _context.SaveChangesAsync();

            return postEntity.Id;
        }

        public async Task<Guid> Update(IPostModel postModel)
        {
            return postModel.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Post
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
