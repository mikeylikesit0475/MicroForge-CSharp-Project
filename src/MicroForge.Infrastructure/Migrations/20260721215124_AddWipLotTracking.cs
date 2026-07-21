using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddWipLotTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StepName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    SequenceOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteSteps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LotNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ActiveRouteStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lots_RouteSteps_ActiveRouteStepId",
                        column: x => x.ActiveRouteStepId,
                        principalTable: "RouteSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotMovementLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LotId = table.Column<int>(type: "INTEGER", nullable: false),
                    RouteStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    EnteredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExitedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OperatorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotMovementLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotMovementLogs_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotMovementLogs_RouteSteps_RouteStepId",
                        column: x => x.RouteStepId,
                        principalTable: "RouteSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Wafers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WaferId = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LotId = table.Column<int>(type: "INTEGER", nullable: false),
                    SlotPosition = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wafers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wafers_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotMovementLogs_LotId",
                table: "LotMovementLogs",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_LotMovementLogs_RouteStepId",
                table: "LotMovementLogs",
                column: "RouteStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_ActiveRouteStepId",
                table: "Lots",
                column: "ActiveRouteStepId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LotNumber",
                table: "Lots",
                column: "LotNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteSteps_StepName",
                table: "RouteSteps",
                column: "StepName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wafers_LotId",
                table: "Wafers",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_Wafers_WaferId",
                table: "Wafers",
                column: "WaferId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotMovementLogs");

            migrationBuilder.DropTable(
                name: "Wafers");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "RouteSteps");
        }
    }
}
