using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class FinishedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "CountClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "CountTime",
                table: "Classes");

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmVisit",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Visited",
                table: "Student",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CountTime",
                table: "Course",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ReplayClasses",
                table: "Classes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ControlMeasures",
                columns: table => new
                {
                    IdControlMeasures = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClasses = table.Column<int>(nullable: false),
                    IdStudent = table.Column<int>(nullable: true),
                    NameControlMeasures = table.Column<string>(nullable: true),
                    CountControlMeasures = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlMeasures", x => x.IdControlMeasures);
                    table.ForeignKey(
                        name: "FK_ControlMeasures_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlMeasures_Student_IdStudent",
                        column: x => x.IdStudent,
                        principalTable: "Student",
                        principalColumn: "IdStudent",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    IdTicket = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberTicket = table.Column<int>(nullable: false),
                    CountTicket = table.Column<int>(nullable: false),
                    IdControlMeasures = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.IdTicket);
                    table.ForeignKey(
                        name: "FK_Ticket_ControlMeasures_IdControlMeasures",
                        column: x => x.IdControlMeasures,
                        principalTable: "ControlMeasures",
                        principalColumn: "IdControlMeasures",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    IdQuestions = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTicket = table.Column<int>(nullable: true),
                    IdControlMeasures = table.Column<int>(nullable: false),
                    ContentQuestions = table.Column<string>(nullable: true),
                    CountQuestions = table.Column<int>(nullable: false),
                    Point = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.IdQuestions);
                    table.ForeignKey(
                        name: "FK_Questions_ControlMeasures_IdControlMeasures",
                        column: x => x.IdControlMeasures,
                        principalTable: "ControlMeasures",
                        principalColumn: "IdControlMeasures",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_Ticket_IdTicket",
                        column: x => x.IdTicket,
                        principalTable: "Ticket",
                        principalColumn: "IdTicket",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    IdTask = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTicket = table.Column<int>(nullable: true),
                    IdControlMeasures = table.Column<int>(nullable: false),
                    ContentTask = table.Column<string>(nullable: true),
                    CountTask = table.Column<int>(nullable: false),
                    Point = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.IdTask);
                    table.ForeignKey(
                        name: "FK_Task_ControlMeasures_IdControlMeasures",
                        column: x => x.IdControlMeasures,
                        principalTable: "ControlMeasures",
                        principalColumn: "IdControlMeasures",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_Ticket_IdTicket",
                        column: x => x.IdTicket,
                        principalTable: "Ticket",
                        principalColumn: "IdTicket",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses");

            migrationBuilder.CreateIndex(
                name: "IX_ControlMeasures_IdClasses",
                table: "ControlMeasures",
                column: "IdClasses");

            migrationBuilder.CreateIndex(
                name: "IX_ControlMeasures_IdStudent",
                table: "ControlMeasures",
                column: "IdStudent");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_IdControlMeasures",
                table: "Questions",
                column: "IdControlMeasures");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_IdTicket",
                table: "Questions",
                column: "IdTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdControlMeasures",
                table: "Task",
                column: "IdControlMeasures");

            migrationBuilder.CreateIndex(
                name: "IX_Task_IdTicket",
                table: "Task",
                column: "IdTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdControlMeasures",
                table: "Ticket",
                column: "IdControlMeasures");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "ControlMeasures");

            migrationBuilder.DropIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "ConfirmVisit",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Visited",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CountTime",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "ReplayClasses",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "CountClasses",
                table: "Classes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountTime",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses",
                unique: true);
        }
    }
}
