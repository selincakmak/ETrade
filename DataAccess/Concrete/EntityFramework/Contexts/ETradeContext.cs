using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Entities.Concrete
{
    public partial class ETradeContext : DbContext
    {
        public ETradeContext()
        {
        }

        public ETradeContext(DbContextOptions<ETradeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Attribute> Attributes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<OperationClaim> OperationClaims { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserOperationClaim> UserOperationClaims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-0KH31F8;Initial Catalog=ETrade;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Attribute>(entity =>
            {
                entity.HasKey(e => e.ProductAttributeId);

                entity.ToTable("Attribute");

                entity.Property(e => e.Attribute1)
                    .HasMaxLength(50)
                    .HasColumnName("Attribute");

                entity.Property(e => e.Value).HasMaxLength(50);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Attributes)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Attributes_Products");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ImageName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Images_Products");
            });

            modelBuilder.Entity<OperationClaim>(entity =>
            {
                entity.ToTable("OperationClaim");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Adres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AdresTanimi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sehir)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Semt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("money");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.ToTable("OrderLine");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderLine_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderLines)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_OrderLine_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.DateAdded).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.HtmlContent).HasMaxLength(500);

                entity.Property(e => e.Image).IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_Categories");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductCategories_Products");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).HasMaxLength(500);

                entity.Property(e => e.PasswordSalt).HasMaxLength(500);
            });

            modelBuilder.Entity<UserOperationClaim>(entity =>
            {
                entity.ToTable("UserOperationClaim");

                entity.HasOne(d => d.OperationClaim)
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.OperationClaimId)
                    .HasConstraintName("FK_UserOperationClaims_OperationClaims");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOperationClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserOperationClaims_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
