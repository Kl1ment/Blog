using Blog.API.Models;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly BlogDbContext _context;

        public LoginRepository(BlogDbContext contxt)
        {
            _context = contxt;
        }

        public async Task<List<LoginModel>> GetAllUsers()
        {
            var userEntities = await _context.UsersLogin
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(b => LoginModel.Login(b.Id, b.Email, b.HashPassword))
                .ToList();

            return users;
        }

        public async Task<LoginModel?> GetUser(string email)
        {
            var userEntity = await _context.UsersLogin
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (userEntity == null) { return null; }

            var user = LoginModel.Login(userEntity.Id, userEntity.Email, userEntity.HashPassword);

            return user;
        }

        public async Task<int> CreateUser(LoginModel user)
        {
            var newUser = new UserLoginEntity
            {
                Id = user.Id,
                Email = user.Email,
                HashPassword = user.HashPassword
            };

            await _context.UsersLogin.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> UpdateUser(int id, string email, int hashPassword)
        {
            var user = await _context.UsersLogin
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Email, b => email)
                    .SetProperty(b => b.HashPassword, b => hashPassword));

            return id;
        }

        public async Task<int> DeleteUser(int id)
        {
            await _context.UsersLogin
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }

    }
}
