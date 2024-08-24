import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";

import FoodsTable from "./FoodsTable";

import FoodsCards from "./FoodsCards";

export default function Foods({units})
{
    const [foods, setFoods] = useState([]);

    const [sortOrder, setSortOrder] = useState("Name");
    const [viewMode, setViewMode] = useState("table");

    function toggleViewMode()
    {
        (viewMode === "table") ? setViewMode("cards") : setViewMode("table");
    }

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
        <div className="d-flex flex-column flex-sm-row justify-content-between align-items-center">
            <h1>Foods</h1>
            <div>
                <button type="button" className="btn btn-primary" onClick={toggleViewMode}>Toggle view mode</button>
            </div>
        </div>

        {viewMode === "table" 
            && <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
        }

        {viewMode === "cards"
            && <FoodsCards foods={foods} setFoods={setFoods} />
        }

        <div>
            <Link to="/foods/new">New food</Link>
        </div>
    </>
}
