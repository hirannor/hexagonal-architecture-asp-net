using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HexagonalArchitecture.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InsertInitialUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO CUSTOMERS (CUSTOMER_ID, USERNAME, EMAIL_ADDRESS, FIRST_NAME, LAST_NAME, BIRTH_ON)
                VALUES 
                    ('1', 'johndoe', 'john.doe@localhost.com', 'John', 'Doe', '1992-01-10'),
                    ('2', 'janedoe', 'jane.doe@localhost.com', 'Jane', 'Doe', '1995-05-15'),
                    ('3', 'michaeljackson', 'michael.jackson@localhost.com', 'Michael', 'Jackson', '1988-08-20'),
                    ('4', 'sarahconnor', 'sarah.connor@localhost.com', 'Sarah', 'Connor', '1990-11-12'),
                    ('5', 'willsmith', 'will.smith@localhost.com', 'Will', 'Smith', '1985-09-25'),
                    ('6', 'emilyblunt', 'emily.blunt@localhost.com', 'Emily', 'Blunt', '1988-02-23');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CUSTOMERS WHERE USERNAME IN ('johndoe', 'janedoe', 'michaeljackson', 'sarahconnor', 'willsmith', 'emilyblunt');");
        }
    }
}
