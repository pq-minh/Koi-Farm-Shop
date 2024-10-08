using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Point = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BatchKoiCategory",
                columns: table => new
                {
                    BatchTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeBatch = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BatchKoi__752A87CE3D96F085", x => x.BatchTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                columns: table => new
                {
                    DiscountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Discount_rate = table.Column<double>(type: "float", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    Used = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    start_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Discount__E43F6DF6B0C0E919", x => x.DiscountID);
                });

            migrationBuilder.CreateTable(
                name: "KoiCategory",
                columns: table => new
                {
                    FishTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeFish = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KoiCateg__3D3EB8EE1589D08A", x => x.FishTypeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressDetails",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Dictrict = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StreetName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AddressD__091C2A1BC4C35DB2", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_AddressDetails_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    UpdateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypePost = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ShoppingCartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ShoppingCartID);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BatchKoi",
                columns: table => new
                {
                    BatchKoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Quantity = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true),
                    Size = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Origin = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Gender = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Personality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Certificate = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    BatchTypeID = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BatchKoi__29AF8367B4ED895F", x => x.BatchKoiID);
                    table.ForeignKey(
                        name: "FK__BatchKoi__BatchT__4D94879B",
                        column: x => x.BatchTypeID,
                        principalTable: "BatchKoiCategory",
                        principalColumn: "BatchTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<double>(type: "float", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountID = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    PaymentStatus = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__C3905BAF06FAB913", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Order_Discount_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discount",
                        principalColumn: "DiscountID");
                });

            migrationBuilder.CreateTable(
                name: "Koi",
                columns: table => new
                {
                    KoiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FishTypeID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Origin = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Gender = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: true),
                    Personality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Certificate = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Koi__E03435B83F39E659", x => x.KoiID);
                    table.ForeignKey(
                        name: "FK__Koi__FishTypeID__4CA06362",
                        column: x => x.FishTypeID,
                        principalTable: "KoiCategory",
                        principalColumn: "FishTypeID");
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<float>(type: "real", nullable: true),
                    TotalPrice = table.Column<float>(type: "real", nullable: true),
                    Status = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    KoiId = table.Column<int>(type: "int", nullable: true),
                    BatchKoiId = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemsID);
                    table.ForeignKey(
                        name: "FK_CartItems_BatchKoi_BatchKoiId",
                        column: x => x.BatchKoiId,
                        principalTable: "BatchKoi",
                        principalColumn: "BatchKoiID");
                    table.ForeignKey(
                        name: "FK_CartItems_Koi_KoiId",
                        column: x => x.KoiId,
                        principalTable: "Koi",
                        principalColumn: "KoiID");
                    table.ForeignKey(
                        name: "FK_CartItems_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ShoppingCartID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    KoiID = table.Column<int>(type: "int", nullable: true),
                    BatchKoiID = table.Column<int>(type: "int", nullable: true),
                    ToTalQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__9DD74D9DC61284A3", x => x.OrderDetailsID);
                    table.ForeignKey(
                        name: "FK__OrderDeta__Batch__4E88ABD4",
                        column: x => x.BatchKoiID,
                        principalTable: "BatchKoi",
                        principalColumn: "BatchKoiID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__KoiID__4F7CD00D",
                        column: x => x.KoiID,
                        principalTable: "Koi",
                        principalColumn: "KoiID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__Order__4BAC3F29",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    PackageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KoiID = table.Column<int>(type: "int", nullable: true),
                    BatchKoiID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Package__322035EC7EC9D4C4", x => x.PackageID);
                    table.ForeignKey(
                        name: "FK__Package__BatchKo__5441852A",
                        column: x => x.BatchKoiID,
                        principalTable: "BatchKoi",
                        principalColumn: "BatchKoiID");
                    table.ForeignKey(
                        name: "FK__Package__KoiID__534D60F1",
                        column: x => x.KoiID,
                        principalTable: "Koi",
                        principalColumn: "KoiID");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "text", nullable: true),
                    KoiID = table.Column<int>(type: "int", nullable: true),
                    BatchKoiID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Review__74BC79AE76B23DCF", x => x.ReviewID);
                    table.ForeignKey(
                        name: "FK_Review_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Review__BatchKoi__5165187F",
                        column: x => x.BatchKoiID,
                        principalTable: "BatchKoi",
                        principalColumn: "BatchKoiID");
                    table.ForeignKey(
                        name: "FK__Review__KoiID__5070F446",
                        column: x => x.KoiID,
                        principalTable: "Koi",
                        principalColumn: "KoiID");
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PackageID = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: true),
                    RelationalRequestID = table.Column<int>(type: "int", nullable: true),
                    ConsignmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AgreementPrice = table.Column<double>(type: "float", nullable: true),
                    TypeRequest = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Request__33A8519A4AC926BF", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Request_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Request__Package__5535A963",
                        column: x => x.PackageID,
                        principalTable: "Package",
                        principalColumn: "PackageID");
                });

            migrationBuilder.CreateTable(
                name: "Quotation",
                columns: table => new
                {
                    QuotationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quotatio__E19752B367481ECA", x => x.QuotationID);
                    table.ForeignKey(
                        name: "FK_Quotation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Quotation__Reque__52593CB8",
                        column: x => x.RequestID,
                        principalTable: "Request",
                        principalColumn: "RequestID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressDetails_UserId",
                table: "AddressDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BatchKoi_BatchTypeID",
                table: "BatchKoi",
                column: "BatchTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_BatchKoiId",
                table: "CartItems",
                column: "BatchKoiId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_KoiId",
                table: "CartItems",
                column: "KoiId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ShoppingCartId",
                table: "CartItems",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Koi_FishTypeID",
                table: "Koi",
                column: "FishTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DiscountID",
                table: "Order",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_BatchKoiID",
                table: "OrderDetails",
                column: "BatchKoiID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_KoiID",
                table: "OrderDetails",
                column: "KoiID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_BatchKoiID",
                table: "Package",
                column: "BatchKoiID");

            migrationBuilder.CreateIndex(
                name: "IX_Package_KoiID",
                table: "Package",
                column: "KoiID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_RequestID",
                table: "Quotation",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_Quotation_UserId",
                table: "Quotation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Request_PackageID",
                table: "Request",
                column: "PackageID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserId",
                table: "Request",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_BatchKoiID",
                table: "Review",
                column: "BatchKoiID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_KoiID",
                table: "Review",
                column: "KoiID");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Quotation");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Discount");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "BatchKoi");

            migrationBuilder.DropTable(
                name: "Koi");

            migrationBuilder.DropTable(
                name: "BatchKoiCategory");

            migrationBuilder.DropTable(
                name: "KoiCategory");
        }
    }
}
