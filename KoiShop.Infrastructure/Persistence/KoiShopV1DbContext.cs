using KoiShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KoiShop.Infrastructure.Persistence
{
    public class KoiShopV1DbContext(DbContextOptions<KoiShopV1DbContext> options)
        : IdentityDbContext<User>(options)
    {
        public virtual DbSet<AddressDetail> AddressDetails { get; set; }

        public virtual DbSet<BatchKoi> BatchKois { get; set; }

        public virtual DbSet<BatchKoiCategory> BatchKoiCategories { get; set; }

        public virtual DbSet<Discount> Discounts { get; set; }

        public virtual DbSet<Koi> Kois { get; set; }

        public virtual DbSet<KoiCategory> KoiCategories { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Package> Packages { get; set; }

        public virtual DbSet<Quotation> Quotations { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AddressDetail>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PK__AddressD__091C2A1BC4C35DB2");

                entity.Property(e => e.AddressId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AddressID");
                entity.Property(e => e.City)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Dictrict)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.StreetName)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Ward)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.AddressDetails)
                     .HasForeignKey(ad => ad.UserId);
                entity.Property(e => e.Status).HasColumnName("Status").IsUnicode(true);

            });
            modelBuilder.Entity<ShoppingCart>(entity =>
               {
                   entity.HasKey(e => e.ShoppingCartID);
                   entity.Property(e => e.ShoppingCartID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShoppingCartID");
                   entity.Property(e => e.CreateDate).HasColumnName("CreateDate");

                   entity.HasOne(ad => ad.User)
                      .WithMany(u => u.ShoppingCarts)
                     .HasForeignKey(ad => ad.UserId);
               }
            );
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => e.CartItemsID);
                entity.Property(e => e.CartItemsID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CartItemsID");
                entity.Property(e => e.UnitPrice).HasColumnName("UnitPrice");
                entity.Property(e => e.TotalPrice).HasColumnName("TotalPrice");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
                entity.Property(e => e.Status).HasMaxLength(200).IsUnicode(false);

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.BatchKoiId);

                entity.HasOne(d => d.Koi).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.KoiId);

                entity.HasOne(d => d.ShoppingCart).WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ShoppingCartId);
            }

            );
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentID);
                entity.Property(e => e.PaymentID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PaymentID");
                entity.Property(e => e.CreateDate).HasColumnName("CreateDate");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.PaymenMethod).HasColumnName("PaymentMethod");
                entity.Property(e => e.Status).HasColumnName("Status").IsUnicode(true);

                entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.OrderId);
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostID);
                entity.Property(e => e.PostID)
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PostID");
                entity.Property(e => e.Title).HasColumnName("Title");

                entity.Property(e => e.Content).HasColumnName("Content").IsUnicode(true);

                entity.Property(e => e.CreateDate).HasColumnName("CreateDate");
                entity.Property(e => e.UpdateDate).HasColumnName("UpdateDate");

                entity.Property(e => e.Status).HasColumnName("Status");

                entity.Property(e => e.TypePost).HasColumnName("TypePost");
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.Posts)
                     .HasForeignKey(ad => ad.UserId);

            });
            modelBuilder.Entity<BatchKoi>(entity =>
            {
                entity.HasKey(e => e.BatchKoiId).HasName("PK__BatchKoi__29AF8367B4ED895F");

                entity.ToTable("BatchKoi");

                entity.Property(e => e.BatchKoiId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BatchKoiID");
                entity.Property(e => e.BatchTypeId).HasColumnName("BatchTypeID");
                entity.Property(e => e.Certificate).HasColumnType("text");
                entity.Property(e => e.Description).HasColumnName("Description").IsUnicode(true);
                entity.Property(e => e.Gender)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Image).HasColumnType("text");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Origin)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Age)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Quantity)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Size)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Weight).HasColumnName("weight").IsUnicode(true);

                entity.HasOne(d => d.BatchType).WithMany(p => p.BatchKois)
                    .HasForeignKey(d => d.BatchTypeId)
                    .HasConstraintName("FK__BatchKoi__BatchT__4D94879B");
                entity.HasOne(d => d.User).WithMany(p => p.BatchKois)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BatchKoiCategory>(entity =>
            {
                entity.HasKey(e => e.BatchTypeId).HasName("PK__BatchKoi__752A87CE3D96F085");

                entity.ToTable("BatchKoiCategory");

                entity.Property(e => e.BatchTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BatchTypeID");
                entity.Property(e => e.TypeBatch)
                    .HasMaxLength(200)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6B0C0E919");

                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DiscountID");
                entity.Property(e => e.Description).HasColumnType("text");
                entity.Property(e => e.DiscountRate).HasColumnName("Discount_rate");
                entity.Property(e => e.EndDate).HasColumnName("end_Date");
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.StartDate).HasColumnName("start_Date");
                entity.Property(e => e.Status).HasColumnName("status").IsUnicode(true);
            });

            modelBuilder.Entity<Koi>(entity =>
            {
                entity.HasKey(e => e.KoiId).HasName("PK__Koi__E03435B83F39E659");

                entity.ToTable("Koi");

                entity.Property(e => e.KoiId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("KoiID");
                entity.Property(e => e.Certificate).HasColumnType("text");
                entity.Property(e => e.Description).HasColumnName("Description").IsUnicode(true);
                entity.Property(e => e.FishTypeId).HasColumnName("FishTypeID");
                entity.Property(e => e.Gender)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Image).HasColumnType("text");
                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Origin)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.Personality)
                    .HasMaxLength(200)
                    .IsUnicode(true);
                entity.Property(e => e.Status)
                    .HasMaxLength(200)
                    .IsUnicode(true);

                entity.HasOne(d => d.FishType).WithMany(p => p.Kois)
                    .HasForeignKey(d => d.FishTypeId)
                    .HasConstraintName("FK__Koi__FishTypeID__4CA06362");
                entity.HasOne(d => d.User).WithMany(p => p.Kois)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<KoiCategory>(entity =>
            {
                entity.HasKey(e => e.FishTypeId).HasName("PK__KoiCateg__3D3EB8EE1589D08A");

                entity.ToTable("KoiCategory");

                entity.Property(e => e.FishTypeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("FishTypeID");
                entity.Property(e => e.TypeFish)
                    .HasMaxLength(200)
                    .IsUnicode(true);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BAF06FAB913");

                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderID");
                entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
                entity.Property(e => e.TotalAmount).HasColumnName("TotalAmount");
                entity.Property(e => e.ShippingAddress).HasColumnName("ShippingAddress").IsUnicode(true);
                entity.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");
                entity.Property(e => e.OrderStatus).HasColumnName("OrderStatus").IsUnicode(true);
                entity.Property(e => e.CreateDate)
                    .HasColumnName("CreateDate");
                
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.Orders)
                     .HasForeignKey(ad => ad.UserId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailsId).HasName("PK__OrderDet__9DD74D9DC61284A3");

                entity.Property(e => e.OrderDetailsId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("OrderDetailsID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");
                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.ShopRevenue).HasColumnName("ShopRevenue");
                entity.Property(e => e.CustomerFunds).HasColumnName("CustomerFunds");
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

                entity.Property(e => e.PackageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PackageID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");

                entity.HasOne(d => d.BatchKoi).WithMany(p => p.Packages)
                    .HasForeignKey(d => d.BatchKoiId)
                    .HasConstraintName("FK__Package__BatchKo__5441852A");

                entity.HasOne(d => d.Koi).WithMany(p => p.Packages)
                    .HasForeignKey(d => d.KoiId)
                    .HasConstraintName("FK__Package__KoiID__534D60F1");
            });

            modelBuilder.Entity<Quotation>(entity =>
            {
                entity.HasKey(e => e.QuotationId).HasName("PK__Quotatio__E19752B367481ECA");

                entity.ToTable("Quotation");

                entity.Property(e => e.QuotationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("QuotationID");
                entity.Property(e => e.RequestId).HasColumnName("RequestID");
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.Note).HasColumnName("Note").IsUnicode(true);

                entity.HasOne(d => d.Request).WithMany(p => p.Quotations)
                    .HasForeignKey(d => d.RequestId)
                    .HasConstraintName("FK__Quotation__Reque__52593CB8");
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.Quotations)
                     .HasForeignKey(ad => ad.UserId);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.HasKey(e => e.RequestId).HasName("PK__Request__33A8519A4AC926BF");

                entity.ToTable("Request");

                entity.Property(e => e.RequestId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RequestID");
                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.RelationalRequestId).HasColumnName("RelationalRequestID");
                entity.Property(e => e.Status).HasMaxLength(255);
                entity.Property(e => e.TypeRequest)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Package).WithMany(p => p.Requests)
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__Request__Package__5535A963");
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.Requests)
                     .HasForeignKey(ad => ad.UserId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasKey(e => e.ReviewId).HasName("PK__Review__74BC79AE76B23DCF");

                entity.ToTable("Review");

                entity.Property(e => e.ReviewId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ReviewID");
                entity.Property(e => e.BatchKoiId).HasColumnName("BatchKoiID");
                entity.Property(e => e.Comments).HasColumnType("text");
                entity.Property(e => e.KoiId).HasColumnName("KoiID");
                entity.Property(e => e.Status).HasColumnName("Status");
                entity.HasOne(d => d.BatchKoi).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.BatchKoiId)
                    .HasConstraintName("FK__Review__BatchKoi__5165187F");

                entity.HasOne(d => d.Koi).WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.KoiId)
                    .HasConstraintName("FK__Review__KoiID__5070F446");
                entity.HasOne(ad => ad.User)
                      .WithMany(u => u.Reviews)
                     .HasForeignKey(ad => ad.UserId);
            });
        }

    }

}
