using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Update_City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Done",
                table: "Cities");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Cities",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Cities");

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "Cities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
