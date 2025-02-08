using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",
                table: "BatchKoi");

            migrationBuilder.DropForeignKey(
                name: "FK__Koi__FishTypeID__4CA06362",
                table: "Koi");

            migrationBuilder.DropTable(
                name: "BatchKoiCategory");

            migrationBuilder.DropTable(
                name: "KoiCategory");

            migrationBuilder.RenameColumn(
                name: "KoiID",
                table: "Koi",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BatchKoiID",
                table: "BatchKoi",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BatchTypeID",
                table: "BatchKoi",
                newName: "FishTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_BatchKoi_BatchTypeID",
                table: "BatchKoi",
                newName: "IX_BatchKoi_FishTypeID");

            migrationBuilder.CreateTable(
                name: "FishCategory",
                columns: table => new
                {
                    FishTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchTypeId = table.Column<int>(type: "int", nullable: false),
                    TypeFish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BatchKoi__752A87CE3D96F085", x => x.FishTypeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",
                table: "BatchKoi",
                column: "FishTypeID",
                principalTable: "FishCategory",
                principalColumn: "FishTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK__Koi__FishTypeID__4CA06362",
                table: "Koi",
                column: "FishTypeID",
                principalTable: "FishCategory",
                principalColumn: "FishTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",
                table: "BatchKoi");

            migrationBuilder.DropForeignKey(
                name: "FK__Koi__FishTypeID__4CA06362",
                table: "Koi");

            migrationBuilder.DropTable(
                name: "FishCategory");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Koi",
                newName: "KoiID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BatchKoi",
                newName: "BatchKoiID");

            migrationBuilder.RenameColumn(
                name: "FishTypeID",
                table: "BatchKoi",
                newName: "BatchTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_BatchKoi_FishTypeID",
                table: "BatchKoi",
                newName: "IX_BatchKoi_BatchTypeID");

            migrationBuilder.CreateTable(
                name: "BatchKoiCategory",
                columns: table => new
                {
                    BatchTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeBatch = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BatchKoi__752A87CE3D96F085", x => x.BatchTypeID);
                });

            migrationBuilder.CreateTable(
                name: "KoiCategory",
                columns: table => new
                {
                    FishTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeFish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KoiCateg__3D3EB8EE1589D08A", x => x.FishTypeID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",
                table: "BatchKoi",
                column: "BatchTypeID",
                principalTable: "BatchKoiCategory",
                principalColumn: "BatchTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK__Koi__FishTypeID__4CA06362",
                table: "Koi",
                column: "FishTypeID",
                principalTable: "KoiCategory",
                principalColumn: "FishTypeID");
        }
    }
}
