﻿// <auto-generated />
using System;
using FinalPoint.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FinalPoint.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210527130958_fixProtocolParcel")]
    partial class fixProtocolParcel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FinalPoint.Data.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [MiddleName] + ' ' + [LastName]");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("WorkOfficeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("WorkOfficeId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Postcode")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Office", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OfficeType")
                        .HasColumnType("int");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("PostCode")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleSortingCenterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OwnerId1");

                    b.HasIndex("ResponsibleSortingCenterId");

                    b.ToTable("Offices");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("CashOnDeliveryPrice")
                        .HasColumnType("float");

                    b.Property<int>("ChargeType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentOfficeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeliveringEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("DeliveringEmployeeId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DontPaletize")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCashOnDelivery")
                        .HasColumnType("bit");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFragile")
                        .HasColumnType("bit");

                    b.Property<double>("Length")
                        .HasColumnType("float");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceivingOfficeId")
                        .HasColumnType("int");

                    b.Property<int>("RecipentId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("SendingEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("SendingEmployeeId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SendingOfficeId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CurrentOfficeId");

                    b.HasIndex("DeliveringEmployeeId1");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("ReceivingOfficeId");

                    b.HasIndex("RecipentId");

                    b.HasIndex("SenderId");

                    b.HasIndex("SendingEmployeeId1");

                    b.HasIndex("SendingOfficeId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Protocol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedByEmployeeId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeFromId")
                        .HasColumnType("int");

                    b.Property<int>("OfficeToId")
                        .HasColumnType("int");

                    b.Property<int?>("ParcelId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByEmployeeId1");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OfficeFromId");

                    b.HasIndex("OfficeToId");

                    b.HasIndex("ParcelId");

                    b.ToTable("Protocols");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.ProtocolParcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("OfficeStorageFromId")
                        .HasColumnType("int");

                    b.Property<int>("OfficeStorageToId")
                        .HasColumnType("int");

                    b.Property<int>("ParcelId")
                        .HasColumnType("int");

                    b.Property<int>("ProtocolId")
                        .HasColumnType("int");

                    b.Property<int>("ResponsibleUserId")
                        .HasColumnType("int");

                    b.Property<string>("ResponsibleUserId1")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeEdited")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.HasIndex("OfficeStorageFromId");

                    b.HasIndex("OfficeStorageToId");

                    b.HasIndex("ParcelId");

                    b.HasIndex("ProtocolId");

                    b.HasIndex("ResponsibleUserId1");

                    b.ToTable("ProtocolParcels");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IsDeleted");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.ApplicationUser", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.Office", "WorkOffice")
                        .WithMany("Employees")
                        .HasForeignKey("WorkOfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("WorkOffice");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Office", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.City", "City")
                        .WithMany("Offices")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", "Owner")
                        .WithMany("OwnOffices")
                        .HasForeignKey("OwnerId1");

                    b.HasOne("FinalPoint.Data.Models.Office", "ResponsibleSortingCenter")
                        .WithMany()
                        .HasForeignKey("ResponsibleSortingCenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Owner");

                    b.Navigation("ResponsibleSortingCenter");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Parcel", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.Office", "CurrentOffice")
                        .WithMany("Parcels")
                        .HasForeignKey("CurrentOfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", "DeliveringEmployee")
                        .WithMany()
                        .HasForeignKey("DeliveringEmployeeId1");

                    b.HasOne("FinalPoint.Data.Models.Office", "ReceivingOffice")
                        .WithMany()
                        .HasForeignKey("ReceivingOfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Client", "Recipent")
                        .WithMany("ReceivedParcels")
                        .HasForeignKey("RecipentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Client", "Sender")
                        .WithMany("SentParcels")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", "SendingEmployee")
                        .WithMany()
                        .HasForeignKey("SendingEmployeeId1");

                    b.HasOne("FinalPoint.Data.Models.Office", "SendingOffice")
                        .WithMany("SentParcels")
                        .HasForeignKey("SendingOfficeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrentOffice");

                    b.Navigation("DeliveringEmployee");

                    b.Navigation("ReceivingOffice");

                    b.Navigation("Recipent");

                    b.Navigation("Sender");

                    b.Navigation("SendingEmployee");

                    b.Navigation("SendingOffice");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Protocol", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", "CreatedByEmployee")
                        .WithMany()
                        .HasForeignKey("CreatedByEmployeeId1");

                    b.HasOne("FinalPoint.Data.Models.Office", "OfficeFrom")
                        .WithMany("ProtocolsFrom")
                        .HasForeignKey("OfficeFromId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Office", "OfficeTo")
                        .WithMany("ProtocolsTo")
                        .HasForeignKey("OfficeToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Parcel", null)
                        .WithMany("Protocols")
                        .HasForeignKey("ParcelId");

                    b.Navigation("CreatedByEmployee");

                    b.Navigation("OfficeFrom");

                    b.Navigation("OfficeTo");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.ProtocolParcel", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.Office", "OfficeStorageFrom")
                        .WithMany()
                        .HasForeignKey("OfficeStorageFromId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Office", "OfficeStorageTo")
                        .WithMany()
                        .HasForeignKey("OfficeStorageToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Parcel", "Parcel")
                        .WithMany()
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.Protocol", "Protocol")
                        .WithMany("Parcels")
                        .HasForeignKey("ProtocolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", "ResponsibleUser")
                        .WithMany()
                        .HasForeignKey("ResponsibleUserId1");

                    b.Navigation("OfficeStorageFrom");

                    b.Navigation("OfficeStorageTo");

                    b.Navigation("Parcel");

                    b.Navigation("Protocol");

                    b.Navigation("ResponsibleUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("FinalPoint.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalPoint.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Logins");

                    b.Navigation("OwnOffices");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.City", b =>
                {
                    b.Navigation("Offices");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Client", b =>
                {
                    b.Navigation("ReceivedParcels");

                    b.Navigation("SentParcels");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Office", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Parcels");

                    b.Navigation("ProtocolsFrom");

                    b.Navigation("ProtocolsTo");

                    b.Navigation("SentParcels");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Parcel", b =>
                {
                    b.Navigation("Protocols");
                });

            modelBuilder.Entity("FinalPoint.Data.Models.Protocol", b =>
                {
                    b.Navigation("Parcels");
                });
#pragma warning restore 612, 618
        }
    }
}
