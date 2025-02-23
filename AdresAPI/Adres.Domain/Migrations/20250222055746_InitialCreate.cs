using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adres.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUnits",
                columns: table => new
                {
                    AdminUnitID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUnits", x => x.AdminUnitID);
                });

            migrationBuilder.CreateTable(
                name: "AssetServiceTypes",
                columns: table => new
                {
                    AssetServiceTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetServiceTypes", x => x.AssetServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ReferenceCode = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderID);
                });

            migrationBuilder.CreateTable(
                name: "Adquisitions",
                columns: table => new
                {
                    AdquisitionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdminUnitID = table.Column<int>(type: "INTEGER", nullable: false),
                    AssetServiceTypeID = table.Column<int>(type: "INTEGER", nullable: false),
                    ProviderID = table.Column<int>(type: "INTEGER", nullable: false),
                    Budget = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    AdquisitionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Documentation = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adquisitions", x => x.AdquisitionID);
                    table.ForeignKey(
                        name: "FK_Adquisitions_AdminUnits_AdminUnitID",
                        column: x => x.AdminUnitID,
                        principalTable: "AdminUnits",
                        principalColumn: "AdminUnitID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisitions_AssetServiceTypes_AssetServiceTypeID",
                        column: x => x.AssetServiceTypeID,
                        principalTable: "AssetServiceTypes",
                        principalColumn: "AssetServiceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adquisitions_Providers_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Providers",
                        principalColumn: "ProviderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdquisitionHistories",
                columns: table => new
                {
                    AdquisitionHistoryID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdquisitionID = table.Column<int>(type: "INTEGER", nullable: false),
                    Operation = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Model = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdquisitionHistories", x => x.AdquisitionHistoryID);
                    table.ForeignKey(
                        name: "FK_AdquisitionHistories_Adquisitions_AdquisitionID",
                        column: x => x.AdquisitionID,
                        principalTable: "Adquisitions",
                        principalColumn: "AdquisitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdquisitionHistories_AdquisitionID",
                table: "AdquisitionHistories",
                column: "AdquisitionID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisitions_AdminUnitID",
                table: "Adquisitions",
                column: "AdminUnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisitions_AssetServiceTypeID",
                table: "Adquisitions",
                column: "AssetServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Adquisitions_ProviderID",
                table: "Adquisitions",
                column: "ProviderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdquisitionHistories");

            migrationBuilder.DropTable(
                name: "Adquisitions");

            migrationBuilder.DropTable(
                name: "AdminUnits");

            migrationBuilder.DropTable(
                name: "AssetServiceTypes");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
