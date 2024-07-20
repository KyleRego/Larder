import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import IngredientsService from "../services/IngredientsService";

export default function Ingredient()
{
    let { id } = useParams();
    const [ingredient, setIngredient] = useState(null);

    useEffect(() =>
    {
        const service = new IngredientsService();

        service.getIngredient(id).then(result => {
            setIngredient(result);
        });
    }, [id]);

    if (ingredient === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>
                {ingredient.name}
            </h1>

            Stock: {ingredient.quantity} {ingredient.unitName}

            <IngredientDetails ingredient={ingredient} />

            <div>
                <Link to={`/ingredients/${ingredient.id}/edit`}>Edit ingredient</Link>
            </div>

            <Link to="/ingredients">Back to ingredients</Link>
        </>
    )
}

function IngredientDetails({ingredient})
{
    const recipes = ingredient.recipes

    if (recipes.length === 0)
    {
        return (
            <>
                <p>
                    This ingredient is not currently used in any recipes.
                </p>

                <DeleteButton ingredient={ingredient} />
            </>
        )
    }
    else
    {
        const recipeListItems = recipes.map(recipe => {
            return <li key={recipe.id}>
                <Link to={`/recipes/${recipe.id}`}>{recipe.name}</Link>
            </li>
        });

        return (
            <>
                <h2>
                    Used in recipes:
                </h2>

                <ul>
                    {recipeListItems}
                </ul>
            </>
        )
    }
}

function DeleteButton({ingredient})
{
    function handleOnClick(e)
    {
        const service = new IngredientsService();

        service.deleteIngredient(ingredient.id);
    }

    return <button onClick={handleOnClick} type="button">
        Delete ingredient
    </button>
}