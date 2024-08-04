using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options)
            : base(options)
        {
        }
        public DbSet<PostEntity> Post { get; set; }

    }
}
