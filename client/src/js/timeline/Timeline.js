import { useState, useEffect } from "react";

import ConsumedFoodsService from "../services/ConsumedFoodsService";

export default function Timeline()
{
    const [data, setData] = useState([]);

    useEffect(() => {
        const service = new ConsumedFoodsService();

        service.getConsumedFoodsIndex().then(result => {
            setData(result);
        }).catch(error => {
            console.error(error);
        });
    }, []);

    console.log(data);

    const days = data.map(day => {

        const consumedFoodItems = day.consumedFoods.map(f => {
            return <li key={f.foodName}>
                {f.servingsConsumed} servings of {f.foodName}: {f.caloriesConsumed} calories and {f.proteinConsumed} grams of protein
            </li>
        });

        return <div key={day.date}>
            <h3>{day.date}</h3>

            <p>
                Total calories: {day.totalCalories}, total protein: {day.totalProtein}
            </p>

            <ul>
                {consumedFoodItems}
            </ul>
        </div>;
    });


    return <>
        <h1>Timeline</h1>

        {days}
    </>;
}
