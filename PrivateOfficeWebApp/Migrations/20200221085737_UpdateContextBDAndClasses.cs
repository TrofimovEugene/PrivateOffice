using Microsoft.EntityFrameworkCore.Migrations;

namespace PrivateOfficeWebApp.Migrations
{
    public partial class UpdateContextBDAndClasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cabinet",
                table: "Classes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cabinet",
                table: "Classes");
        }
    }
}
