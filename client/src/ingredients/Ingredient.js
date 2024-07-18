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

    const recipeListItems = ingredient.recipes.map(recipe => {
        return <li key={recipe.id}>
            <Link to={`/recipes/${recipe.id}`}>{recipe.name}</Link>
        </li>
    });

    return (
        <>
            <h1>
                {ingredient.name}
            </h1>

            <h2>
                Recipes
            </h2>

            <ul>
                {recipeListItems}
            </ul>

            <Link to="/ingredients">Back to ingredients</Link>
        </>
    )
}