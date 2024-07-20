import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import IngredientsService from "../services/IngredientsService";
import IngredientForm from "./IngredientForm";

export default function EditIngredient({units})
{
    async function handleFormSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const ingredientData = {
            id: id,
            name: formData.get("name"),
            quantity: formData.get("quantity")
        };

        const unitValue = formData.get("unit");

        if (unitValue !== "")
        {
            ingredientData.unitId = unitValue;
        }

        const ingredientsService = new IngredientsService();

        await ingredientsService.putIngredient(ingredientData);
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
            <h1>hello world from edit ingredients</h1>

            <IngredientForm initialIngredient={ingredient} units={units} handleFormSubmit={handleFormSubmit} />

            <Link to={`/ingredients/${id}`}>Back to ingredient</Link>
        </>
    )
}