import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";

import IngredientsService from "../services/IngredientsService";
import IngredientsTable from "./IngredientsTable";
import { UnitsContext } from "../../UnitsContext";
import { AlertContext } from "../../AlertContext";

export default function Ingredients() {
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext);
    const [ingredients, setIngredients] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(sortOrder).then(result => {
            setIngredients(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });
    }, [sortOrder, setAlertMessage]);

    return <>
        <div className="mt-2 mb-4 d-flex justify-content-around">
            <h1>Your ingredients:</h1>

            <Link className="btn btn-primary" title="New ingredient" to="/ingredients/new">New ingredient</Link>
        </div>
            
        <IngredientsTable ingredients={ingredients} setIngredients={setIngredients}
                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                            units={units} />
        </>;
}
