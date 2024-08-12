using Blog.Core.Models;
using Blog.DataAccess.Entities;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly BlogDbContext _context;

        private const int PageSize = 10;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> Get(int page)
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderByDescending(u => u.Followers.Count())
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            return users
                .Select(u => UserModel.Create(u.Id, u.UserName)).ToList();
        }

        public async Task<UserModel?> GetByUserName(string userName)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == userName);

            if (userEntity == null) { return null; }

            return UserModel.Create(userEntity.Id, userEntity.UserName);
        }

        public async Task<UserModel?> GetById(int id)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (userEntity == null) { return null; }

            return UserModel.Create(userEntity.Id, userEntity.UserName);
        }

        //public async Task<UserModel?> GetWithPosts(int id)
        //{
        //    var userEntity = await _context.Users
        //        .AsNoTracking()
        //        .Include(u => u.Posts)
        //        .FirstOrDefaultAsync(u => u.Id == id);

        //    if (userEntity == null) { return null; }

        //    return userEntity;
        //}

        public async Task<int> Add(UserModel user)
        {           
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<int> Update(int id, string userName)
        {
            var user = await _context.Users
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.UserName, b => userName));

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Users
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<IResult> Subscribe(int userId, int authorId)
        {
            var (user, author) = await GetUserAndAuthor(userId, authorId);

            if (user == null || author == null || userId == authorId)
            {
                return Result.Failure("Ошибка");
            }

            user.Subscriptions.Add(author);
            author.Followers.Add(user);

            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<IResult> Unsubscribe(int userId, int authorId)
        {
            var (user, author) = await GetUserAndAuthor(userId, authorId);

            if (user == null || author == null || userId == authorId)
            {
                return Result.Failure("Ошибка");
            }

            user.Subscriptions.Remove(author);
            author.Followers.Remove(user);

            await _context.SaveChangesAsync();

            return Result.Success();
        }

        public async Task<List<UserModel>?> GetSubscriptions(int userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var subscriptions = user?.Subscriptions
                .Select(s => UserModel.Create(s.Id, s.UserName))
                .ToList();

            return subscriptions;
        }

        public async Task<List<UserModel>?> GetFollowers(int authorId)
        {
            var author = await _context.Users
                .AsNoTracking()
                .Include(a => a.Followers)
                .FirstOrDefaultAsync(a => a.Id == authorId);

            var Followers = author?.Followers
                .Select(s => UserModel.Create(s.Id, s.UserName))
                .ToList();

            return Followers;
        }

        async private Task<(UserEntity? user, UserEntity? author)> GetUserAndAuthor(int userId, int authorId)
        {
            var users = _context.Users;

            var user = await users
                .Include(u => u.Subscriptions)
                .FirstOrDefaultAsync(u => u.Id == userId);

            var author = await users
                .Include(a => a.Followers)
                .FirstOrDefaultAsync(a => a.Id == authorId);

            return (user, author);
        }
    }
}
