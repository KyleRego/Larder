export default class recipeFormDataMapper
{
    static map(formData)
    {
        const recipeData = {};
        const ingredientsData = [];

        formData.entries().forEach(entry => {
            const key = entry[0];
            const val = entry[1];

            if (key === "recipeName")
            {
                recipeData["name"] = val;
            }
            else if (key.startsWith("ingredient"))
            {
                const index = key.substring(key.length - 1);

                if (!ingredientsData[index])
                {
                    ingredientsData[index] = {};
                }

                const restOfKey = key.substring("ingredient".length)

                if (restOfKey.startsWith("Name"))
                {
                    ingredientsData[index]["ingredientName"] = val;
                }
                else if (restOfKey.startsWith("Amount"))
                {
                    ingredientsData[index]["amount"] = val;
                }
                else if (restOfKey.startsWith("Unit"))
                {
                    ingredientsData[index]["unitId"] = val;
                }
            }
        });

        recipeData["ingredients"] = ingredientsData;

        return recipeData;
    }
}