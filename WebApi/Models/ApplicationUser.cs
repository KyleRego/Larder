using Microsoft.AspNetCore.Identity;

namespace Larder.Models;

public class ApplicationUser : IdentityUser
{
    public List<Food> Foods { get; set; } = [];
    public List<Ingredient> Ingredients { get; set; } = [];
    public List<Recipe> Recipes { get; set; } = [];
    public List<Unit> Units { get; set; } = [];
}
