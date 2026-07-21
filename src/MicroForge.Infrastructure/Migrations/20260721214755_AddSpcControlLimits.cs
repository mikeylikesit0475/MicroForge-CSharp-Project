using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroForge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpcControlLimits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Lcl",
                table: "SensorReadings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Mean",
                table: "SensorReadings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Ucl",
                table: "SensorReadings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lcl",
                table: "SensorReadings");

            migrationBuilder.DropColumn(
                name: "Mean",
                table: "SensorReadings");

            migrationBuilder.DropColumn(
                name: "Ucl",
                table: "SensorReadings");
        }
    }
}
