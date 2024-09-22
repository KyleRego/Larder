import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import IngredientsService from "../services/IngredientsService";
import IngredientForm from "./IngredientForm";
import { AlertContext } from "../../AlertContext";

export default function EditIngredient() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);

    async function handleFormSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const dto = {
            id: id,
            name: formData.get("name"),
            quantity: {
                amount: formData.get("amount"),
                unitId: formData.get("unitId")
            }
        };

        const ingredientsService = new IngredientsService();

        await ingredientsService.putIngredient(dto).then(() => {
            setAlertMessage(`Ingredient "${dto.name}" was updated.`);
            navigate(`/ingredients/${dto.id}`);
        });
    }

    let { id } = useParams();
    const [ingredient, setIngredient] = useState(null);

    useEffect(() =>
    {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredient(id).then(result => {
            setIngredient(result);
        });

    }, [id]);

    if (ingredient === null) return <h1>Loading...</h1>;

    return <>
            <h1>Editing ingredient {ingredient.name}</h1>

            <div className="card">
                <div className="card-body">
                    <IngredientForm ingredient={ingredient} handleFormSubmit={handleFormSubmit} />
                </div>
            </div>

            <Link to={`/ingredients/${id}`}>Back to ingredient</Link>
        </>;
}
