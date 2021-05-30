using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class FixParcelProtocol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProtocolParcels_Offices_OfficeStorageFromId",
                table: "ProtocolParcels");

            migrationBuilder.DropForeignKey(
                name: "FK_ProtocolParcels_Offices_OfficeStorageToId",
                table: "ProtocolParcels");

            migrationBuilder.DropIndex(
                name: "IX_ProtocolParcels_OfficeStorageFromId",
                table: "ProtocolParcels");

            migrationBuilder.DropIndex(
                name: "IX_ProtocolParcels_OfficeStorageToId",
                table: "ProtocolParcels");

            migrationBuilder.DropColumn(
                name: "OfficeStorageFromId",
                table: "ProtocolParcels");

            migrationBuilder.DropColumn(
                name: "OfficeStorageToId",
                table: "ProtocolParcels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeStorageFromId",
                table: "ProtocolParcels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OfficeStorageToId",
                table: "ProtocolParcels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_OfficeStorageFromId",
                table: "ProtocolParcels",
                column: "OfficeStorageFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_OfficeStorageToId",
                table: "ProtocolParcels",
                column: "OfficeStorageToId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProtocolParcels_Offices_OfficeStorageFromId",
                table: "ProtocolParcels",
                column: "OfficeStorageFromId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProtocolParcels_Offices_OfficeStorageToId",
                table: "ProtocolParcels",
                column: "OfficeStorageToId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
