using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Admissions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDictionaryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "manager",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "manager",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "manager");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "manager");
        }
    }
}
