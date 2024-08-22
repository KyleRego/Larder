import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";

import FoodsTable from "./FoodsTable";

import FoodEater from "./FoodEater";

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
        <div className="d-flex justify-content-between align-items-center">
            <h1>Foods</h1>
            <div>
                <button type="button" className="btn btn-primary" onClick={toggleViewMode}>Toggle view mode</button>
            </div>
        </div>

        {viewMode === "table" 
            && <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
        }

        <div className="">
            {viewMode === "cards"
                && foods.map(food => {
                    return <div className="row">
                            <div className="mb-3 card shadow-sm" key={food.id} style={{maxWidth: "28rem"}}>
                                <div className="card-body">
                                    <h5 className="card-title">{food.name}</h5>
                                    <p className="">{food.description}</p>
                                    <p className="">Amount: {food.amount}</p>
                                    <div>
                                        <FoodEater food={food} />
                                    </div>   
                                </div>
                            </div>
                        </div> 
                    }
                )
            }
        </div>

        <div>
            <Link to="/foods/new">New food</Link>
        </div>
    </>
}