using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteBulb.OatShop.Infrastructure.Migrations.Migrations
{
    public partial class RenameUpdatedToLastModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Product",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "OrderItem",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Order",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Customer",
                newName: "LastModified");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Product",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "OrderItem",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Order",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Customer",
                newName: "Updated");
        }
    }
}
