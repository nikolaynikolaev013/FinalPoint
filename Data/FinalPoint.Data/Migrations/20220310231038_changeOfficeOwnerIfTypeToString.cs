using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class changeOfficeOwnerIfTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_AspNetUsers_OwnerId1",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_OwnerId1",
                table: "Offices");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Offices");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Offices",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OwnerId",
                table: "Offices",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_AspNetUsers_OwnerId",
                table: "Offices",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offices_AspNetUsers_OwnerId",
                table: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_Offices_OwnerId",
                table: "Offices");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Offices",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Offices",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OwnerId1",
                table: "Offices",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Offices_AspNetUsers_OwnerId1",
                table: "Offices",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
