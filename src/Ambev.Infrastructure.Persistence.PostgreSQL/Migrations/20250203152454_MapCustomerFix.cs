using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.Infrastructure.Persistence.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class MapCustomerFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sales",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "UserFirstName",
                table: "Sales",
                newName: "CustomerLastName");

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "Sales",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "CustomerLastName",
                table: "Sales",
                newName: "UserFirstName");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sales",
                newName: "UserId");
        }
    }
}
