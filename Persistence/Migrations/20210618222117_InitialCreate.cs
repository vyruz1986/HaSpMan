using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HaSpMan");

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "HaSpMan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address_Street = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Address_City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address_Country = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Address_ZipCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Address_HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members",
                schema: "HaSpMan");
        }
    }
}
