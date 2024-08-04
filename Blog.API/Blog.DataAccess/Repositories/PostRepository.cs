using Blog.Core.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class PostRepository : DbContext
    {
        private readonly PostDbContext _context;

        public PostRepository(PostDbContext context)
        {
            _context = context;
        }

        public async Task<List<PostModel>> GetByAuthorId(int authorId)
        {
            var postEntities = await _context.Post
                .AsNoTracking()
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();

            var posts = postEntities
                .Select(b => PostModel.CreateFromDb(b.Id, b.AuthorId, b.Title, b.TextData, b.CreatedDate, b.Views))
                .ToList();

            return posts;
        }
        
        public async Task<Guid> Create(PostModel postModel)
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



    }
}
