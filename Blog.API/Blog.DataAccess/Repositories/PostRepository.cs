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

        private const int PageSize = 10;

        public PostRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<IPostModel>> GetByAuthorId(int authorId, int page)
        {
            var postEntities = await _context.Post
                .Skip((page - 1) * PageSize)
                .Where(b => b.AuthorId == authorId)
                .Take(PageSize)
                .ToListAsync();

            foreach (var post in postEntities)
            {
                post.Views += 1;
            }

            await _context.SaveChangesAsync();

            var posts = new List<IPostModel>();

            posts.AddRange(postEntities
                .Select(p => new PostDto(p.Id, p.AuthorId, p.Title, File.ReadAllText(p.TextData), p.CreatedDate, p.Views))
                .ToList());

            return posts;
        }

        public async Task<List<IPostModel>> GetNewsFeed(int userId, int page)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var subscriptions = user?.Subscriptions
                .Select(s => s.Id)
                .ToList();

            var postEntities = await _context.Post
                .Where(p => subscriptions.Contains(p.AuthorId))
                .OrderByDescending(p => p.CreatedDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            foreach (var post in postEntities)
            {
                post.Views += 1;
            }

            await _context.SaveChangesAsync();

            var posts = new List<IPostModel>();
            
            posts.AddRange(postEntities
                .Select(p => new PostDto(p.Id, p.AuthorId, p.Title, File.ReadAllText(p.TextData), p.CreatedDate, p.Views))
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
