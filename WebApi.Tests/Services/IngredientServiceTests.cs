using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Larder.Services;

namespace Larder.Tests.Services;

public class IngredientServiceTests : ServiceTestsBase
{
    [Fact]
    public async void UpdateIngredientThrowsIfMissingId()
    {
        var ingredientRepository = new Mock<IIngredientRepository>();

        IngredientService service = new(mSP.Object, ingredientRepository.Object);

        IngredientDto ingredient = new()
        {
            Name = "Eggs",
            Quantity = new()
            {
                Amount = 5
            }
        };

        await Assert.ThrowsAsync<ApplicationException>(
            async () => await service.UpdateIngredient(ingredient));
    }

    [Fact]
    public async void UpdateIngredientThrowsIfIngredientNotFound()
    {
        var ingredientRepository = new Mock<IIngredientRepository>();

        string id = "made_up_id";
        ingredientRepository.Setup(ir => ir.Get(mockUserId, id)).ReturnsAsync((Item?)null);

        IngredientService service = new(mSP.Object, ingredientRepository.Object);

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
