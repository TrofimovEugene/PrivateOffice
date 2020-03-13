using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class BD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    IdGroup = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.IdGroup);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    IdTeacher = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    SecondName = table.Column<string>(nullable: false),
                    Patronymic = table.Column<string>(nullable: false),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.IdTeacher);
                });
            migrationBuilder.InsertData(
                 table: "Teacher",
                 columns: new[] { "Login", "Password", "FirstName", "SecondName", "Patronymic", "Role" },
                 values: new object[] { "Olga", "1111", "Ольга", "Курганская", "Викторовна", "admin" }
                );

            migrationBuilder.CreateTable(
                name: "TypeClasses",
                columns: table => new
                {
                    IdTypeClasses = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeClass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeClasses", x => x.IdTypeClasses);
                });

            migrationBuilder.InsertData(
            table: "TypeClasses",
            column: "TypeClass",
            value: "Лабораторная работа"
            );
            migrationBuilder.InsertData(
                table: "TypeClasses",
                column: "TypeClass",
                value: "Лекции"
                );
            migrationBuilder.InsertData(
                table: "TypeClasses",
                column: "TypeClass",
                value: "Семинар"
                );


            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IdStudent = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdGroup = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.IdStudent);
                    table.ForeignKey(
                        name: "FK_Student_Group_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "IdGroup",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    IdCourse = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NameCourse = table.Column<string>(nullable: true),
                    IdGroup = table.Column<int>(nullable: true),
                    IdTeacher = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    CountTime = table.Column<int>(nullable: false),
                    NameUniversity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.IdCourse);
                    table.ForeignKey(
                        name: "FK_Course_Group_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "IdGroup",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_Teacher_IdTeacher",
                        column: x => x.IdTeacher,
                        principalTable: "Teacher",
                        principalColumn: "IdTeacher",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    IdClasses = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdTypeClasses = table.Column<int>(nullable: false),
                    IdCourse = table.Column<int>(nullable: false),
                    IdGroup = table.Column<int>(nullable: true),
                    NameClasses = table.Column<string>(nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    DateClasses = table.Column<DateTime>(type: "date", nullable: false),
                    DaysWeek = table.Column<string>(nullable: true),
                    Cabinet = table.Column<string>(nullable: true),
                    ReplayClasses = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.IdClasses);
                    table.ForeignKey(
                        name: "FK_Classes_Course_IdCourse",
                        column: x => x.IdCourse,
                        principalTable: "Course",
                        principalColumn: "IdCourse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Group_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "IdGroup",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_TypeClasses_IdTypeClasses",
                        column: x => x.IdTypeClasses,
                        principalTable: "TypeClasses",
                        principalColumn: "IdTypeClasses",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ControlMeasures",
                columns: table => new
                {
                    IdControlMeasures = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                name: "Homework",
                columns: table => new
                {
                    IdHomework = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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

            migrationBuilder.CreateTable(
                name: "HomeworkGroup",
                columns: table => new
                {
                    IdHomeworkGroup = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdClasses = table.Column<int>(nullable: true),
                    IdGroup = table.Column<int>(nullable: true),
                    ContentHomeworkGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkGroup", x => x.IdHomeworkGroup);
                    table.ForeignKey(
                        name: "FK_HomeworkGroup_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeworkGroup_Group_IdGroup",
                        column: x => x.IdGroup,
                        principalTable: "Group",
                        principalColumn: "IdGroup",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VisitedStudents",
                columns: table => new
                {
                    IdVisitedStudent = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitedStudents_Student_IdStudent",
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
                        .Annotation("Sqlite:Autoincrement", true),
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
                        .Annotation("Sqlite:Autoincrement", true),
                    IdTicket = table.Column<int>(nullable: true),
                    IdControlMeasures = table.Column<int>(nullable: false),
                    ContentQuestions = table.Column<string>(nullable: true),
                    CountQuestions = table.Column<int>(nullable: false),
                    Point = table.Column<double>(type: "real", nullable: false)
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
                        .Annotation("Sqlite:Autoincrement", true),
                    IdTicket = table.Column<int>(nullable: true),
                    IdControlMeasures = table.Column<int>(nullable: false),
                    ContentTask = table.Column<string>(nullable: true),
                    CountTask = table.Column<int>(nullable: false),
                    Point = table.Column<double>(type: "real", nullable: false)
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
                name: "IX_Classes_IdCourse",
                table: "Classes",
                column: "IdCourse");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdGroup",
                table: "Classes",
                column: "IdGroup");

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
                name: "IX_Course_IdGroup",
                table: "Course",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Course_IdTeacher",
                table: "Course",
                column: "IdTeacher");

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

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkGroup_IdClasses",
                table: "HomeworkGroup",
                column: "IdClasses",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkGroup_IdGroup",
                table: "HomeworkGroup",
                column: "IdGroup");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_IdControlMeasures",
                table: "Questions",
                column: "IdControlMeasures");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_IdTicket",
                table: "Questions",
                column: "IdTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdGroup",
                table: "Student",
                column: "IdGroup");

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
                name: "Homework");

            migrationBuilder.DropTable(
                name: "HomeworkGroup");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "VisitedStudents");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "ControlMeasures");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "TypeClasses");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Teacher");
        }
    }
}
