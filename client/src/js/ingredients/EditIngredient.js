import { useState, useEffect, useContext } from "react";
import { useParams, Link } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import IngredientsService from "../services/IngredientsService";
import IngredientForm from "./IngredientForm";

export default function EditIngredient() {
    const { units } = useContext(UnitsContext);
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

        await ingredientsService.putIngredient(dto);
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

    return (
        <>
            <h1>Editing ingredient {ingredient.name}</h1>

            <div className="card">
                <div className="card-body">
                    <IngredientForm ingredient={ingredient} units={units} handleFormSubmit={handleFormSubmit} />
                </div>
            </div>

            <Link to={`/ingredients/${id}`}>Back to ingredient</Link>
        </>
    )
}