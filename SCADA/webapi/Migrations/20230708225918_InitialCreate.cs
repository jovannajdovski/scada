﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalogInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanTime = table.Column<double>(type: "REAL", nullable: false),
                    IsScanning = table.Column<bool>(type: "INTEGER", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogInputs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnalogOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialValue = table.Column<double>(type: "REAL", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogOutputs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanTime = table.Column<double>(type: "REAL", nullable: false),
                    IsScanning = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalInputs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DigitalOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalOutputs_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealTimeUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealTimeUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealTimeUnits_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Limit = table.Column<double>(type: "REAL", nullable: false),
                    AnalogInputId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_AnalogInputs_AnalogInputId",
                        column: x => x.AnalogInputId,
                        principalTable: "AnalogInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Type" },
                values: new object[] { "admin", "admin", 0 });
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Type" },
                values: new object[] { "user", "user", 1 });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Type", "Value" },
                values: new object[] { "double", "200.0" }
                );
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Type", "Value" },
                values: new object[] { "double", "200.0" }
                );
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Type", "Value" },
                values: new object[] { "double", "20.0" }
                );

            migrationBuilder.InsertData(
               table: "AnalogInputs",
               columns: new[] { "Description", "AddressId", "ScanTime", "IsScanning", "LowLimit", "HighLimit", "Unit" },
               values: new object[] { "Water level in pool", 1, 1.0, true, 100.0, 1000.0, "cm" });
            migrationBuilder.InsertData(
               table: "AnalogInputs",
               columns: new[] { "Description", "AddressId", "ScanTime", "IsScanning", "LowLimit", "HighLimit", "Unit" },
               values: new object[] { "Gas volume in pool", 2, 2.0, true, 100.0, 1000.0, "kiloliter" });
            migrationBuilder.InsertData(
               table: "AnalogInputs",
               columns: new[] { "Description", "AddressId", "ScanTime", "IsScanning", "LowLimit", "HighLimit", "Unit" },
               values: new object[] { "Coal amount in furnace", 3, 1.0, true, 0.0, 100.0, "kg" });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogInputs_AddressId",
                table: "AnalogInputs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogOutputs_AddressId",
                table: "AnalogOutputs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalInputs_AddressId",
                table: "DigitalInputs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalOutputs_AddressId",
                table: "DigitalOutputs",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_RealTimeUnits_AddressId",
                table: "RealTimeUnits",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "AnalogOutputs");

            migrationBuilder.DropTable(
                name: "DigitalInputs");

            migrationBuilder.DropTable(
                name: "DigitalOutputs");

            migrationBuilder.DropTable(
                name: "RealTimeUnits");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AnalogInputs");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}