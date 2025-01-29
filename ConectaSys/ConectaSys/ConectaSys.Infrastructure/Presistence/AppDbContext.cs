using Microsoft.EntityFrameworkCore;
using ConectaSys.ConectaSys.Domain.Entities.Users;
using ConectaSys.ConectaSys.Domain.Entities;

namespace ConectaSys.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LogEntry> Logs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Configuração para a tabela de logs
            modelBuilder.Entity<LogEntry>()
                .ToTable("logs");

            // 🔹 Configuração da tabela Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product"); // Nome correto da tabela

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
             .HasColumnName("Id")
             .HasColumnType("uniqueidentifier") // ✅ Certifique-se de que está correto
             .ValueGeneratedOnAdd(); //

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasColumnType("varchar(255)")
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasColumnName("Description")
                    .HasColumnType("text");

                entity.Property(e => e.SupplierId)
                  .HasColumnName("SupplierId")
                  .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Price)
                    .HasColumnName("Price")
                    .HasColumnType("decimal(10,2)")
                    .IsRequired();

                entity.Property(e => e.StockQuantity)
                    .HasColumnName("StockQuantity")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.ProductCode)
                    .HasColumnName("ProductCode")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Category)
                    .HasColumnName("Category")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Brand)
                    .HasColumnName("Brand")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Weight)
                    .HasColumnName("Weight")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.Dimensions)
                    .HasColumnName("Dimensions")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("ImageUrl")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("CreatedAt")
                    .HasColumnType("datetime2");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("UpdatedAt")
                    .HasColumnType("datetime2") 
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Status)
                    .HasColumnName("Status")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValue("active");

                entity.Property(e => e.Cost)
                    .HasColumnName("Cost")
                    .HasColumnType("decimal(10,2)");

                entity.Property(e => e.ProfitMargin)
                    .HasColumnName("ProfitMargin")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.Discount)
                    .HasColumnName("Discount")
                    .HasColumnType("decimal(5,2)");

                entity.Property(e => e.UnitOfMeasure)
                    .HasColumnName("UnitOfMeasure")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Tags)
                    .HasColumnName("Tags")
                    .HasColumnType("varchar(MAX)"); 
            });
        }
    }
}
