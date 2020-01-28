using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTypeClasses",
                table: "Classes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    IdGroup = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClasses = table.Column<int>(nullable: false),
                    NameGroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.IdGroup);
                    table.ForeignKey(
                        name: "FK_Group_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeClasses",
                columns: table => new
                {
                    IdTypeClasses = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeClass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeClasses", x => x.IdTypeClasses);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    IdStudent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGroup = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_IdClasses",
                table: "Group",
                column: "IdClasses");

            migrationBuilder.CreateIndex(
                name: "IX_Student_IdGroup",
                table: "Student",
                column: "IdGroup");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TypeClasses_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses",
                principalTable: "TypeClasses",
                principalColumn: "IdTypeClasses",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TypeClasses_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "TypeClasses");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IdTypeClasses",
                table: "Classes");
        }
    }
}
