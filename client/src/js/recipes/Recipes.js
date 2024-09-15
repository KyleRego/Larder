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

    useEffect(() => {
        const service = new RecipesService();

        service.getRecipes(sortOrder).then(result => {
            setRecipes(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        })
    }, [sortOrder, setAlertMessage]);

    return <>
        <h1>Recipes</h1>

        <RecipesTable recipes={recipes} sortOrder={sortOrder} setSortOrder={setSortOrder} />

        <Link to="/recipes/new">New recipe</Link>
    </>;
}
