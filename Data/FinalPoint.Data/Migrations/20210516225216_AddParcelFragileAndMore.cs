using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class AddParcelFragileAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CashOnDeliveryPrice",
                table: "Parcels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DontPaletize",
                table: "Parcels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasCashOnDelivery",
                table: "Parcels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFragile",
                table: "Parcels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashOnDeliveryPrice",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "DontPaletize",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "HasCashOnDelivery",
                table: "Parcels");

            migrationBuilder.DropColumn(
                name: "IsFragile",
                table: "Parcels");
        }
    }
}
