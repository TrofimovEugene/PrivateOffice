using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeDataBaseAPI.Migrations
{
    public partial class UpdateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TypeClasses_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropTable(
                name: "TypeClasses");

            migrationBuilder.DropIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "IdTypeClasses",
                table: "Classes");

            migrationBuilder.AddColumn<string>(
                name: "TypeClasses",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeClasses",
                table: "Classes");

            migrationBuilder.AddColumn<int>(
                name: "IdTypeClasses",
                table: "Classes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeClasses",
                columns: table => new
                {
                    IdTypeClasses = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeClasses", x => x.IdTypeClasses);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TypeClasses_IdTypeClasses",
                table: "Classes",
                column: "IdTypeClasses",
                principalTable: "TypeClasses",
                principalColumn: "IdTypeClasses",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
