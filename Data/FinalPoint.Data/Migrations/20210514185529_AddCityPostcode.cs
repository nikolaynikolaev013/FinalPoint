using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class AddCityPostcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Postcode",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "Cities");
        }
    }
}
