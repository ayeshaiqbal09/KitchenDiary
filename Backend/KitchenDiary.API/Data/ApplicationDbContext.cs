using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KitchenDiary.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();

    public DbSet<Ingredient> Ingredients => Set<Ingredient>();

    public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();

    public DbSet<RecipeImage> RecipeImages => Set<RecipeImage>();

    public DbSet<Tag> Tags => Set<Tag>();

    public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RecipeTag>()
            .HasKey(rt => new { rt.RecipeId, rt.TagId });
    }
}