using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoThis.Migrations
{
    public partial class Add_AggregatedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AggregatedTime",
                table: "Items",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AggregatedTime",
                table: "Items");
        }
    }
}
