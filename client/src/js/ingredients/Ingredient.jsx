import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import IngredientsService from "../services/IngredientsService";
import findUnitName from "../helpers/findUnitName";
import { UnitsContext } from "../../UnitsContext";
import { AlertContext } from "../../AlertContext";

export default function Ingredient() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
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

    function handleDelete() {
        if (window.confirm(`Are you sure you want to delete this ingredient - ${ingredient.name}?`))
        {
            const service = new IngredientsService();
            service.deleteIngredient(ingredient.id).then(() => {
                setAlertMessage(`Ingredient "${ingredient.name}" was deleted.`);
                navigate("/ingredients");
            });
        }
    }

    return <>
            <div className="mt-2 mb-4 d-flex justify-content-between align-items-center">
                <h1 className="m-0">{ingredient.name}</h1>

                <div className="d-flex align-items-center column-gap-3">
                    <Link className="btn btn-primary" title="Edit ingredient" to={`/ingredients/${ingredient.id}/edit`}>Edit</Link>

                    <button type="button" title="Delete ingredient" className="btn btn-danger" onClick={handleDelete}>Delete</button>
                </div>
            </div>

            Quantity: {ingredient.quantity?.amount} {unitName}

            <IngredientDetails ingredient={ingredient} />

            <Link to="/ingredients">Back to ingredients</Link>
        </>;
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