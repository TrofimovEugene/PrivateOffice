using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class UpdateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmVisit",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Visited",
                table: "Student");

            migrationBuilder.CreateTable(
                name: "VisitedStudents",
                columns: table => new
                {
                    IdVisitedStudent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStudent = table.Column<int>(nullable: true),
                    IdClasses = table.Column<int>(nullable: true),
                    Visited = table.Column<bool>(nullable: false),
                    ConfirmVisited = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitedStudents", x => x.IdVisitedStudent);
                    table.ForeignKey(
                        name: "FK_VisitedStudents_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitedStudents_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisitedStudents_IdClasses",
                table: "VisitedStudents",
                column: "IdClasses");

            migrationBuilder.CreateIndex(
                name: "IX_VisitedStudents_IdStudent",
                table: "VisitedStudents",
                column: "IdStudent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitedStudents");

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmVisit",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visited",
                table: "Student",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
