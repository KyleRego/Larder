import { Link, useNavigate } from "react-router-dom";

import IngredientForm from "./IngredientForm";
import IngredientsService from "../services/IngredientsService";
import { useContext } from "react";
import { UnitsContext } from "../../UnitsContext";
import { AlertContext } from "../../AlertContext";

export default function NewIngredient() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext);

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

        await ingredientsService.postIngredient(dto).then(() => {
            setAlertMessage(`Ingredient "${dto.name}" was created.`);
            navigate("/ingredients");
        });
    }

    return (
        <>
            <h1>New ingredient</h1>

            <IngredientForm ingredient={{}} units={units} handleFormSubmit={handleFormSubmit} />

            <Link to="/ingredients">Back to ingredients</Link>
        </>
    )
}