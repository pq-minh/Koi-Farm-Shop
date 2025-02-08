using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK__Koi__FishTypeID__4CA06362",
                table: "Koi",
                column: "FishTypeId",
                principalTable: "FishCategory",
                principalColumn: "FishTypeId"); // Cách xử lý khi xóa

            // Thêm lại Foreign Key trong BatchKoi
            migrationBuilder.AddForeignKey(
                name: "FK__BatchKoi__BatchT__4D94879B",
                table: "BatchKoi",
                column: "FishTypeId",
                principalTable: "FishCategory",
                principalColumn: "FishTypeId"); // Cách xử lý khi xóa

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Koi_FishCategory_FishTypeId",
                table: "Koi");

            migrationBuilder.DropForeignKey(
                name: "FK_BatchKoi_FishCategory_FishTypeId",
                table: "BatchKoi");
        }
    }
}
