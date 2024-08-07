
using Blog.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder
            .HasOne(u => u.UserLogin)
            .WithOne(l => l.User)
            .HasForeignKey<UserEntity>(u => u.Id);

        builder
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);

        builder
            .HasMany(u => u.Subscriptions)
            .WithMany(u => u.Folowers);

        builder
            .HasMany(u => u.Folowers)
            .WithMany(u => u.Subscriptions);
    }
}
