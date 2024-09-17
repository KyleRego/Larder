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
        ApplicationUser demoUser = new()
        {
            UserName = "DemoUser@larder.lol"
        };

        await _userManager.CreateAsync(demoUser);

        await _signInManager.SignInAsync(demoUser, false);
    }
}
