import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

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

    let ingredientListItems = recipe.ingredients.map(ri => IngredientListItem(ri));

    return (<>
        <h1>{recipe.recipeName}</h1>

        <h2>Ingredients</h2>

        <ul>
            {ingredientListItems}
        </ul>

        <Link to={`/recipes/${id}/edit`}>Edit recipe</Link>

    </>)
}

function IngredientListItem(recipeIngredient)
{
    return <li key={recipeIngredient.ingredientId}>
        {recipeIngredient.amount} {recipeIngredient.unitName} {recipeIngredient.ingredientName} 
    </li>
}