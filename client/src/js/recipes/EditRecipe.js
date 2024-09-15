import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";

import RecipesService from "../services/RecipesService";

import RecipeForm from "./RecipeForm";
import { UnitsContext } from "../../UnitsContext";
import { AlertContext } from "../../AlertContext";

export default function EditRecipe() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext);

    async function handleSubmit(e, formRecipe)
    {
        e.preventDefault();
        const recipeName = new FormData(e.target).get("recipeName");
        formRecipe.name = recipeName;

        const recipesService = new RecipesService();

        await recipesService.putRecipe(formRecipe).then(() => {
            setAlertMessage(`Recipe "${formRecipe.name}" was updated.`);
            navigate(`/recipes/${formRecipe.id}`);
        });
    }

    let { id } = useParams();
    const [recipe, setRecipe] = useState(null);

    useEffect(() =>
    {
        const recipesService = new RecipesService();

        recipesService.getRecipe(id).then(result => {
            setRecipe(result);
        });

    }, [id]);

    if (recipe === null || units === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>Editing recipe: {recipe.name}</h1>

            <div className="card shadow-sm">
                <div className="card-body">
                    <RecipeForm recipe={recipe} units={units} handleSubmit={handleSubmit} />
                </div>
            </div>

            <div>
                <Link to={`/recipes/${id}`}>Back to recipe</Link>
            </div>
        </>
    );
}
