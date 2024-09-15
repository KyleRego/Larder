import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import IngredientsService from "../services/IngredientsService";
import IngredientsTable from "./IngredientsTable";
import { UnitsContext } from "../../UnitsContext";

export default function Ingredients()
{
    const { units } = useContext(UnitsContext);
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

            <IngredientsTable ingredients={ingredients} setIngredients={setIngredients}
                                sortOrder={sortOrder} setSortOrder={setSortOrder}
                                units={units} />

            <Link to="/ingredients/new">New ingredient</Link>
        </>
    )
}