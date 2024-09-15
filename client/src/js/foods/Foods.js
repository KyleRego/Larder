import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import FoodsService from "../services/FoodsService";
import FoodsTable from "./FoodsTable";
import { AlertContext } from "../../AlertContext";

export default function Foods()
{
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext)
    const [foods, setFoods] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFoods(sortOrder).then(result => {
            setFoods(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });

    }, [sortOrder, setAlertMessage]);

    return <>
        <div className="d-flex column-gap-3 row-gap-1 flex-wrap align-items-center">
            <h1>Foods inventory</h1>

            <Link to="/foods/new" className="btn btn-primary" title="Add new food">Add food</Link>
        </div>

        <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
    </>
}
