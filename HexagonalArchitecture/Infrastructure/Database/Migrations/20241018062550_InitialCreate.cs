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
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<string>(type: "varchar(200)", nullable: false),
                    EMAIL_ADDRESS = table.Column<string>(type: "varchar(200)", nullable: false),
                    FULL_NAME = table.Column<string>(type: "varchar(200)", nullable: false),
                    AGE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USERS_EMAIL_ADDRESS",
                table: "USERS",
                column: "EMAIL_ADDRESS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_USERS_USER_ID",
                table: "USERS",
                column: "USER_ID",
                unique: true);
            
            // Insert demo data
            migrationBuilder.InsertData(
                table: "USERS",
                columns: ["ID", "EMAIL_ADDRESS", "FULL_NAME", "AGE", "USER_ID"],
                values: new object[,]
                {
                    { "1", "user1@example.com", "User One", 25, Guid.NewGuid().ToString() },
                    { "2", "user2@example.com", "User Two", 30, Guid.NewGuid().ToString() },
                    { "3", "user3@example.com", "User Three", 22, Guid.NewGuid().ToString() },
                    { "4", "user4@example.com", "User Four", 35, Guid.NewGuid().ToString() },
                    { "5", "user5@example.com", "User Five", 28, Guid.NewGuid().ToString() },
                    { "6", "user6@example.com", "User Six", 40, Guid.NewGuid().ToString() },
                    { "7", "user7@example.com", "User Seven", 33, Guid.NewGuid().ToString() },
                    { "8", "user8@example.com", "User Eight", 29, Guid.NewGuid().ToString() },
                    { "9", "user9@example.com", "User Nine", 26, Guid.NewGuid().ToString() },
                    { "10", "user10@example.com", "User Ten", 31, Guid.NewGuid().ToString() }
                });
            
            migrationBuilder.Sql("DBCC CHECKIDENT('USERS', RESEED, 11)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
