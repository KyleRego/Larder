using Larder.Dtos;
using Larder.Models;
using Microsoft.AspNetCore.Identity;

namespace Larder.Services;

public interface IDemoService
{
    public Task CreateDemo();
}

public class DemoService(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IFoodService foodService,
                        IRecipeService recipeService,
                        IUnitService unitService) : IDemoService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IFoodService _foodService = foodService;
    private readonly IRecipeService _recipeService = recipeService;
    private readonly IUnitService _unitService = unitService;

    public async Task CreateDemo()
    {
        string userName = $"demo-user-{Guid.NewGuid()}@larder.lol";

        ApplicationUser demoUser = new()
        {
            UserName = userName
        };

        IdentityResult result = await _userManager.CreateAsync(demoUser);

        if (!result.Succeeded)
        {
            throw new ApplicationException("Unable to create the demo user");
        }

        await _signInManager.SignInAsync(demoUser, false);

        // FoodDto food1 = new()
        // {
        //     Name = "Bananas",
        //     Servings = 5,
        //     Calories = 100,
        //     GramsProtein = 1,
        //     GramsTotalSugars = 15,
        //     GramsTotalCarbs = 28,
        //     GramsTotalFat = 0,
        //     GramsDietaryFiber = 3
        // };

        // FoodDto food2 = new()
        // {
        //     Name = "Oatmeal",
        //     Servings = 17,
        //     Calories = 110,
        //     GramsTotalFat = 2,
        //     GramsSaturatedFat = 0,
        //     GramsTransFat = 0,
        //     MilligramsCholesterol = 0,
        //     MilligramsSodium = 120,
        //     GramsTotalCarbs = 22,
        //     GramsDietaryFiber = 2,
        //     GramsTotalSugars = 8,
        //     GramsProtein = 3
        // };

        // FoodDto food3 = new()
        // {
        //     Name = "Milk",
        //     Servings = 8,
        //     Calories = 100,
        //     MilligramsSodium = 107,
        //     GramsTotalCarbs = 12,
        //     GramsTotalSugars = 12,
        //     GramsProtein = 8,
        //     MilligramsCholesterol = 10
        // };

        // await _foodService.CreateFood(food1);
        // await _foodService.CreateFood(food2);
        // await _foodService.CreateFood(food3);
    }
}
