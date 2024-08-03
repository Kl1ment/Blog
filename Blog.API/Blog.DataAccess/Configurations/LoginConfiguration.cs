
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Configurations
{
    public class LoginConfiguration : IEntityTypeConfiguration<UserLoginEntity>
    {
        public void Configure(EntityTypeBuilder<UserLoginEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Email)
                .IsRequired();

            builder.Property(b => b.HashPassword)
                .IsRequired();
        }
    }
}
