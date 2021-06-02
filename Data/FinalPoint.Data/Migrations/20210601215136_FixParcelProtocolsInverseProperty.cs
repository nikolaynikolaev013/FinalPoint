using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class FixParcelProtocolsInverseProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protocols_Parcels_ParcelId",
                table: "Protocols");

            migrationBuilder.DropIndex(
                name: "IX_Protocols_ParcelId",
                table: "Protocols");

            migrationBuilder.DropColumn(
                name: "ParcelId",
                table: "Protocols");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParcelId",
                table: "Protocols",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_ParcelId",
                table: "Protocols",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocols_Parcels_ParcelId",
                table: "Protocols",
                column: "ParcelId",
                principalTable: "Parcels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
