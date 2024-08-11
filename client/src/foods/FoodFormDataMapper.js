import FoodConstants from "./FoodConstants";

export default class FoodFormDataMapper
{
    static map(formData)
    {
        const food = {};

        food.name = formData.get("name");
        food.description = formData.get("description");

        food.quantity = {};
        food.quantity.amount = formData.get("amount");
        food.quantity.unitId = formData.get("unitId");

        food.calories = formData.get("calories");

        const quantityProperties = FoodConstants.quantityProperties;

        for (let i = 0; i < quantityProperties.length; i += 1)
        {
            const propertyName = quantityProperties[i];

            food[propertyName] = {};
            food[propertyName]["amount"] = formData.get(`${propertyName}_amount`);
            food[propertyName]["unitId"] = formData.get(`${propertyName}_unitId`);
        }

        return food;
    }
}