using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class AddHomeworkGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeworkGroup",
                columns: table => new
                {
                    IdHomeworkGroup = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkGroup_IdClasses",
                table: "HomeworkGroup",
                column: "IdClasses",
                unique: true,
                filter: "[IdClasses] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkGroup_IdGroup",
                table: "HomeworkGroup",
                column: "IdGroup");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeworkGroup");
        }
    }
}
