using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

public partial class AddedAuditEventstoBankAccount : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "BankAccount_AuditEvents",
            schema: "HaSpMan",
            columns: table => new
            {
                BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                PerformedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BankAccount_AuditEvents", x => new { x.BankAccountId, x.Id });
                table.ForeignKey(
                    name: "FK_BankAccount_AuditEvents_BankAccounts_BankAccountId",
                    column: x => x.BankAccountId,
                    principalSchema: "HaSpMan",
                    principalTable: "BankAccounts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "BankAccount_AuditEvents",
            schema: "HaSpMan");
    }
}
