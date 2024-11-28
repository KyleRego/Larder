using Microsoft.AspNetCore.Identity;

namespace Larder.Models;

public class ApplicationUser : IdentityUser
{
    public List<Recipe> Recipes { get; set; } = [];
    public List<Unit> Units { get; set; } = [];
    public List<Item> Items { get; set; } = [];
}
