import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import IngredientsService from "../services/IngredientsService";
import IngredientForm from "./IngredientForm";

export default function EditIngredient({units})
{
    function handleFormSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);
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