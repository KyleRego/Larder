import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import RecipesService from "../services/RecipesService";
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
        });
    }, [id]);

    function handleDelete()
    {
        // if (window.confirm(`Are you sure you want to delete this recipe - ${recipe.name}?`))
        // {
            const service = new RecipesService();
            service.deleteRecipe(recipe.id);
        // }
    }

    if (recipe === null) return <h1>Loading...</h1>

    console.log(recipe);

    let ingredientListItems = recipe.ingredients.map(ri => IngredientListItem(ri));

    return <>
        <h1>{recipe.name}</h1>

        <button className="btn btn-danger" onClick={handleDelete}>Delete recipe</button>

        <h2>Ingredients</h2>

        <ul>
            {ingredientListItems}
        </ul>

        <div>
            <Link to={`/recipes/${id}/edit`}>Edit recipe</Link>
        </div>
        
        <Link to="/recipes">Back to recipes</Link>
    </>;
}

function IngredientListItem(recipeIngredient)
{
    return <li key={recipeIngredient.Id}>
        {recipeIngredient.quantity.amount} {recipeIngredient.name} 
    </li>
}
