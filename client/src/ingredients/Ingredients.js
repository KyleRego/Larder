import { useState, useEffect } from "react";

import IngredientsService from "../services/IngredientsService";
import IngredientsTable from "./IngredientsTable";

export default function Ingredients()
{
    const [ingredients, setIngredients] = useState(null);

    useEffect(() =>
    {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients().then(result => {
            setIngredients(result);
        });
    }, []);

    if (ingredients === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>Ingredients</h1>

            <IngredientsTable ingredients={ingredients} />
        </>
    )
}