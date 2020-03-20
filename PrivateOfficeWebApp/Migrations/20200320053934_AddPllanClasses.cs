using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class AddPllanClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanClasses",
                columns: table => new
                {
                    IdPlanClasses = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdClasses = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    Poll = table.Column<string>(nullable: true),
                    Block = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanClasses", x => x.IdPlanClasses);
                    table.ForeignKey(
                        name: "FK_PlanClasses_Classes_IdClasses",
                        column: x => x.IdClasses,
                        principalTable: "Classes",
                        principalColumn: "IdClasses",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanClasses_IdClasses",
                table: "PlanClasses",
                column: "IdClasses",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanClasses");
        }
    }
}
