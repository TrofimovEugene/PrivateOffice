using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class HomeworkDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.CreateTable(
                name: "Homework",
                columns: table => new
                {
                    IdHomework = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdStudent = table.Column<int>(nullable: true),
                    IdClasses = table.Column<int>(nullable: true),
                    IdGroup = table.Column<int>(nullable: true),
                    ContentHomework = table.Column<string>(nullable: true),
                    TypeHomework = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homework", x => x.IdHomework);
                    table.ForeignKey(
                        name: "FK_Homework_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homework_Group_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "IdGroup",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Homework_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Homework_IdClasses",
                table: "Homework",
                column: "IdClasses");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_IdGroup",
                table: "Homework",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Homework_IdStudent",
                table: "Homework",
                column: "IdStudent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Homework");

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    IdReport = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClasses = table.Column<int>(type: "int", nullable: true),
                    IdStudent = table.Column<int>(type: "int", nullable: true),
                    NameReport = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.IdReport);
                    table.ForeignKey(
                        name: "FK_Report_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Report_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_IdClasses",
                table: "Report",
                column: "IdClasses");

            migrationBuilder.CreateIndex(
                name: "IX_Report_IdStudent",
                table: "Report",
                column: "IdStudent");
        }
    }
}
