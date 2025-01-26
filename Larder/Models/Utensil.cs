namespace Larder.Models;

/// <summary>
/// A non-food item for cooking
/// </summary>
public class Utensil(string userId, string name, string? description)
                                    : Item(userId, name,  description) { }
// TODO: This probably needs to be the join table between
// 