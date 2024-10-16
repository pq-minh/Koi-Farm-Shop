using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class statusAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AddressDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AddressDetails");
        }
    }
}
