using Blog.Core.Abstractions;
using Blog.Core.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly string storageDirectory = $"{Directory.GetCurrentDirectory()}/Storage/PostData";
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
                .Select(b => new PostDto(b.Id, b.AuthorId, b.Title, File.ReadAllText(b.TextData), b.CreatedDate, b.Views ))
                .ToList());

            return posts;
        }

        public async Task<Guid> Create(IPostModel postModel)
        {
            string directory = $"{storageDirectory}/{postModel.AuthorId}";
            Directory.CreateDirectory(directory);

            string filePath = directory + $"/{postModel.Id}.txt";
            File.Create(filePath).Close();

            await File.WriteAllTextAsync(filePath, postModel.TextData);

            var postEntity = new PostEntity
            {
                Id = postModel.Id,
                AuthorId = postModel.AuthorId,
                Title = postModel.Title,
                TextData = filePath,
                CreatedDate = postModel.CreatedDate,
                Views = postModel.Views,
            };

            await _context.Post.AddAsync(postEntity);
            await _context.SaveChangesAsync();

            return postEntity.Id;
        }

        public async Task<Guid> Update(IPostModel postModel)
        {
            var post = await _context.Post
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == postModel.Id);

            if (post == null)
                return postModel.Id;

            await File.WriteAllTextAsync(post.TextData, postModel.TextData);

            await _context.Post
                .Where(p => p.Id == postModel.Id)
                .ExecuteUpdateAsync(p => p
                    .SetProperty(s => s.Title, s => postModel.Title));

            await _context.SaveChangesAsync();

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
