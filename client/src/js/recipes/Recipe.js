import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import RecipesService from "../services/RecipesService";
import findUnitName from "../helpers/findUnitName";
import { AlertContext } from "../../AlertContext";

export default function Recipe() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext)
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
        if (window.confirm(`Are you sure you want to delete recipe "${recipe.name}"?`)) {
            const service = new RecipesService();
            service.deleteRecipe(recipe.id).then(() => {
                setAlertMessage(`Recipe "${recipe.name}" was deleted.`);
                navigate("/recipes");
            });
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
