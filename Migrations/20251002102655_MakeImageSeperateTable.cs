using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_tracker.Migrations
{
    /// <inheritdoc />
    public partial class MakeImageSeperateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "RecipeDetails");

            migrationBuilder.AddColumn<int>(
                name: "ImageID",
                table: "RecipeDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecipeImages",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Image = table.Column<byte[]>(type: "BLOB", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeImages", x => x.ImageID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeDetails_ImageID",
                table: "RecipeDetails",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageID",
                table: "RecipeDetails",
                column: "ImageID",
                principalTable: "RecipeImages",
                principalColumn: "ImageID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageID",
                table: "RecipeDetails");

            migrationBuilder.DropTable(
                name: "RecipeImages");

            migrationBuilder.DropIndex(
                name: "IX_RecipeDetails_ImageID",
                table: "RecipeDetails");

            migrationBuilder.DropColumn(
                name: "ImageID",
                table: "RecipeDetails");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "RecipeDetails",
                type: "BLOB",
                nullable: true);
        }
    }
}
