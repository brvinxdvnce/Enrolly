using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedAdmissionStatusColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActiveAdmission",
                table: "Applicants",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActiveAdmission",
                table: "Applicants");
        }
    }
}
