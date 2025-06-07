using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Revalb.Migrations
{
    /// <inheritdoc />
    public partial class AlbumTableUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zanr",
                table: "Albumi");

            migrationBuilder.AlterColumn<string>(
                name: "IdArtist",
                table: "Albumi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdArtist",
                table: "Albumi",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Zanr",
                table: "Albumi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
