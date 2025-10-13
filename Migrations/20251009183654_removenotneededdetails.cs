using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_tracker.Migrations
{
    /// <inheritdoc />
    public partial class removenotneededdetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CookMins",
                table: "RecipeDetails");

            migrationBuilder.DropColumn(
                name: "PrepMins",
                table: "RecipeDetails");

            migrationBuilder.DropColumn(
                name: "Servings",
                table: "RecipeDetails");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "RecipeDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CookMins",
                table: "RecipeDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PrepMins",
                table: "RecipeDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Servings",
                table: "RecipeDetails",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "RecipeDetails",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
