using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexagonalArchitecture.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressFieldsToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CITY_NAME",
                table: "CUSTOMERS",
                type: "varchar(200)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "COUNTRY_NAME",
                table: "CUSTOMERS",
                type: "varchar(200)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "POSTAL_CODE",
                table: "CUSTOMERS",
                type: "varchar(20)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "STREET_NAME",
                table: "CUSTOMERS",
                type: "varchar(200)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "STREET_NUMBER",
                table: "CUSTOMERS",
                type: "varchar(20)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CITY_NAME",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "COUNTRY_NAME",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "POSTAL_CODE",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "STREET_NAME",
                table: "CUSTOMERS");

            migrationBuilder.DropColumn(
                name: "STREET_NUMBER",
                table: "CUSTOMERS");
        }
    }
}
