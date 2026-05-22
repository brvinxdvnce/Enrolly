using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Admissions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDocumentTypeTableAndChangedDocumentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_education_level_document_DocumentId",
                table: "education_level");

            migrationBuilder.DropTable(
                name: "document");

            migrationBuilder.RenameColumn(
                name: "DocumentId",
                table: "education_level",
                newName: "EducationDocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_education_level_DocumentId",
                table: "education_level",
                newName: "IX_education_level_EducationDocumentTypeId");

            migrationBuilder.CreateTable(
                name: "education_document",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_document", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_education_document_applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "applicant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "education_document_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: false),
                    NextEducationLevelIds = table.Column<int[]>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_document_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_education_document_type_education_level_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "education_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_education_document_ApplicantId",
                table: "education_document",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_education_document_type_EducationLevelId",
                table: "education_document_type",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_education_level_education_document_type_EducationDocumentTy~",
                table: "education_level",
                column: "EducationDocumentTypeId",
                principalTable: "education_document_type",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_education_level_education_document_type_EducationDocumentTy~",
                table: "education_level");

            migrationBuilder.DropTable(
                name: "education_document");

            migrationBuilder.DropTable(
                name: "education_document_type");

            migrationBuilder.RenameColumn(
                name: "EducationDocumentTypeId",
                table: "education_level",
                newName: "DocumentId");

            migrationBuilder.RenameIndex(
                name: "IX_education_level_EducationDocumentTypeId",
                table: "education_level",
                newName: "IX_education_level_DocumentId");

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: true),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_document_education_level_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "education_level",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_ApplicantId",
                table: "document",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_document_EducationLevelId",
                table: "document",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_education_level_document_DocumentId",
                table: "education_level",
                column: "DocumentId",
                principalTable: "document",
                principalColumn: "Id");
        }
    }
}
