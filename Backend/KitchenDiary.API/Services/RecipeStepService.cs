using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;
namespace KitchenDiary.API.Services;

public class RecipeStepService : IRecipeStepService
{
    private readonly ApplicationDbContext _context;
        private readonly ILogger<RecipeService> _logger;

    public RecipeStepService(ApplicationDbContext context, ILogger<RecipeService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecipeStepDto?> AddRecipeStepAsync(int recipeId, CreateRecipeStepDto recipeStepDto)
    {
        var recipe=await _context.Recipes.FindAsync(recipeId);
        if(recipe==null)
        {
            _logger.LogWarning("Recipe not found.");
            return null;
        }
        var recipeStep= new RecipeStep
        {
            StepNumber=recipeStepDto.StepNumber,
            Instruction=recipeStepDto.Instruction,
            RecipeId=recipeId
        };
        _context.RecipeSteps.Add(recipeStep);
        await _context.SaveChangesAsync();
         _logger.LogInformation("Recipe Steps for {RecipeId} added successfully.",
        recipe.Id);

        return new RecipeStepDto
        {
            Id=recipeStep.Id,
            StepNumber=recipeStep.StepNumber,
            Instruction=recipeStep.Instruction
        };

    }

    public async Task<bool> DeleteRecipeStepAsync(int stepId)
    {
        var step= await _context.RecipeSteps.FindAsync(stepId);
        if(step == null)
        {
            _logger.LogWarning("Step not found.");
            return false;
        }
        _context.RecipeSteps.Remove(step);
        await _context.SaveChangesAsync();
         _logger.LogInformation("Recipe Step {stepid} deleted successfully.",
        stepId);
        return true;
    }

    public async Task<RecipeStepDto?> UpdateRecipeStepAsync(int stepId, CreateRecipeStepDto recipeStepDto)
    {
        var step= await _context.RecipeSteps.FindAsync(stepId);
        if(step == null)
        {
            _logger.LogWarning("Step not found.");
            return null;
        }
        
            step.StepNumber=recipeStepDto.StepNumber;
            step.Instruction=recipeStepDto.Instruction;
           
        
        await _context.SaveChangesAsync();
         _logger.LogInformation("Recipe Step  {RecipeId} updated successfully.",
        stepId);
        return new RecipeStepDto
        {
            Id=step.Id,
            StepNumber=step.StepNumber,
            Instruction=step.Instruction
        };
    }

    
}