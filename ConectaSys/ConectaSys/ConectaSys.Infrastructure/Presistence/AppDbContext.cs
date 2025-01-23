using Microsoft.EntityFrameworkCore;
using ConectaSys.ConectaSys.Domain.Entities.Users;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets
        public DbSet<LogEntry> Logs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Chamada ao método base
            base.OnModelCreating(modelBuilder);

            // Configuração da entidade LogEntry
            modelBuilder.Entity<LogEntry>()
                .ToTable("logs");

            // Configuração da entidade User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("uuid")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdat")
                    .HasColumnType("timestamptz")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdateAt)
                    .HasColumnName("updatedat")
                    .HasColumnType("timestamptz")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasColumnType("boolean")
                    .HasDefaultValue(true);
            });

            // Configuração da entidade Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("product");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("serial");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(10, 2)")
                    .IsRequired();

                entity.Property(e => e.StockQuantity)
                    .HasColumnName("stock_quantity")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.ProductCode)
                    .HasColumnName("product_code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Brand)
                    .HasColumnName("brand")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Weight)
                    .HasColumnName("weight")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.Dimensions)
                    .HasColumnName("dimensions")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("image_url")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamptz")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamptz")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValue("active");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ProfitMargin)
                    .HasColumnName("profit_margin")
                    .HasColumnType("numeric(5, 2)");

                entity.Property(e => e.Discount)
                    .HasColumnName("discount")
                    .HasColumnType("numeric(5, 2)");
            });
        }
    }
}
