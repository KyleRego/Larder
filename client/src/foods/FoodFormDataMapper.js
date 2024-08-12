import FoodConstants from "./FoodConstants";

export default class FoodFormDataMapper
{
    static map(formData)
    {
        const food = {};

        food.name = formData.get("name");
        food.description = formData.get("description");

        food.amount = formData.get("amount");
        food.calories = formData.get("calories");

        const quantityProperties = FoodConstants.quantityProperties;

        for (let i = 0; i < quantityProperties.length; i += 1)
        {
            const propertyName = quantityProperties[i];

            const amount = formData.get(`${propertyName}_amount`);
            const unitId = formData.get(`${propertyName}_unitId`);

            if (amount !== "")
            {
                food[propertyName] = {};
                food[propertyName]["amount"] = amount;
                food[propertyName]["unitId"] = unitId;
            }
        }

        return food;
    }
}