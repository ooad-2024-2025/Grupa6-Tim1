using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVALB.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameOfAnalitika : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AveragingRating",
                table: "AnalyticsData",
                newName: "AverageRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AverageRating",
                table: "AnalyticsData",
                newName: "AveragingRating");
        }
    }
}
