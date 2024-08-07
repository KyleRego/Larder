import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";

import FoodsTable from "./FoodsTable";

export default function Foods()
{
    const [foods, setFoods] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFoods(sortOrder).then(result => {
            setFoods(result);
        }).catch(error =>
        {
            console.log(error);
        });

    }, [sortOrder]);

    return <>
        <h1>Foods (ready to eat servings of food)</h1>

        <FoodsTable foods={foods} sortOrder={sortOrder} setSortOrder={setSortOrder} />

        <div>
            <Link to="/foods/new">New food</Link>
        </div>
    </>
}