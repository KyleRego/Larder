import { useState, useEffect, useContext } from "react";
import { useParams, Link } from "react-router-dom";
import IngredientsService from "../services/IngredientsService";
import findUnitName from "../helpers/findUnitName";
import { UnitsContext } from "../../UnitsContext";

export default function Ingredient() {
    const { units } = useContext(UnitsContext);
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

    const unitName = findUnitName(ingredient.quantity.unitId, units);

    function handleDelete()
    {
        if (window.confirm(`Are you sure you want to delete this ingredient - ${ingredient.name}?`))
        {
            const service = new IngredientsService();
            service.deleteIngredient(ingredient.id);
        }
    }

    return (
        <>
            <h1>
                {ingredient.name}
            </h1>

            Stock: {ingredient.quantity?.amount} {unitName}

            <IngredientDetails ingredient={ingredient} />

            <div className="d-flex column-gap-3">
                <Link className="btn btn-primary" to={`/ingredients/${ingredient.id}/edit`}>
                    Edit
                </Link>
            
                <button className="btn btn-danger" onClick={handleDelete} type="button">
                    Delete
                </button>
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
    

    return 
}