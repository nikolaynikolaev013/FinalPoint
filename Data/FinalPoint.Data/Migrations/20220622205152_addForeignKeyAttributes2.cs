using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalPoint.Data.Migrations
{
    public partial class addForeignKeyAttributes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProtocolParcels_AspNetUsers_ResponsibleUserId1",
                table: "ProtocolParcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Protocols_AspNetUsers_CreatedByEmployeeId1",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Protocols_CreatedByEmployeeId1",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_ProtocolParcels_ResponsibleUserId1",
                table: "ProtocolParcels");

            migrationBuilder.DropColumn(
                name: "CreatedByEmployeeId1",
                table: "Protocols");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId1",
                table: "ProtocolParcels");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByEmployeeId",
                table: "Protocols",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ResponsibleUserId",
                table: "ProtocolParcels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_CreatedByEmployeeId",
                table: "Protocols",
                column: "CreatedByEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_ResponsibleUserId",
                table: "ProtocolParcels",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProtocolParcels_AspNetUsers_ResponsibleUserId",
                table: "ProtocolParcels",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocols_AspNetUsers_CreatedByEmployeeId",
                table: "Protocols",
                column: "CreatedByEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProtocolParcels_AspNetUsers_ResponsibleUserId",
                table: "ProtocolParcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Protocols_AspNetUsers_CreatedByEmployeeId",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Protocols_CreatedByEmployeeId",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_ProtocolParcels_ResponsibleUserId",
                table: "ProtocolParcels");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByEmployeeId",
                table: "Protocols",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByEmployeeId1",
                table: "Protocols",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ResponsibleUserId",
                table: "ProtocolParcels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUserId1",
                table: "ProtocolParcels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_CreatedByEmployeeId1",
                table: "Protocols",
                column: "CreatedByEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_ResponsibleUserId1",
                table: "ProtocolParcels",
                column: "ResponsibleUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProtocolParcels_AspNetUsers_ResponsibleUserId1",
                table: "ProtocolParcels",
                column: "ResponsibleUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocols_AspNetUsers_CreatedByEmployeeId1",
                table: "Protocols",
                column: "CreatedByEmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
