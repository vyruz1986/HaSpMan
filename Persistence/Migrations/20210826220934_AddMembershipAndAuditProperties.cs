using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations;

public partial class AddMembershipAndAuditProperties : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "MembershipExpiryDate",
            schema: "HaSpMan",
            table: "Members",
            type: "datetimeoffset",
            nullable: true);

        migrationBuilder.AddColumn<double>(
            name: "MembershipFee",
            schema: "HaSpMan",
            table: "Members",
            type: "float",
            nullable: false,
            defaultValue: 0.0);

        migrationBuilder.CreateTable(
            name: "Member_AuditEvents",
            schema: "HaSpMan",
            columns: table => new
            {
                MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                PerformedBy = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Member_AuditEvents", x => new { x.MemberId, x.Id });
                table.ForeignKey(
                    name: "FK_Member_AuditEvents_Members_MemberId",
                    column: x => x.MemberId,
                    principalSchema: "HaSpMan",
                    principalTable: "Members",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Member_AuditEvents",
            schema: "HaSpMan");

        migrationBuilder.DropColumn(
            name: "MembershipExpiryDate",
            schema: "HaSpMan",
            table: "Members");

        migrationBuilder.DropColumn(
            name: "MembershipFee",
            schema: "HaSpMan",
            table: "Members");
    }
}
