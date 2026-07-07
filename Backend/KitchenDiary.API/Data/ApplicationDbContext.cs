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

        modelBuilder.Entity<Ingredient>()
        .HasOne(i => i.Recipe)
        .WithMany(r => r.Ingredients)
        .HasForeignKey(i => i.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeStep>()
        .HasOne(s => s.Recipe)
        .WithMany(r => r.Steps)
        .HasForeignKey(s => s.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeImage>()
        .HasOne(i => i.Recipe)
        .WithMany(r => r.Images)
        .HasForeignKey(i => i.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<RecipeTag>()
        .HasOne(rt => rt.Recipe)
        .WithMany(r => r.RecipeTags)
        .HasForeignKey(rt => rt.RecipeId)
        .OnDelete(DeleteBehavior.Cascade);
       
    }
}