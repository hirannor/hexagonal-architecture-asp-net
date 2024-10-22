using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexagonalArchitecture.Migrations
{
    /// <inheritdoc />
    public partial class InsertInitialCustomerAuthentications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string hashedPassword = "AQAAAAIAAYagAAAAENYz+85CzlqtVIH4asTdbGVi+V+NbbYndJrLuwUpbYhHS8owx0PGhTk+THB9n41Mbw=="; 
            string securityStamp = Guid.NewGuid().ToString();

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnd", "LockoutEnabled", "AccessFailedCount" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid().ToString(), // User 1
                        "janedoe",
                        "JANEDOE",
                        "jane.doe@gmail.com",
                        "JANE.DOE@GMAIL.COM",
                        true,
                        hashedPassword,
                        securityStamp,
                        Guid.NewGuid().ToString(), // Unique value for concurrency stamp
                        null,
                        false,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        Guid.NewGuid().ToString(), // User 2
                        "michaeljackson",
                        "MICHAELJACKSON",
                        "michael.jackson@gmail.com",
                        "MICHAEL.JACKSON@GMAIL.COM",
                        true,
                        hashedPassword,
                        securityStamp,
                        Guid.NewGuid().ToString(),
                        null,
                        false,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        Guid.NewGuid().ToString(), // User 3
                        "sarahconnor",
                        "SARAHCONNOR",
                        "sarah.connor@gmail.com",
                        "SARAH.CONNOR@GMAIL.COM",
                        true,
                        hashedPassword,
                        securityStamp,
                        Guid.NewGuid().ToString(),
                        null,
                        false,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        Guid.NewGuid().ToString(), // User 4
                        "willsmith",
                        "WILLSMITH",
                        "will.smith@gmail.com",
                        "WILL.SMITH@GMAIL.COM",
                        true,
                        hashedPassword,
                        securityStamp,
                        Guid.NewGuid().ToString(),
                        null,
                        false,
                        false,
                        null,
                        true,
                        0
                    },
                    {
                        Guid.NewGuid().ToString(), // User 5
                        "emilyblunt",
                        "EMILYBLUNT",
                        "emily.blunt@gmail.com",
                        "EMILY.BLUNT@GMAIL.COM",
                        true,
                        hashedPassword,
                        securityStamp,
                        Guid.NewGuid().ToString(),
                        null,
                        false,
                        false,
                        null,
                        true,
                        0
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetUsers WHERE UserName IN ('janedoe', 'michaeljackson', 'sarahconnor', 'willsmith', 'emilyblunt');");
        }
    }
}
