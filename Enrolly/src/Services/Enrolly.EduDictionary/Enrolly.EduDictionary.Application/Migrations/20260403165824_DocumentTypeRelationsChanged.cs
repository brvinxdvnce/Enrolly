using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.EduDictionary.Application.Migrations
{
    /// <inheritdoc />
    public partial class DocumentTypeRelationsChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_type_education_level_NextEducationLevelId",
                table: "document_type");

            migrationBuilder.DropIndex(
                name: "IX_document_type_NextEducationLevelId",
                table: "document_type");

            migrationBuilder.DropColumn(
                name: "NextEducationLevelId",
                table: "document_type");

            migrationBuilder.CreateTable(
                name: "document_type_next_edu_level",
                columns: table => new
                {
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    NextEducationLevelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type_next_edu_level", x => new { x.DocumentTypeId, x.NextEducationLevelsId });
                    table.ForeignKey(
                        name: "FK_document_type_next_edu_level_document_type_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "document_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_document_type_next_edu_level_education_level_NextEducationL~",
                        column: x => x.NextEducationLevelsId,
                        principalTable: "education_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_type_next_edu_level_NextEducationLevelsId",
                table: "document_type_next_edu_level",
                column: "NextEducationLevelsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_type_next_edu_level");

            migrationBuilder.AddColumn<int>(
                name: "NextEducationLevelId",
                table: "document_type",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_document_type_NextEducationLevelId",
                table: "document_type",
                column: "NextEducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_document_type_education_level_NextEducationLevelId",
                table: "document_type",
                column: "NextEducationLevelId",
                principalTable: "education_level",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
