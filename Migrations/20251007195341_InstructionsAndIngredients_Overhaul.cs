using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace recipe_tracker.Migrations
{
    /// <inheritdoc />
    public partial class InstructionsAndIngredients_Overhaul : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeID",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Recipes_RecipeID",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageID",
                table: "RecipeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeDetails_RecipeDetailsID",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeDetailsID",
                table: "Recipes",
                newName: "RecipeDetailsId");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "Recipes",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeDetailsID",
                table: "Recipes",
                newName: "IX_Recipes_RecipeDetailsId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "RecipeImages",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "ImageID",
                table: "RecipeDetails",
                newName: "ImageId");

            migrationBuilder.RenameColumn(
                name: "RecipeDetailsID",
                table: "RecipeDetails",
                newName: "RecipeDetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDetails_ImageID",
                table: "RecipeDetails",
                newName: "IX_RecipeDetails_ImageId");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "Instructions",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "InstructionID",
                table: "Instructions",
                newName: "InstructionId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructions_RecipeID",
                table: "Instructions",
                newName: "IX_Instructions_RecipeId");

            migrationBuilder.RenameColumn(
                name: "RecipeID",
                table: "Ingredients",
                newName: "RecipeId");

            migrationBuilder.RenameColumn(
                name: "IngredientID",
                table: "Ingredients",
                newName: "IngredientId");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_RecipeID",
                table: "Ingredients",
                newName: "IX_Ingredients_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Recipes_RecipeId",
                table: "Instructions",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageId",
                table: "RecipeDetails",
                column: "ImageId",
                principalTable: "RecipeImages",
                principalColumn: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeDetails_RecipeDetailsId",
                table: "Recipes",
                column: "RecipeDetailsId",
                principalTable: "RecipeDetails",
                principalColumn: "RecipeDetailsId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_Recipes_RecipeId",
                table: "Instructions");

            migrationBuilder.DropForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageId",
                table: "RecipeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_RecipeDetails_RecipeDetailsId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "RecipeDetailsId",
                table: "Recipes",
                newName: "RecipeDetailsID");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Recipes",
                newName: "RecipeID");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_RecipeDetailsId",
                table: "Recipes",
                newName: "IX_Recipes_RecipeDetailsID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "RecipeImages",
                newName: "ImageID");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "RecipeDetails",
                newName: "ImageID");

            migrationBuilder.RenameColumn(
                name: "RecipeDetailsId",
                table: "RecipeDetails",
                newName: "RecipeDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_RecipeDetails_ImageId",
                table: "RecipeDetails",
                newName: "IX_RecipeDetails_ImageID");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Instructions",
                newName: "RecipeID");

            migrationBuilder.RenameColumn(
                name: "InstructionId",
                table: "Instructions",
                newName: "InstructionID");

            migrationBuilder.RenameIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                newName: "IX_Instructions_RecipeID");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "Ingredients",
                newName: "RecipeID");

            migrationBuilder.RenameColumn(
                name: "IngredientId",
                table: "Ingredients",
                newName: "IngredientID");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                newName: "IX_Ingredients_RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeID",
                table: "Ingredients",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_Recipes_RecipeID",
                table: "Instructions",
                column: "RecipeID",
                principalTable: "Recipes",
                principalColumn: "RecipeID");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeDetails_RecipeImages_ImageID",
                table: "RecipeDetails",
                column: "ImageID",
                principalTable: "RecipeImages",
                principalColumn: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_RecipeDetails_RecipeDetailsID",
                table: "Recipes",
                column: "RecipeDetailsID",
                principalTable: "RecipeDetails",
                principalColumn: "RecipeDetailsID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
