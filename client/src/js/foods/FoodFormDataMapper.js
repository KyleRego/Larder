export default class FoodFormDataMapper
{
    static map(formData)
    {
        const food = {};

        food.name = formData.get("name");
        food.description = formData.get("description");
        food.servings = formData.get("servings");
        food.calories = formData.get("calories");
        food.gramsProtein = formData.get("gramsProtein");
        food.gramsSaturatedFat = formData.get("gramsSaturatedFat")
        food.gramsTransFat = formData.get("gramsTransFat");
        food.milligramsCholesterol = formData.get("milligramsCholesterol");
        food.milligramsSodium = formData.get("milligramsSodium");
        food.gramsTotalCarbs = formData.get("gramsTotalCarbs");
        food.gramsDietaryFiber = formData.get("gramsDietaryFiber");
        food.gramsTotalSugars = formData.get("gramsTotalSugars");

        return food;
    }
}
