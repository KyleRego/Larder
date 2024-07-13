import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

import RecipesService from "./services/RecipesService";
import "./Recipe.css"

export default function Recipe()
{
    let { id } = useParams();
    const [recipe, setRecipe] = useState(null);

    useEffect(() =>
    {
        const service = new RecipesService();

        service.getRecipe(id).then(result => {
            setRecipe(result);
        })
    }, [id]);

    if (recipe === null) return <h1>Loading...</h1>

    let ingredientListItems = recipe.recipeIngredients.map(ri => IngredientListItem(ri));

    return (<>
        <h1>{recipe.name}</h1>

        <h2>Ingredients</h2>

        <ul>
            {ingredientListItems}
        </ul>

    </>)
}

function IngredientListItem(data)
{
    return <li key={data.id}>
        {data.amount} {data.unit.name} {data.ingredient.name} 
    </li>
}