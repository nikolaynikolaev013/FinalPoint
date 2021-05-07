using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalPoint.Data.Migrations
{
    public partial class InitialTablesCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostCode = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeType = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    OwnerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleSortingCenterId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offices_AspNetUsers_OwnerId1",
                        column: x => x.OwnerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offices_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offices_Offices_ResponsibleSortingCenterId",
                        column: x => x.ResponsibleSortingCenterId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parcels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SendingEmployeeId = table.Column<int>(type: "int", nullable: false),
                    SendingEmployeeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DeliveringEmployeeId = table.Column<int>(type: "int", nullable: false),
                    DeliveringEmployeeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    SendingOfficeId = table.Column<int>(type: "int", nullable: false),
                    ReceivingOfficeId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    RecipentId = table.Column<int>(type: "int", nullable: false),
                    CurrentOfficeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parcels_AspNetUsers_DeliveringEmployeeId1",
                        column: x => x.DeliveringEmployeeId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_AspNetUsers_SendingEmployeeId1",
                        column: x => x.SendingEmployeeId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Clients_RecipentId",
                        column: x => x.RecipentId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Clients_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Offices_CurrentOfficeId",
                        column: x => x.CurrentOfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Offices_ReceivingOfficeId",
                        column: x => x.ReceivingOfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Parcels_Offices_SendingOfficeId",
                        column: x => x.SendingOfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Protocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedByEmployeeId = table.Column<int>(type: "int", nullable: false),
                    CreatedByEmployeeId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ParcelId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Protocols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Protocols_AspNetUsers_CreatedByEmployeeId1",
                        column: x => x.CreatedByEmployeeId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Protocols_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProtocolParcels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParcelId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleUserId = table.Column<int>(type: "int", nullable: false),
                    ResponsibleUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TimeEdited = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OfficeStorageFromId = table.Column<int>(type: "int", nullable: false),
                    OfficeStorageToId = table.Column<int>(type: "int", nullable: false),
                    ProtocolId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProtocolParcels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProtocolParcels_AspNetUsers_ResponsibleUserId1",
                        column: x => x.ResponsibleUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtocolParcels_Offices_OfficeStorageFromId",
                        column: x => x.OfficeStorageFromId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtocolParcels_Offices_OfficeStorageToId",
                        column: x => x.OfficeStorageToId,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtocolParcels_Parcels_ParcelId",
                        column: x => x.ParcelId,
                        principalTable: "Parcels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProtocolParcels_Protocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "Protocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OfficeId",
                table: "AspNetUsers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_IsDeleted",
                table: "Clients",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_CityId",
                table: "Offices",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_IsDeleted",
                table: "Offices",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_OwnerId1",
                table: "Offices",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_ResponsibleSortingCenterId",
                table: "Offices",
                column: "ResponsibleSortingCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_CurrentOfficeId",
                table: "Parcels",
                column: "CurrentOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_DeliveringEmployeeId1",
                table: "Parcels",
                column: "DeliveringEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_IsDeleted",
                table: "Parcels",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_ReceivingOfficeId",
                table: "Parcels",
                column: "ReceivingOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_RecipentId",
                table: "Parcels",
                column: "RecipentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SenderId",
                table: "Parcels",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SendingEmployeeId1",
                table: "Parcels",
                column: "SendingEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Parcels_SendingOfficeId",
                table: "Parcels",
                column: "SendingOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_IsDeleted",
                table: "ProtocolParcels",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_OfficeStorageFromId",
                table: "ProtocolParcels",
                column: "OfficeStorageFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_OfficeStorageToId",
                table: "ProtocolParcels",
                column: "OfficeStorageToId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_ParcelId",
                table: "ProtocolParcels",
                column: "ParcelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_ProtocolId",
                table: "ProtocolParcels",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProtocolParcels_ResponsibleUserId1",
                table: "ProtocolParcels",
                column: "ResponsibleUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_CreatedByEmployeeId1",
                table: "Protocols",
                column: "CreatedByEmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_IsDeleted",
                table: "Protocols",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Protocols_ParcelId",
                table: "Protocols",
                column: "ParcelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Offices_OfficeId",
                table: "AspNetUsers",
                column: "OfficeId",
                principalTable: "Offices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Offices_OfficeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ProtocolParcels");

            migrationBuilder.DropTable(
                name: "Protocols");

            migrationBuilder.DropTable(
                name: "Parcels");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Offices");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OfficeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "AspNetUsers");
        }
    }
}
