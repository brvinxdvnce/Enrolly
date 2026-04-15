using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Documents.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    DepartmentCode = table.Column<string>(type: "text", nullable: false),
                    Registration = table.Column<string>(type: "text", nullable: false),
                    Series = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passports_Applicants_Id",
                        column: x => x.Id,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diplomas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Series = table.Column<string>(type: "text", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    IssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IssuedBy = table.Column<string>(type: "text", nullable: false),
                    Qualification = table.Column<string>(type: "text", nullable: true),
                    Speciality = table.Column<string>(type: "text", nullable: false),
                    AverageGrade = table.Column<double>(type: "double precision", nullable: true),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diplomas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diplomas_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diplomas_EducationDocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "EducationDocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalName = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    Extension = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    EducationDocumentId = table.Column<Guid>(type: "uuid", nullable: true),
                    PassportId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.Id);
                    table.ForeignKey(
                        name: "FK_file_Diplomas_EducationDocumentId",
                        column: x => x.EducationDocumentId,
                        principalTable: "Diplomas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_file_Passports_PassportId",
                        column: x => x.PassportId,
                        principalTable: "Passports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_ApplicantId",
                table: "Diplomas",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Diplomas_DocumentTypeId",
                table: "Diplomas",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_file_EducationDocumentId",
                table: "file",
                column: "EducationDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_file_PassportId",
                table: "file",
                column: "PassportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "file");

            migrationBuilder.DropTable(
                name: "Diplomas");

            migrationBuilder.DropTable(
                name: "Passports");

            migrationBuilder.DropTable(
                name: "EducationDocumentTypes");

            migrationBuilder.DropTable(
                name: "Applicants");
        }
    }
}
