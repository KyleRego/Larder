using System.Runtime;
using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Moq;

namespace Larder.Services.Tests;

public class IngredientServiceTests
{
    [Fact]
    public async void UpdateIngredientThrowsIfMissingId()
    {
        var ingredientRepository = new Mock<IIngredientRepository>();

        IngredientService service = new(ingredientRepository.Object);

        IngredientDto ingredient = new()
        {
            Name = "Eggs",
            Quantity = new()
            {
                Amount = 5
            }
        };

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateIngredient(ingredient));
    }

    [Fact]
    public async void UpdateIngredientThrowsIfIngredientNotFound()
    {
        var ingredientRepository = new Mock<IIngredientRepository>();

        string id = "made_up_id";
        ingredientRepository.Setup(ir => ir.Get(id)).ReturnsAsync((Ingredient?)null);

        IngredientService service = new(ingredientRepository.Object);

        IngredientDto ingredient = new()
        {
            Id = id,
            Name = "Eggs",
            Quantity = new()
            {
                Amount = 5
            }
        };

        await Assert.ThrowsAsync<ApplicationException>(async () => await service.UpdateIngredient(ingredient)); 
    }
}