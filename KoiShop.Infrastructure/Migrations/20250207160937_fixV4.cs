using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixV4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Koi",
                newName: "KoiId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "BatchKoi",
                newName: "BatchKoiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KoiId",
                table: "Koi",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BatchKoiId",
                table: "BatchKoi",
                newName: "Id");

        }
    }
}
