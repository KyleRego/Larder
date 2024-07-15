using Larder.Dtos;

namespace Larder.Services;

public interface IRecipeService
{
    public RecipeDto Update(RecipeDto recipeDto);
}

public class RecipeService : IRecipeService
{
    public RecipeDto Update(RecipeDto recipeDto)
    {
        return recipeDto;
        // TODO: This will need to interact with the repositories
        // to create, update entities
    }
}