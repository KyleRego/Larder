export default class FoodFormDataMapper
{
    static map(formData)
    {
        const food = {};

        food.name = formData.get("name");
        food.description = formData.get("description");
        food.amount = formData.get("amount");
        food.unitId = formData.get("unitId");
        food.calories = formData.get("calories");

        return food;
    }
}