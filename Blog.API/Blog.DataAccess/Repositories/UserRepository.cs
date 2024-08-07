using Blog.Core.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly BlogDbContext _context;

        public UserRepository(BlogDbContext context)
        {
            _context = context;
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
    }
}
