using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KitchenDiary.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCoverImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCoverImage",
                table: "RecipeImages",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoverImage",
                table: "RecipeImages");
        }
    }
}
