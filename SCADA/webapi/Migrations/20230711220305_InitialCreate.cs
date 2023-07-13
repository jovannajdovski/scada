using System;
using Microsoft.EntityFrameworkCore.Migrations;

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
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
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
                name: "TagBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    AddressId = table.Column<int>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    AnalogInput_LowLimit = table.Column<double>(type: "REAL", nullable: true),
                    AnalogInput_HighLimit = table.Column<double>(type: "REAL", nullable: true),
                    AnalogInput_Unit = table.Column<string>(type: "TEXT", nullable: true),
                    ScanTime = table.Column<double>(type: "REAL", nullable: true),
                    IsScanning = table.Column<bool>(type: "INTEGER", nullable: true),
                    LowLimit = table.Column<double>(type: "REAL", nullable: true),
                    HighLimit = table.Column<double>(type: "REAL", nullable: true),
                    Unit = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagBases_Addresses_AddressId",
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
                    AnalogInputId = table.Column<int>(type: "INTEGER", nullable: false),
                    isMuted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarms_TagBases_AnalogInputId",
                        column: x => x.AnalogInputId,
                        principalTable: "TagBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TagValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TagBaseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagValues_TagBases_TagBaseId",
                        column: x => x.TagBaseId,
                        principalTable: "TagBases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlarmsTriggers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlarmId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmsTriggers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlarmsTriggers_Alarms_AlarmId",
                        column: x => x.AlarmId,
                        principalTable: "Alarms",
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

            for (int i = 0; i < 100; i++)
            {
                migrationBuilder.InsertData(
                    table: "Addresses",
                    columns: new[] { "Type", "Value" },
                    values: new object[] { default(string?), default(string?) }
                );
            }

            migrationBuilder.InsertData(
               table: "TagBases",
               columns: new[] { "Discriminator", "Description", "AddressId", "ScanTime", "IsScanning", "AnalogInput_LowLimit", "AnalogInput_HighLimit", "AnalogInput_Unit" },
               values: new object[] { "AnalogInput", "Water level in pool", 1, 5.0, true, 100.0, 1000.0, "cm" });
            migrationBuilder.InsertData(
               table: "TagBases",
               columns: new[] { "Discriminator", "Description", "AddressId", "ScanTime", "IsScanning", "AnalogInput_LowLimit", "AnalogInput_HighLimit", "AnalogInput_Unit" },
               values: new object[] { "AnalogInput", "Gas volume in pool", 2, 2.0, true, 100.0, 1000.0, "kiloliter" });
            migrationBuilder.InsertData(
               table: "TagBases",
               columns: new[] { "Discriminator", "Description", "AddressId", "ScanTime", "IsScanning", "AnalogInput_LowLimit", "AnalogInput_HighLimit", "AnalogInput_Unit" },
               values: new object[] { "AnalogInput", "Coal amount in furnace", 3, 1.0, true, 0.0, 100.0, "kg" });

            migrationBuilder.InsertData(
             table: "Alarms",
             columns: new[] { "Type", "Priority", "Limit", "AnalogInputId", "isMuted" },
             values: new object[] { 0, 0, 3, 1, true });

            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmsTriggers_AlarmId",
                table: "AlarmsTriggers",
                column: "AlarmId");

            migrationBuilder.CreateIndex(
                name: "IX_RealTimeUnits_AddressId",
                table: "RealTimeUnits",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TagBases_AddressId",
                table: "TagBases",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TagValues_TagBaseId",
                table: "TagValues",
                column: "TagBaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmsTriggers");

            migrationBuilder.DropTable(
                name: "RealTimeUnits");

            migrationBuilder.DropTable(
                name: "TagValues");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "TagBases");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}