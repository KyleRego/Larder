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
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        const ingredientsService = new IngredientsService();

        ingredientsService.getIngredients(sortOrder, searchParam).then(result => {
            setIngredients(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });
    }, [sortOrder, searchParam, setAlertMessage]);

    return <>
        <div className="mb-4 mt-2 d-flex flex-wrap column-gap-1 row-gap-3 align-items-center justify-content-around">
            <h1>Your ingredients:</h1>

            <div className="d-flex flex-column align-items-start">
                <label htmlFor="search">Search:</label>
                <input id="search" className="form-control-sm"
                    type="search" onChange={(e) => setSearchParam(e.target.value)} />
            </div>

            <Link className="btn btn-primary" title="New ingredient" to="/ingredients/new">New ingredient</Link>
        </div>
            
        <IngredientsTable ingredients={ingredients} setIngredients={setIngredients}
                            sortOrder={sortOrder} setSortOrder={setSortOrder}
                            units={units} />
        </>;
}
