using Eluryn.Users.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Eluryn.Users.Api.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ConfigureUser(modelBuilder);
    }

    private static void ConfigureUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users", "users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id");

            entity.Property(x => x.Username)
                .HasColumnName("username")
                .IsRequired();

            entity.Property(x => x.CreatedAtUtc)
                .HasColumnName("created_at_utc");

            entity.Property(x => x.UpdatedAtUtc)
                .HasColumnName("updated_at_utc");

            entity.HasIndex(x => x.Username)
                .IsUnique();
        });
    }
}