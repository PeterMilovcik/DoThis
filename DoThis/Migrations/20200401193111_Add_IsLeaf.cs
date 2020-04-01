using Microsoft.EntityFrameworkCore.Migrations;

namespace DoThis.Migrations
{
    public partial class Add_IsLeaf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLeaf",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLeaf",
                table: "Items");
        }
    }
}
