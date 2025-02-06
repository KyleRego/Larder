using Larder.Dtos;
using Larder.Models;
using Larder.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace Larder.Services.Impl;

public class DemoService(   UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IUnitService unitService,
                            IUnitConversionService unitConversionService,
                            IItemService itemService)
                                                        : IDemoService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager
                                                            = signInManager;
    private readonly IUnitService _unitService = unitService;
    private readonly IUnitConversionService _unitConversionService
                                                    = unitConversionService;
    private readonly IItemService _itemService = itemService;

    private static readonly List<UnitDto> _demoUnits = [
        new() { Name="mg", Type=UnitType.Mass },
        new() { Name="g", Type=UnitType.Mass},
        new() { Name="ml", Type=UnitType.Volume},
        new() { Name="cups", Type=UnitType.Volume},
        new() { Name="tablespoons", Type=UnitType.Volume },
        new() { Name="half butter sticks", Type=UnitType.Volume}
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
        UnitDto halfSticks = demoUnits.First(u => u.Name == "half butter sticks");

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

        ItemDto butter = new()
        {
            Name = "Butter",
            Description = "",
            Quantity = new() { Amount = 3, UnitId = halfSticks.Id },
            Nutrition = new()
            {
                ServingSize = new() { Amount = 1, UnitId = tablespoons.Id },
                Calories = 100,
                GramsTotalFat = 11,
                GramsSaturatedFat = 7,
                MilligramsCholesterol = 30,
                MilligramsSodium = 90,
                GramsTotalCarbs = 0,
                GramsProtein = 0
            }
        };

        ItemDto riceBox = new()
        {
            Name = "Rice Roni chicken (family size)",
            Description = "Family size",
            Quantity = QuantityDto.One(),
            Nutrition = new()
            {
                ServingSize = new() { Amount = 56, UnitId = grams.Id},
                Calories = 190,
                GramsTotalFat = 5,
                MilligramsSodium = 730,
                GramsTotalCarbs = 41,
                GramsDietaryFiber = 1,
                GramsTotalSugars = 1,
                GramsProtein = 5
            }
        };

        ItemDto apples = new()
        {
            Name = "Apples",
            Description = "Crunchy red fruit",
            Quantity = QuantityDto.Scalar(5),
            Nutrition = new()
            {
                ServingSize = QuantityDto.One(),
                Calories = 95,
                GramsProtein = 0.5,
                GramsTotalFat = 0.3,
                GramsSaturatedFat = 0.1,
                GramsTotalCarbs = 25,
                GramsDietaryFiber = 4.4,
                GramsTotalSugars = 19,
                MilligramsCholesterol = 0,
                MilligramsSodium = 2
            }
        };

        await _itemService.CreateItems([butter, riceBox, apples]);
    }
}
