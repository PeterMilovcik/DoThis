using Microsoft.EntityFrameworkCore.Migrations;

namespace Beeffective.Migrations
{
    public partial class Add_UrgencyAndImportance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Importance",
                table: "Cells",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Urgency",
                table: "Cells",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Importance",
                table: "Cells");

            migrationBuilder.DropColumn(
                name: "Urgency",
                table: "Cells");
        }
    }
}
