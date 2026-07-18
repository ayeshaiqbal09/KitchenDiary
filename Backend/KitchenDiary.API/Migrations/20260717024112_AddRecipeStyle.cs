using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenDiary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRecipeStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipeStyle",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipeStyle",
                table: "Recipes");
        }
    }
}
