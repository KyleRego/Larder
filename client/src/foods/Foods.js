import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";

import FoodsTable from "./FoodsTable";
import FoodCard from "../components/Cards/FoodCard";

import SelectedFood from "./SelectedFood";

export default function Foods({units})
{
    const [foods, setFoods] = useState([]);
    const [selectedFood, setSelectedFood] = useState(null);

    const [sortOrder, setSortOrder] = useState("Name");
    const [viewMode, setViewMode] = useState("table");

    function toggleViewMode()
    {
        if (viewMode === "table")
        {
            setViewMode("cards");
        }
        else
        {
            setViewMode("table");
        }
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
        <h1>Foods</h1>

        {selectedFood && <SelectedFood food={selectedFood} />}

        <div className="text-center">
            <button type="button" className="btn btn-primary" onClick={toggleViewMode}>Toggle view mode</button>
        </div>

        {viewMode === "table" 
            && <FoodsTable foods={foods} setFoods={setFoods} sortOrder={sortOrder} setSortOrder={setSortOrder} units={units} />
        }

        <div className="flex row-gap-3">
            {viewMode === "cards"
                && foods.map(food => {
                    return <div key={food.id} onClick={() => setSelectedFood(food)}>
                            <FoodCard food={food} />
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