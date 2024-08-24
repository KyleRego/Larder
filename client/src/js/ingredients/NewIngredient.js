import { Link } from "react-router-dom";

import IngredientForm from "./IngredientForm";
import IngredientsService from "../services/IngredientsService";

export default function NewIngredient({units})
{
    async function handleFormSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const ingredientData = {
            name: formData.get("name"),
            quantity: formData.get("quantity")
        };

        const unitValue = formData.get("unit");

        if (unitValue !== "")
        {
            ingredientData.unitId = unitValue;
        }

        const ingredientsService = new IngredientsService();

        await ingredientsService.postIngredient(ingredientData);
    }

    return (
        <>
            <h1>New ingredient</h1>

            <IngredientForm initialIngredient={{}} units={units} handleFormSubmit={handleFormSubmit} />

            <Link to="/ingredients">Back to ingredients</Link>
        </>
    )
}