using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexagonalArchitecture.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CUSTOMER_ID = table.Column<string>(type: "varchar(200)", nullable: false),
                    USERNAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    EMAIL_ADDRESS = table.Column<string>(type: "varchar(200)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    LAST_NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    BIRTH_ON = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_EMAIL_ADDRESS",
                table: "CUSTOMERS",
                column: "EMAIL_ADDRESS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_CUSTOMER_ID",
                table: "CUSTOMERS",
                column: "CUSTOMER_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_USERNAME",
                table: "CUSTOMERS",
                column: "USERNAME",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMERS");
        }
    }
}
