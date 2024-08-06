export default class FoodFormDataMapper
{
    static map(formData)
    {
        const food = {};
        food.quantity = {};

        food.name = formData.get("name");
        food.description = formData.get("description");
        food.quantity.amount = formData.get("quantity");
        food.calories = formData.get("calories");

        return food;
    }
}