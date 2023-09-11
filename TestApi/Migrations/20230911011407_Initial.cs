using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartnerTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partners_PartnerTypes_PartnerTypeId",
                        column: x => x.PartnerTypeId,
                        principalTable: "PartnerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Partners_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerSubject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerSubject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerSubject_Restrict_Student",
                        column: x => x.StudentId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PartnerSubject_Restrict_Subject",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PartnerTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Estudiante" },
                    { 2, "Profesor" }
                });

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "Name", "PartnerTypeId" },
                values: new object[,]
                {
                    { 1, "Profesor A", 2 },
                    { 2, "Profesor B", 2 },
                    { 3, "Profesor C", 2 },
                    { 4, "Profesor D", 2 },
                    { 5, "Profesor E", 2 }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Credits", "Name", "TeacherId" },
                values: new object[,]
                {
                    { 1, 3, "Física", 1 },
                    { 2, 3, "Matematicas", 1 },
                    { 3, 3, "Filosofía", 2 },
                    { 4, 3, "Psicología", 2 },
                    { 5, 3, "Literatura", 3 },
                    { 6, 3, "Idiomas Extranjeros", 3 },
                    { 7, 3, "Biología", 4 },
                    { 8, 3, "Química", 4 },
                    { 9, 3, "Historia del Arte", 5 },
                    { 10, 3, "Música", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partners_PartnerTypeId",
                table: "Partners",
                column: "PartnerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerSubject_StudentId",
                table: "PartnerSubject",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerSubject_SubjectId",
                table: "PartnerSubject",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TeacherId",
                table: "Subjects",
                column: "TeacherId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerSubject");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "PartnerTypes");
        }
    }
}
