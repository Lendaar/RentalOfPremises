﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalOfPremises.Context;

#nullable disable

namespace RentalOfPremises.Context.Migrations
{
    [DbContext(typeof(RentalOfPremisesContext))]
    [Migration("20240520103226_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Archive")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset>("DateEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateStart")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<decimal>("Payment")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .HasDatabaseName("IX_Contract_Number")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("RoomId");

                    b.HasIndex("TenantId");

                    b.ToTable("Contracts", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.PaymentInvoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Electricity")
                        .HasColumnType("int");

                    b.Property<int>("NumberContract")
                        .HasColumnType("int");

                    b.Property<int?>("PassGrСar")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PassLegСar")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int>("PassPerson")
                        .HasColumnType("int");

                    b.Property<int>("PeriodPayment")
                        .HasColumnType("int");

                    b.Property<Guid>("PriceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("WaterMi")
                        .HasColumnType("int");

                    b.Property<int>("WaterPl")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NumberContract")
                        .HasDatabaseName("IX_PaymentInvoice_NumberContract")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("PriceId");

                    b.ToTable("PaymentInvoices", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Price", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Electricity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PassGrСar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PassLegСar")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PassPerson")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("WaterMi")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("WaterPl")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Prices", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Liter")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("NumberRoom")
                        .HasColumnType("int");

                    b.Property<bool>("Occupied")
                        .HasColumnType("bit");

                    b.Property<double>("SquareRoom")
                        .HasColumnType("float");

                    b.Property<int>("TypeRoom")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Liter")
                        .HasDatabaseName("IX_Room_Liter")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Rooms", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bik")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Kpp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ks")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ogrn")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Okpo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rs")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Inn")
                        .IsUnique()
                        .HasDatabaseName("IX_Tenant_Inn")
                        .HasFilter("DeletedAt is null");

                    b.HasIndex("Ogrn")
                        .IsUnique();

                    b.HasIndex("Okpo")
                        .IsUnique();

                    b.HasIndex("Telephone")
                        .IsUnique();

                    b.ToTable("Tenants", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LoginUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleUser")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("UpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("LoginUser")
                        .IsUnique()
                        .HasDatabaseName("IX_User_LoginUser")
                        .HasFilter("DeletedAt is null");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Contract", b =>
                {
                    b.HasOne("RentalOfPremises.Context.Contracts.Models.Room", "Room")
                        .WithMany("Contract")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RentalOfPremises.Context.Contracts.Models.Tenant", "Tenant")
                        .WithMany("Contract")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.PaymentInvoice", b =>
                {
                    b.HasOne("RentalOfPremises.Context.Contracts.Models.Price", "Price")
                        .WithMany("PaymentInvoice")
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Price");
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Price", b =>
                {
                    b.Navigation("PaymentInvoice");
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Room", b =>
                {
                    b.Navigation("Contract");
                });

            modelBuilder.Entity("RentalOfPremises.Context.Contracts.Models.Tenant", b =>
                {
                    b.Navigation("Contract");
                });
#pragma warning restore 612, 618
        }
    }
}