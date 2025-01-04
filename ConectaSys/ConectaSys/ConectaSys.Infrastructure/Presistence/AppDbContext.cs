using Microsoft.EntityFrameworkCore;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<LogEntry> Logs { get; set; }

        public DbSet<User> Users { get; set; }


     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<LogEntry>()
                .ToTable("logs");


            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("users");

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .IsRequired();


            modelBuilder.Entity<User>()
                .Property(u => u.Name)
                .HasColumnName("name")
                .HasColumnType("varchar(255)")
                .IsRequired();


            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(255)")
                .IsRequired();


            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasColumnType("varchar(255)")
                .IsRequired();


            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnName("createdat")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasColumnName("updatedat")
                .HasColumnType("timestamptz")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");


            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasColumnName("role")
                .HasColumnType("varchar(50)");


            modelBuilder.Entity<User>()
                .Property(u => u.Phone)
                .HasColumnName("phone")
                .HasColumnType("varchar(20)");


            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasColumnName("is_active")
                .HasColumnType("boolean")
                .HasDefaultValue(true);
        }
    }
}
