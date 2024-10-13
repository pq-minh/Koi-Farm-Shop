using System;
using System.Collections.Generic;
using KoiFarmShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Infrastructure.Persistence
{

    public partial class KoiFarmShopContext : DbContext
    {

        //Scaffold-DbContext "Server=LAPTOP-SG66B6BP\SQLEXPRESS;Database=KoiShopV5;User Id=sa;Password=12345;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir../KoiFarmShop.Domain/Entities -Context KoiFarmShopContext

        public KoiFarmShopContext()
        {
        }

        public KoiFarmShopContext(DbContextOptions<KoiFarmShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressDetail> AddressDetails { get; set; }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

        public virtual DbSet<BatchKoi> BatchKois { get; set; }

        public virtual DbSet<BatchKoiCategory> BatchKoiCategories { get; set; }

        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<Koi> Kois { get; set; }

        public virtual DbSet<KoiCategory> KoiCategories { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Package> Packages { get; set; }

        public virtual DbSet<Post> Posts { get; set; }

        public virtual DbSet<Quotation> Quotations { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=LAPTOP-SG66B6BP\\SQLEXPRESS;Database=KoiShopV5;User Id=sa;Password=12345;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressDetail>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PK__AddressD__091C2A1BC4C35DB2");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.City).HasMaxLength(200);
                entity.Property(e => e.Dictrict).HasMaxLength(200);
                entity.Property(e => e.StreetName).HasMaxLength(200);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User).WithMany(p => p.AddressDetails).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");
                            j.ToTable("AspNetUserRoles");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BatchKoi>(entity =>
            {
                entity.HasKey(e => e.BatchKoiId).HasName("PK__BatchKoi__29AF8367B4ED895F");

                entity.ToTable("BatchKoi");

                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.BatchTypeId).HasColumnName("BatchTypeID");
                entity.Property(e => e.Certificate).HasColumnType("text");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.Gender)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Image).HasColumnType("text");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Origin)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Personality)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Quantity)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Size)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Weight).HasColumnName("weight");

                entity.HasOne(d => d.BatchType).WithMany(p => p.BatchKois)
                    .HasForeignKey(d => d.BatchTypeId)
                    .HasConstraintName("FK__BatchKoi__BatchT__4D94879B");
            });

            modelBuilder.Entity<BatchKoiCategory>(entity =>
            {
                entity.HasKey(e => e.BatchTypeId).HasName("PK__BatchKoi__752A87CE3D96F085");

                entity.ToTable("BatchKoiCategory");

                entity.Property(e => e.BatchTypeId).HasColumnName("BatchTypeID");
                entity.Property(e => e.TypeBatch)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.CartItemsId);

                entity.Property(e => e.CartItemsId).HasColumnName("CartItemsID");
                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.CartItems).HasForeignKey(d => d.BatchKoiId);

                entity.HasOne(d => d.Koi).WithMany(p => p.CartItems).HasForeignKey(d => d.KoiId);

                entity.HasOne(d => d.ShoppingCart).WithMany(p => p.CartItems).HasForeignKey(d => d.ShoppingCartId);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6B0C0E919");

                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.DiscountRate).HasColumnName("Discount_rate");
                entity.Property(e => e.EndDate).HasColumnName("end_Date");
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.StartDate).HasColumnName("start_Date");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Koi>(entity =>
            {
                entity.HasKey(e => e.KoiId).HasName("PK__Koi__E03435B83F39E659");

                entity.ToTable("Koi");

                entity.Property(e => e.KoiId).HasColumnName("KoiID");
                entity.Property(e => e.Certificate).HasColumnType("text");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.FishTypeId).HasColumnName("FishTypeID");
                entity.Property(e => e.Gender)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Image).HasColumnType("text");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Origin)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Personality)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.FishType).WithMany(p => p.Kois)
                    .HasForeignKey(d => d.FishTypeId)
                    .HasConstraintName("FK__Koi__FishTypeID__4CA06362");
            });

            modelBuilder.Entity<KoiCategory>(entity =>
            {
                entity.HasKey(e => e.FishTypeId).HasName("PK__KoiCateg__3D3EB8EE1589D08A");

                entity.ToTable("KoiCategory");

                entity.Property(e => e.FishTypeId).HasColumnName("FishTypeID");
                entity.Property(e => e.TypeFish)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF06FAB913");

                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
                //entity.Property(e => e.PaymentMethod)
                //    .HasMaxLength(200)
                //    .IsUnicode(false);
                //entity.Property(e => e.PaymentStatus)
                //    .HasMaxLength(200)
                //    .IsUnicode(false);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Discount).WithMany(p => p.Orders).HasForeignKey(d => d.DiscountId);

                entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId).HasName("PK__OrderDet__9DD74D9DC61284A3");

                entity.Property(e => e.OrderDetailsId).HasColumnName("OrderDetailsID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.BatchKoiId)
                    .HasConstraintName("FK__OrderDeta__Batch__4E88ABD4");

                entity.HasOne(d => d.Koi).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.KoiId)
                    .HasConstraintName("FK__OrderDeta__KoiID__4F7CD00D");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__4BAC3F29");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.PackageId).HasName("PK__Package__322035EC7EC9D4C4");

                entity.ToTable("Package");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.Packages)
                    .HasForeignKey(d => d.BatchKoiId)
                    .HasConstraintName("FK__Package__BatchKo__5441852A");

                entity.HasOne(d => d.Koi).WithMany(p => p.Packages)
                    .HasForeignKey(d => d.KoiId)
                    .HasConstraintName("FK__Package__KoiID__534D60F1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId).HasColumnName("PostID");
                entity.Property(e => e.Content).HasColumnType("text");
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User).WithMany(p => p.Posts).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.HasKey(e => e.QuotationId).HasName("PK__Quotatio__E19752B367481ECA");

                entity.ToTable("Quotation");

                entity.Property(e => e.QuotationId).HasColumnName("QuotationID");
                entity.Property(e => e.RequestId).HasColumnName("RequestID");
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Request).WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__Quotation__Reque__52593CB8");

                entity.HasOne(d => d.User).WithMany(p => p.Quotations).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PK__Request__33A8519A4AC926BF");

                entity.ToTable("Request");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");
                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.RelationalRequestId).HasColumnName("RelationalRequestID");
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.TypeRequest)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Package).WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__Request__Package__5535A963");

                entity.HasOne(d => d.User).WithMany(p => p.Requests).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79AE76B23DCF");

                entity.ToTable("Review");

                entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.Comments).HasColumnType("text");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BatchKoiId)
                    .HasConstraintName("FK__Review__BatchKoi__5165187F");

                entity.HasOne(d => d.Koi).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.KoiId)
                    .HasConstraintName("FK__Review__KoiID__5070F446");

                entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.Property(e => e.ShoppingCartId).HasColumnName("ShoppingCartID");
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts).HasForeignKey(d => d.UserId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}