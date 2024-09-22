import { Link, useNavigate } from "react-router-dom";

import IngredientForm from "./IngredientForm";
import IngredientsService from "../services/IngredientsService";
import { useContext } from "react";
import { AlertContext } from "../../AlertContext";

export default function NewIngredient() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);

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

    return <>
            <h1 className="mt-2 mb-4">New ingredient:</h1>

            <IngredientForm ingredient={{}} handleFormSubmit={handleFormSubmit} />

            <div className="mt-4">
                <Link to="/ingredients">Back to ingredients</Link>
            </div>
        </>;
}
