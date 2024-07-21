import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import IngredientsService from "../services/IngredientsService";
import IngredientsTable from "./IngredientsTable";

export default function Ingredients()
{
    const [ingredients, setIngredients] = useState(null);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(sortOrder).then(result => {
            setIngredients(result);
        });
    }, [sortOrder]);

    if (ingredients === null) return <h1>Loading...</h1>;

    return (
        <>
            <h1>Ingredients</h1>

            <IngredientsTable ingredients={ingredients} sortOrder={sortOrder} setSortOrder={setSortOrder} />

            <Link to="/ingredients/new">New ingredient</Link>
        </>
    )
}