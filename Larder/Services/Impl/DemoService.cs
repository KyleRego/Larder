using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Larder.Services.Impl;

public class DemoService(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager,
                        IFoodService foodService,
                        IRecipeService recipeService,
                        IUnitService unitService,
                        IUnitConversionService unitConversionService)
                                                        : IDemoService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager
                                                            = signInManager;
    private readonly IFoodService _foodService = foodService;
    private readonly IRecipeService _recipeService = recipeService;
    private readonly IUnitService _unitService = unitService;
    private readonly IUnitConversionService _unitConversionService
                                                    = unitConversionService;

    private static readonly List<UnitDto> _demoUnits = [
        new() { Name="mg", Type=UnitType.Mass },
        new() { Name="g", Type=UnitType.Mass},
        new() { Name="ml", Type=UnitType.Volume},
        new() { Name="cups", Type=UnitType.Volume},
        new() { Name="tablespoons", Type=UnitType.Volume }
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

        IEnumerable<UnitDto> demoUnits = await _unitService.CreateUnits(_demoUnits);

        UnitDto grams = demoUnits.First(u => u.Name == "g");
        UnitDto milligrams = demoUnits.First(u => u.Name == "mg");
        UnitDto cups = demoUnits.First(u => u.Name == "cups");
        UnitDto tablespoons = demoUnits.First(u => u.Name == "tablespoons");

        UnitConversionDto gramsConversion = new()
        {
            UnitId = grams.Id!,
            TargetUnitId = milligrams.Id!,
            TargetUnitsPerUnit = 1000
        };

        UnitConversionDto cupsConversion = new()
        {
            UnitId = cups.Id!,
            TargetUnitId = tablespoons.Id!,
            TargetUnitsPerUnit = 16
        };

        await _unitConversionService.CreateUnitConversion(gramsConversion);
        await _unitConversionService.CreateUnitConversion(cupsConversion);
    }
}
