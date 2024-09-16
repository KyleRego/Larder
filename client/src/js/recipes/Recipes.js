import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import RecipesTable from "./RecipesTable";
import RecipesService from "../services/RecipesService";
import "./Recipes.css"
import { AlertContext } from "../../AlertContext";

export default function Recipes() {
    const { setAlertMessage } = useContext(AlertContext);
    const [recipes, setRecipes] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        const service = new RecipesService();

        service.getRecipes(sortOrder, searchParam).then(result => {
            setRecipes(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        })
    }, [sortOrder, searchParam, setAlertMessage]);

    return <>
        <div className="mt-2 mb-4 d-flex justify-content-around align-items-center">
            <h1>Your recipes:</h1>

            <div className="d-flex flex-column align-items-start">
                <label htmlFor="search">Search:</label>
                <input id="search" className="form-control-sm"
                    type="search" onChange={(e) => setSearchParam(e.target.value)} />
            </div>

            <Link className="btn btn-primary" title="New recipe" to="/recipes/new">New recipe</Link>
        </div>
        
        <RecipesTable recipes={recipes} sortOrder={sortOrder} setSortOrder={setSortOrder} />
    </>;
}
