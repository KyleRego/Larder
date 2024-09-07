import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import RecipesService from "../services/RecipesService";
import "./Recipe.css"
import findUnitName from "../helpers/findUnitName";

export default function Recipe({units})
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

    function handleDelete() {
        if (window.confirm(`Are you sure you want to delete this recipe - ${recipe.name}?`)) {
            const service = new RecipesService();
            service.deleteRecipe(recipe.id);
        }
    }

    if (recipe === null) return <h1>Loading...</h1>;

    let ingredientListItems = recipe.ingredients.map(ri =>
        <IngredientListItem key={ri.id} ingredient={ri} units={units} />);

    return <>
        <div className="card my-4">
            <div className="card-body">
                <div className="d-flex align-items-center justify-content-between">
                    <div className="d-flex align-items-center column-gap-3">
                        <h1 className="m-0">{recipe.name}</h1>

                        <Link className="btn btn-primary" to={`/recipes/${id}/edit`}>Edit</Link>
                    </div>

                    <button className="btn btn-danger" onClick={handleDelete}>Delete</button>
                </div>

                <h2>Ingredients:</h2>

                <ul className="list-group">
                    {ingredientListItems}
                </ul>
            </div>
        </div>

        <Link to="/recipes">Back to recipes</Link>
    </>;
}

function IngredientListItem({ingredient, units})
{
    const unitName = findUnitName(ingredient.quantity.unitId, units);

    return <li className="list-group-item">
        {ingredient.quantity.amount} {unitName} {ingredient.name} 
    </li>;
}
