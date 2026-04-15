using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedManagerGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Managers");
        }
    }
}
