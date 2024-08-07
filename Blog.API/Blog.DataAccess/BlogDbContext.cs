using Blog.DataAccess.Configurations;
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
        }

        public DbSet<LoginEntity> Login { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Post { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new LoginConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
