using Larder.Models;

namespace Larder.Dtos;

public static class IngredientDtoAssembler
{
    public static IngredientDto Assemble(Ingredient ingredient)
    {
        IngredientDto ingredientDto = new()
        {
            IngredientId = ingredient.Id,
            Name = ingredient.Name
        };

        return ingredientDto;
    }
}