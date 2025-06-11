using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace REVALB.Migrations
{
    /// <inheritdoc />
    public partial class AddAlbumCategoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumCategory_Albums_AlbumsId",
                table: "AlbumCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumCategory_Categories_CategoriesId",
                table: "AlbumCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumCategory",
                table: "AlbumCategory");

            migrationBuilder.RenameTable(
                name: "AlbumCategory",
                newName: "AlbumCategories");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumCategory_CategoriesId",
                table: "AlbumCategories",
                newName: "IX_AlbumCategories_CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumCategories",
                table: "AlbumCategories",
                columns: new[] { "AlbumsId", "CategoriesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumCategories_Albums_AlbumsId",
                table: "AlbumCategories",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumCategories_Categories_CategoriesId",
                table: "AlbumCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlbumCategories_Albums_AlbumsId",
                table: "AlbumCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AlbumCategories_Categories_CategoriesId",
                table: "AlbumCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AlbumCategories",
                table: "AlbumCategories");

            migrationBuilder.RenameTable(
                name: "AlbumCategories",
                newName: "AlbumCategory");

            migrationBuilder.RenameIndex(
                name: "IX_AlbumCategories_CategoriesId",
                table: "AlbumCategory",
                newName: "IX_AlbumCategory_CategoriesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlbumCategory",
                table: "AlbumCategory",
                columns: new[] { "AlbumsId", "CategoriesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumCategory_Albums_AlbumsId",
                table: "AlbumCategory",
                column: "AlbumsId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AlbumCategory_Categories_CategoriesId",
                table: "AlbumCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
