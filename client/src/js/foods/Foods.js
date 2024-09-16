import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import FoodsService from "../services/FoodsService";
import FoodsTable from "./FoodsTable";
import { AlertContext } from "../../AlertContext";

export default function Foods() {
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
        <div className="mb-4 mt-2 d-flex flex-wrap row-gap-1 align-items-center justify-content-around">
            <h1 className="m-0">Your foods:</h1>

            <Link to="/foods/new" className="btn btn-primary" title="Add new food">New food</Link>
        </div>

        <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
    </>;
}
