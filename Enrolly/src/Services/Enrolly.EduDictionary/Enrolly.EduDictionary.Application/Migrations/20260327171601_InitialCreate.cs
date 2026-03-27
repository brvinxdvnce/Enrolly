using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Enrolly.EduDictionary.Application.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "education_level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RelevanceStatus = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_level", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "faculty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RelevanceStatus = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionName = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Received = table.Column<int>(type: "integer", nullable: false),
                    Added = table.Column<int>(type: "integer", nullable: false),
                    Removed = table.Column<int>(type: "integer", nullable: false),
                    Updated = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "document_type",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RelevanceStatus = table.Column<string>(type: "text", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: false),
                    NextEducationLevelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_document_type", x => x.Id);
                    table.ForeignKey(
                        name: "FK_document_type_education_level_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "education_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_document_type_education_level_NextEducationLevelId",
                        column: x => x.NextEducationLevelId,
                        principalTable: "education_level",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "program",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RelevanceStatus = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    EducationForm = table.Column<string>(type: "text", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_program", x => x.Id);
                    table.ForeignKey(
                        name: "FK_program_education_level_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "education_level",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_program_faculty_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "faculty",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_document_type_EducationLevelId",
                table: "document_type",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_document_type_NextEducationLevelId",
                table: "document_type",
                column: "NextEducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_program_EducationLevelId",
                table: "program",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_program_FacultyId",
                table: "program",
                column: "FacultyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "document_type");

            migrationBuilder.DropTable(
                name: "Imports");

            migrationBuilder.DropTable(
                name: "program");

            migrationBuilder.DropTable(
                name: "education_level");

            migrationBuilder.DropTable(
                name: "faculty");
        }
    }
}
