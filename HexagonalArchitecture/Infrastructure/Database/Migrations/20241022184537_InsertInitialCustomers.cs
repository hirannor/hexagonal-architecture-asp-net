using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace HexagonalArchitecture.Infrastructure.Database.Migrations;

public partial class InsertInitialUsers : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql($@"
                INSERT INTO CUSTOMERS (CUSTOMER_ID, USERNAME, EMAIL_ADDRESS, FIRST_NAME, LAST_NAME, BIRTH_ON)
                VALUES 
                    ('{Guid.NewGuid()}', 'johndoe', 'john.doe@localhost.com', 'John', 'Doe', '1992-01-10'),
                    ('{Guid.NewGuid()}', 'janedoe', 'jane.doe@localhost.com', 'Jane', 'Doe', '1995-05-15'),
                    ('{Guid.NewGuid()}', 'michaeljackson', 'michael.jackson@localhost.com', 'Michael', 'Jackson', '1988-08-20'),
                    ('{Guid.NewGuid()}', 'sarahconnor', 'sarah.connor@localhost.com', 'Sarah', 'Connor', '1990-11-12'),
                    ('{Guid.NewGuid()}', 'willsmith', 'will.smith@localhost.com', 'Will', 'Smith', '1985-09-25'),
                    ('{Guid.NewGuid()}', 'emilyblunt', 'emily.blunt@localhost.com', 'Emily', 'Blunt', '1988-02-23');");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM CUSTOMERS WHERE USERNAME IN ('johndoe', 'janedoe', 'michaeljackson', 'sarahconnor', 'willsmith', 'emilyblunt');");
    }
}