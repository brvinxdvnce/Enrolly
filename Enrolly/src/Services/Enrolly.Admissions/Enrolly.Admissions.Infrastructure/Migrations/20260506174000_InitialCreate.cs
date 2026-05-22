using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Admissions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "applicant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_applicant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "faculty",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faculty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "manager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: true),
                    Grade = table.Column<int>(type: "integer", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manager", x => x.Id);
                    table.ForeignKey(
                        name: "FK_manager_applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "applicant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "admission",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    AdmissionStatus = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_admission_applicant_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_admission_manager_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "manager",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "admission_program",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdmissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admission_program", x => new { x.ProgramId, x.AdmissionId });
                    table.ForeignKey(
                        name: "FK_admission_program_admission_AdmissionId",
                        column: x => x.AdmissionId,
                        principalTable: "admission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "document",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicantId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "education_level",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DocumentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_level", x => x.Id);
                    table.ForeignKey(
                        name: "FK_education_level_document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "document",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "program",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    EducationForm = table.Column<string>(type: "text", nullable: false),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: true),
                    EducationLevelId = table.Column<int>(type: "integer", nullable: true)
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
                name: "IX_admission_ApplicantId",
                table: "admission",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_admission_ManagerId",
                table: "admission",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_admission_program_AdmissionId",
                table: "admission_program",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_document_ApplicantId",
                table: "document",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_document_EducationLevelId",
                table: "document",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_education_level_DocumentId",
                table: "education_level",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_manager_ApplicantId",
                table: "manager",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_program_EducationLevelId",
                table: "program",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_program_FacultyId",
                table: "program",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_admission_program_program_ProgramId",
                table: "admission_program",
                column: "ProgramId",
                principalTable: "program",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_document_education_level_EducationLevelId",
                table: "document",
                column: "EducationLevelId",
                principalTable: "education_level",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_document_applicant_ApplicantId",
                table: "document");

            migrationBuilder.DropForeignKey(
                name: "FK_document_education_level_EducationLevelId",
                table: "document");

            migrationBuilder.DropTable(
                name: "admission_program");

            migrationBuilder.DropTable(
                name: "admission");

            migrationBuilder.DropTable(
                name: "program");

            migrationBuilder.DropTable(
                name: "manager");

            migrationBuilder.DropTable(
                name: "faculty");

            migrationBuilder.DropTable(
                name: "applicant");

            migrationBuilder.DropTable(
                name: "education_level");

            migrationBuilder.DropTable(
                name: "document");
        }
    }
}
