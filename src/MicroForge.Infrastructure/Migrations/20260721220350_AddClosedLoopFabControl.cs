using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClosedLoopFabControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToolQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductionLineId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecipeName = table.Column<string>(type: "TEXT", nullable: false),
                    IsQualified = table.Column<bool>(type: "INTEGER", nullable: false),
                    QualifiedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolQualifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolQualifications_ProductionLines_ProductionLineId",
                        column: x => x.ProductionLineId,
                        principalTable: "ProductionLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkInstructions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RouteStepId = table.Column<int>(type: "INTEGER", nullable: false),
                    TaskDescription = table.Column<string>(type: "TEXT", nullable: false),
                    IsMandatory = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkInstructions_RouteSteps_RouteStepId",
                        column: x => x.RouteStepId,
                        principalTable: "RouteSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotChecklistProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LotId = table.Column<int>(type: "INTEGER", nullable: false),
                    WorkInstructionId = table.Column<int>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CompletedByOperatorId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotChecklistProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotChecklistProgresses_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotChecklistProgresses_WorkInstructions_WorkInstructionId",
                        column: x => x.WorkInstructionId,
                        principalTable: "WorkInstructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotChecklistProgresses_LotId_WorkInstructionId",
                table: "LotChecklistProgresses",
                columns: new[] { "LotId", "WorkInstructionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotChecklistProgresses_WorkInstructionId",
                table: "LotChecklistProgresses",
                column: "WorkInstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolQualifications_ProductionLineId_RecipeName",
                table: "ToolQualifications",
                columns: new[] { "ProductionLineId", "RecipeName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkInstructions_RouteStepId",
                table: "WorkInstructions",
                column: "RouteStepId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotChecklistProgresses");

            migrationBuilder.DropTable(
                name: "ToolQualifications");

            migrationBuilder.DropTable(
                name: "WorkInstructions");
        }
    }
}
