import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";
import FoodsTable from "./FoodsTable";

export default function Foods({units})
{
    const [foods, setFoods] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFoods(sortOrder).then(result => {
            setFoods(result);
        }).catch(error => {
            console.log(error);
        });

    }, [sortOrder]);

    return <>
        <div className="d-flex column-gap-3 row-gap-1 flex-wrap align-items-center">
            <h1>Foods Inventory</h1>

            <Link to="/foods/new" className="btn btn-primary" title="Add new food">Add food</Link>
        </div>

        <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
    </>
}
