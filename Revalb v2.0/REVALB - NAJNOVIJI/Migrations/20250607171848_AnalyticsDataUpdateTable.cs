using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVALB.Migrations
{
    /// <inheritdoc />
    public partial class AnalyticsDataUpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AveragingRating",
                table: "AnalyticsData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AveragingRating",
                table: "AnalyticsData");
        }
    }
}
