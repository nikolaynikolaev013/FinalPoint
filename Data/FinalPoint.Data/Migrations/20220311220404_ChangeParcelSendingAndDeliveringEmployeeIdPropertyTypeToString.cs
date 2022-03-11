using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class ChangeParcelSendingAndDeliveringEmployeeIdPropertyTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveringEmployeeId1",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_SendingEmployeeId1",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_DeliveringEmployeeId1",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_SendingEmployeeId1",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "DeliveringEmployeeId1",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "SendingEmployeeId1",
                table: "Parcels");

            migrationBuilder.AlterColumn<string>(
                name: "SendingEmployeeId",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveringEmployeeId",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DeliveringEmployeeId",
                table: "Parcels",
                column: "DeliveringEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SendingEmployeeId",
                table: "Parcels",
                column: "SendingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveringEmployeeId",
                table: "Parcels",
                column: "DeliveringEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_SendingEmployeeId",
                table: "Parcels",
                column: "SendingEmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveringEmployeeId",
                table: "Parcels");

            migrationBuilder.DropForeignKey(
                name: "FK_Parcels_AspNetUsers_SendingEmployeeId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_DeliveringEmployeeId",
                table: "Parcels");

            migrationBuilder.DropIndex(
                name: "IX_Parcels_SendingEmployeeId",
                table: "Parcels");

            migrationBuilder.AlterColumn<int>(
                name: "SendingEmployeeId",
                table: "Parcels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveringEmployeeId",
                table: "Parcels",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveringEmployeeId1",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SendingEmployeeId1",
                table: "Parcels",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DeliveringEmployeeId1",
                table: "Parcels",
                column: "DeliveringEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SendingEmployeeId1",
                table: "Parcels",
                column: "SendingEmployeeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_DeliveringEmployeeId1",
                table: "Parcels",
                column: "DeliveringEmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Parcels_AspNetUsers_SendingEmployeeId1",
                table: "Parcels",
                column: "SendingEmployeeId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
