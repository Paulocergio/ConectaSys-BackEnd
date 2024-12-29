using Microsoft.EntityFrameworkCore;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeia a entidade para a tabela "Users" no esquema "public"
            modelBuilder.Entity<User>().ToTable("users");

            // Configura a propriedade ID
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .IsRequired();

            // Configura propriedades adicionais (opcional)
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
                .Property(u => u.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(255)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasColumnName("createdat")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<User>()
                .Property(u => u.UpdatedAt)
                .HasColumnName("updatedat")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }




    }
}
