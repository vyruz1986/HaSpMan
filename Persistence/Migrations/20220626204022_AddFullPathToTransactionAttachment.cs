using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations;

public partial class AddFullPathToTransactionAttachment : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "BlobURI",
            schema: "HaSpMan",
            table: "Transaction_Attachments",
            newName: "FullPath");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "FullPath",
            schema: "HaSpMan",
            table: "Transaction_Attachments",
            newName: "BlobURI");
    }
}
