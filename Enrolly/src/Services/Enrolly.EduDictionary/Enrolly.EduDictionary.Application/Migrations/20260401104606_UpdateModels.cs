using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.EduDictionary.Application.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Imports",
                table: "Imports");

            migrationBuilder.RenameTable(
                name: "Imports",
                newName: "import_summary");

            migrationBuilder.AddPrimaryKey(
                name: "PK_import_summary",
                table: "import_summary",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_import_summary",
                table: "import_summary");

            migrationBuilder.RenameTable(
                name: "import_summary",
                newName: "Imports");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Imports",
                table: "Imports",
                column: "Id");
        }
    }
}
