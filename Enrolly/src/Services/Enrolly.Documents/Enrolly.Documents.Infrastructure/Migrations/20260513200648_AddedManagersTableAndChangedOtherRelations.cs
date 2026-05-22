using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Enrolly.Documents.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedManagersTableAndChangedOtherRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_Applicants_ApplicantId",
                table: "Diplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_EducationDocumentTypes_DocumentTypeId",
                table: "Diplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_file_Diplomas_EducationDocumentId",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_file_Passports_PassportId",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_Passports_Applicants_Id",
                table: "Passports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Passports",
                table: "Passports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EducationDocumentTypes",
                table: "EducationDocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants");

            migrationBuilder.RenameTable(
                name: "Passports",
                newName: "passport_meta");

            migrationBuilder.RenameTable(
                name: "EducationDocumentTypes",
                newName: "education_document_type");

            migrationBuilder.RenameTable(
                name: "Diplomas",
                newName: "education_document_meta");

            migrationBuilder.RenameTable(
                name: "Applicants",
                newName: "applicant");

            migrationBuilder.RenameIndex(
                name: "IX_Diplomas_DocumentTypeId",
                table: "education_document_meta",
                newName: "IX_education_document_meta_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Diplomas_ApplicantId",
                table: "education_document_meta",
                newName: "IX_education_document_meta_ApplicantId");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmissionActive",
                table: "applicant",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "applicant",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_passport_meta",
                table: "passport_meta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_education_document_type",
                table: "education_document_type",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_education_document_meta",
                table: "education_document_meta",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_applicant",
                table: "applicant",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "manager",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Grade = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manager", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantManager",
                columns: table => new
                {
                    ManagersId = table.Column<Guid>(type: "uuid", nullable: false),
                    PendingApplicantsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantManager", x => new { x.ManagersId, x.PendingApplicantsId });
                    table.ForeignKey(
                        name: "FK_ApplicantManager_applicant_PendingApplicantsId",
                        column: x => x.PendingApplicantsId,
                        principalTable: "applicant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicantManager_manager_ManagersId",
                        column: x => x.ManagersId,
                        principalTable: "manager",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantManager_PendingApplicantsId",
                table: "ApplicantManager",
                column: "PendingApplicantsId");

            migrationBuilder.AddForeignKey(
                name: "FK_education_document_meta_applicant_ApplicantId",
                table: "education_document_meta",
                column: "ApplicantId",
                principalTable: "applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_education_document_meta_education_document_type_DocumentTyp~",
                table: "education_document_meta",
                column: "DocumentTypeId",
                principalTable: "education_document_type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_education_document_meta_EducationDocumentId",
                table: "file",
                column: "EducationDocumentId",
                principalTable: "education_document_meta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_passport_meta_PassportId",
                table: "file",
                column: "PassportId",
                principalTable: "passport_meta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_passport_meta_applicant_Id",
                table: "passport_meta",
                column: "Id",
                principalTable: "applicant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_education_document_meta_applicant_ApplicantId",
                table: "education_document_meta");

            migrationBuilder.DropForeignKey(
                name: "FK_education_document_meta_education_document_type_DocumentTyp~",
                table: "education_document_meta");

            migrationBuilder.DropForeignKey(
                name: "FK_file_education_document_meta_EducationDocumentId",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_file_passport_meta_PassportId",
                table: "file");

            migrationBuilder.DropForeignKey(
                name: "FK_passport_meta_applicant_Id",
                table: "passport_meta");

            migrationBuilder.DropTable(
                name: "ApplicantManager");

            migrationBuilder.DropTable(
                name: "manager");

            migrationBuilder.DropPrimaryKey(
                name: "PK_passport_meta",
                table: "passport_meta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_education_document_type",
                table: "education_document_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_education_document_meta",
                table: "education_document_meta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_applicant",
                table: "applicant");

            migrationBuilder.DropColumn(
                name: "IsAdmissionActive",
                table: "applicant");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "applicant");

            migrationBuilder.RenameTable(
                name: "passport_meta",
                newName: "Passports");

            migrationBuilder.RenameTable(
                name: "education_document_type",
                newName: "EducationDocumentTypes");

            migrationBuilder.RenameTable(
                name: "education_document_meta",
                newName: "Diplomas");

            migrationBuilder.RenameTable(
                name: "applicant",
                newName: "Applicants");

            migrationBuilder.RenameIndex(
                name: "IX_education_document_meta_DocumentTypeId",
                table: "Diplomas",
                newName: "IX_Diplomas_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_education_document_meta_ApplicantId",
                table: "Diplomas",
                newName: "IX_Diplomas_ApplicantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Passports",
                table: "Passports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EducationDocumentTypes",
                table: "EducationDocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Diplomas",
                table: "Diplomas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_Applicants_ApplicantId",
                table: "Diplomas",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_EducationDocumentTypes_DocumentTypeId",
                table: "Diplomas",
                column: "DocumentTypeId",
                principalTable: "EducationDocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_Diplomas_EducationDocumentId",
                table: "file",
                column: "EducationDocumentId",
                principalTable: "Diplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_file_Passports_PassportId",
                table: "file",
                column: "PassportId",
                principalTable: "Passports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Passports_Applicants_Id",
                table: "Passports",
                column: "Id",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
