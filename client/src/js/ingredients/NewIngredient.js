import { Link } from "react-router-dom";

import IngredientForm from "./IngredientForm";
import IngredientsService from "../services/IngredientsService";

export default function NewIngredient({units})
{
    async function handleFormSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);
        const name = formData.get("name");
        const amount = formData.get("amount");
        const unitId = formData.get("unitId");

        const dto = {
            name: name,
            quantity: {
                amount: amount,
                unitId: unitId
            }
        };

        const ingredientsService = new IngredientsService();

        await ingredientsService.postIngredient(dto);
    }

    return (
        <>
            <h1>New ingredient</h1>

            <IngredientForm ingredient={{}} units={units} handleFormSubmit={handleFormSubmit} />

            <Link to="/ingredients">Back to ingredients</Link>
        </>
    )
}