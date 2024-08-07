
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Configurations
{
    public class LoginConfiguration : IEntityTypeConfiguration<LoginEntity>
    {
        public void Configure(EntityTypeBuilder<LoginEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired();

            builder.Property(u => u.passwordHash)
                .IsRequired();

            builder
                .HasOne(l => l.User)
                .WithOne(u => u.UserLogin)
                .HasForeignKey<LoginEntity>(l => l.Id);
        }
    }
}