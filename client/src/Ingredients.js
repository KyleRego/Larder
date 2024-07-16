import { useState, useEffect } from "react";

import IngredientsService from "./services/IngredientsService";

export default function Ingredients()
{
    const [ingredients, setIngredients] = useState(null);

    useEffect(() =>
    {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients().then(result => {
            setIngredients(result);
        });
    }, []);

    if (ingredients === null) return <h1>Loading...</h1>;

    const ingredientListItems = ingredients.map(ingredient => {
        return <li key={ingredient.id}>
            {ingredient.name}
        </li>
    })

    return (
        <>
            <h1>Ingredients</h1>

            <ol>
                {ingredientListItems}
            </ol>
        </>
    )
}