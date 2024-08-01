using Blog.API.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class UserLoginRepository : IUserLoginRepository
    {

        private readonly BlogDbContext _context;

        public UserLoginRepository(BlogDbContext contxt)
        {
            _context = contxt;
        }

        public async Task<UserLogin?> GetUser(string email)
        {
            var userEntity = await _context.UsersLogin
                .FindAsync(email);

            if (userEntity == null) { return null; }

            var user = UserLogin.Create(userEntity.Email, userEntity.HashPassword);

            return user;
        }

        public async Task<string?> Create(UserLogin user)
        {
            var newUser = new UserLoginEntity
            {
                Email = user.Email,
                HashPassword = user.HashPassword
            };

            await _context.UsersLogin.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return user.Email;
        }

        public async Task<string> Update(string email, string password)
        {
            var user = await _context.UsersLogin
                .Where(b => b.Email == email)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Email, b => email)
                    .SetProperty(b => b.HashPassword, b => password.GetHashCode()));

            return email;
        }

        public async Task<string> Delete(string email)
        {
            await _context.UsersLogin
                .Where(b => b.Email == email)
                .ExecuteDeleteAsync();

            return email;
        }

    }
}
