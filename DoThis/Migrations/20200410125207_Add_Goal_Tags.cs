using Microsoft.EntityFrameworkCore.Migrations;

namespace Beeffective.Migrations
{
    public partial class Add_Goal_Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Goal",
                table: "Cells",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Cells",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Cells");
        }
    }
}
