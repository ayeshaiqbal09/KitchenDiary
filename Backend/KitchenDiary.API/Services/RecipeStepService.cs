using KitchenDiary.API.Data;
using KitchenDiary.API.DTOs;
using KitchenDiary.API.Interfaces;
using KitchenDiary.API.Models;
using Microsoft.EntityFrameworkCore;
namespace KitchenDiary.API.Services;

public class RecipeStepService : IRecipeStepService
{
    private readonly ApplicationDbContext _context;

    public RecipeStepService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RecipeStepDto?> AddRecipeStepAsync(int recipeId, CreateRecipeStepDto recipeStepDto)
    {
        var recipe=await _context.Recipes.FindAsync(recipeId);
        if(recipe==null)
        {
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
            return false;
        }
        _context.RecipeSteps.Remove(step);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<RecipeStepDto?> UpdateRecipeStepAsync(int stepId, CreateRecipeStepDto recipeStepDto)
    {
        var step= await _context.RecipeSteps.FindAsync(stepId);
        if(step == null)
        {
            return null;
        }
        
            step.StepNumber=recipeStepDto.StepNumber;
            step.Instruction=recipeStepDto.Instruction;
           
        
        await _context.SaveChangesAsync();
        return new RecipeStepDto
        {
            Id=step.Id,
            StepNumber=step.StepNumber,
            Instruction=step.Instruction
        };
    }

    
}