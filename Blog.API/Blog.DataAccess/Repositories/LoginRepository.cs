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

        public async Task<List<LoginModel>> GetAll()
        {
            var userEntities = await _context.Login
                .AsNoTracking()
                .ToListAsync();

            var users = userEntities
                .Select(b => LoginModel.Create(b.Id, b.Email, b.passwordHash))
                .ToList();

            return users;
        }

        public async Task<LoginModel?> GetByEmail(string email)
        {
            var userEntity = await _context.Login
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);

            if (userEntity == null) { return null; }

            var user = LoginModel.Create(userEntity.Id, userEntity.Email, userEntity.passwordHash);

            return user;
        }

        public async Task<int> Add(LoginModel user)
        {
            var newUser = new LoginEntity
            {
                Email = user.Email,
                passwordHash = user.PasswordHash
            };

            await _context.Login.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser.Id;
        }

        public async Task<int> Update(int id, string email, string passwordHash)
        {
            var user = await _context.Login
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Email, b => email)
                    .SetProperty(b => b.passwordHash, b => passwordHash));

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<int> Delete(int id)
        {
            await _context.Login
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            await _context.SaveChangesAsync();

            return id;
        }
    }
}
