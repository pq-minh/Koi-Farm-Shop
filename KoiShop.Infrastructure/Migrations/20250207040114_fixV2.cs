using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
        name: "FK__Koi__FishTypeID__4CA06362",
        table: "Koi");

            // Xóa Foreign Key trong BatchKoi
            migrationBuilder.DropForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",  // Thay XXXXX bằng tên FK chính xác
                table: "BatchKoi");

            migrationBuilder.DropPrimaryKey(
                name: "PK__BatchKoi__752A87CE3D96F085",
                table: "FishCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FishCategory",
                table: "FishCategory",
                column: "FishTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FishCategory",
                table: "FishCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK__BatchKoi__752A87CE3D96F085",
                table: "FishCategory",
                column: "FishTypeID");
        }
    }
}
