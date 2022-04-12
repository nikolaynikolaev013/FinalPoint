using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalPoint.Data.Migrations
{
    public partial class ThemeToOffices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "Offices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_ThemeId",
                table: "Offices",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ThemeId",
                table: "AspNetUsers",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Themes_ThemeId",
                table: "AspNetUsers",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Themes_ThemeId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offices_Themes_ThemeId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_ThemeId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ThemeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "AspNetUsers");
        }
    }
}
