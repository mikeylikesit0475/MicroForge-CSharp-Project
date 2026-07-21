using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFabExpansionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClassifiedType",
                table: "DefectRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "CoordinateX",
                table: "DefectRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CoordinateY",
                table: "DefectRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "CapaActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LotId = table.Column<int>(type: "INTEGER", nullable: true),
                    ActionDescription = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedTo = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapaActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EcoRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeName = table.Column<string>(type: "TEXT", nullable: false),
                    ChangeDetail = table.Column<string>(type: "TEXT", nullable: false),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcoRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeVersions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RecipeName = table.Column<string>(type: "TEXT", nullable: false),
                    VersionNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    ParameterPayloadXml = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShiftLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ShiftName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SummaryNotes = table.Column<string>(type: "TEXT", nullable: false),
                    ActiveAlertsSummary = table.Column<string>(type: "TEXT", nullable: false),
                    HandoverCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompletedBy = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToolMaintenanceLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductionLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolStatus = table.Column<string>(type: "TEXT", nullable: false),
                    CumulativeRuntimeHours = table.Column<double>(type: "REAL", nullable: false),
                    HoursSinceLastPM = table.Column<double>(type: "REAL", nullable: false),
                    NextScheduledPM = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolMaintenanceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolMaintenanceLogs_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapaActions_LotId",
                table: "CapaActions",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_EcoRecords_RecipeName",
                table: "EcoRecords",
                column: "RecipeName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeVersions_RecipeName_VersionNumber",
                table: "RecipeVersions",
                columns: new[] { "RecipeName", "VersionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToolMaintenanceLogs_ProductionLineId",
                table: "ToolMaintenanceLogs",
                column: "ProductionLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CapaActions");

            migrationBuilder.DropTable(
                name: "EcoRecords");

            migrationBuilder.DropTable(
                name: "RecipeVersions");

            migrationBuilder.DropTable(
                name: "ShiftLogs");

            migrationBuilder.DropTable(
                name: "ToolMaintenanceLogs");

            migrationBuilder.DropColumn(
                name: "ClassifiedType",
                table: "DefectRecords");

            migrationBuilder.DropColumn(
                name: "CoordinateX",
                table: "DefectRecords");

            migrationBuilder.DropColumn(
                name: "CoordinateY",
                table: "DefectRecords");
        }
    }
}
