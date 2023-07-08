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
                name: "Adresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.Id);
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
                    AdressId = table.Column<int>(type: "INTEGER", nullable: false),
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
                        name: "FK_AnalogInputs_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
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
                    AdressId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialValue = table.Column<double>(type: "REAL", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnalogOutputs_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
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
                    AdressId = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanTime = table.Column<double>(type: "REAL", nullable: false),
                    IsScanning = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalInputs_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
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
                    AdressId = table.Column<int>(type: "INTEGER", nullable: false),
                    InitialValue = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DigitalOutputs_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
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
                    AdressId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealTimeUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealTimeUnits_Adresses_AdressId",
                        column: x => x.AdressId,
                        principalTable: "Adresses",
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

            // Add user user
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "Password", "Type" },
                values: new object[] { "user", "user", 1 });
            migrationBuilder.CreateIndex(
                name: "IX_Alarms_AnalogInputId",
                table: "Alarms",
                column: "AnalogInputId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogInputs_AdressId",
                table: "AnalogInputs",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalogOutputs_AdressId",
                table: "AnalogOutputs",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalInputs_AdressId",
                table: "DigitalInputs",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_DigitalOutputs_AdressId",
                table: "DigitalOutputs",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_RealTimeUnits_AdressId",
                table: "RealTimeUnits",
                column: "AdressId");
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
                name: "Adresses");
        }
    }
}
