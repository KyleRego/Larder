using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Larder.Services.Impl;

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

    private static readonly List<UnitDto> _demoUnits = [
        new() { Name="mg", Type=UnitType.Mass },
        new() { Name="g", Type=UnitType.Mass},
        new() { Name="ml", Type=UnitType.Volume},
        new() { Name="cups", Type=UnitType.Volume}
    ];

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

        await _unitService.CreateUnits(_demoUnits);
    }
}
