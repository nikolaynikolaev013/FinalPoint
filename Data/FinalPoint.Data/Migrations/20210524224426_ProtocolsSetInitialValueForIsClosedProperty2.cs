using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class ProtocolsSetInitialValueForIsClosedProperty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromId",
                table: "Protocols",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ToId",
                table: "Protocols",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_FromId",
                table: "Protocols",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_ToId",
                table: "Protocols",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocols_Offices_FromId",
                table: "Protocols",
                column: "FromId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Protocols_Offices_ToId",
                table: "Protocols",
                column: "ToId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protocols_Offices_FromId",
                table: "Protocols");

            migrationBuilder.DropForeignKey(
                name: "FK_Protocols_Offices_ToId",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Protocols_FromId",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Protocols_ToId",
                table: "Protocols");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Protocols");

            migrationBuilder.DropColumn(
                name: "ToId",
                table: "Protocols");
        }
    }
}
