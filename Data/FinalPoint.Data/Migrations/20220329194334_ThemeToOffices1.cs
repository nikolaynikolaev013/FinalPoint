using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalPoint.Data.Migrations
{
    public partial class ThemeToOffices1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Offices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Offices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
