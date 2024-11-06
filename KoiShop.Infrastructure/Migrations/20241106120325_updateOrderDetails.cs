using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CustomerFunds",
                table: "OrderDetails",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ShopRevenue",
                table: "OrderDetails",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerFunds",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ShopRevenue",
                table: "OrderDetails");
        }
    }
}
