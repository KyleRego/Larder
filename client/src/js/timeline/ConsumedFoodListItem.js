import { useState } from "react";
import { CiEdit } from "react-icons/ci";
import { TiDeleteOutline } from "react-icons/ti";

import ConsumedFoodService from "../services/ConsumedFoodService";
import EditConsumedFood from "./EditConsumedFood";

export default function ConsumedFoodListItem({consumedFood, nutritionDay, nutritionDays, setNutritionDays})
{
    const [editing, setEditing] = useState(false);

    function handleDelete()
    {
        if (window.confirm(`Are you sure you want to delete ${consumedFood.foodName} consumed on ${nutritionDay.date}?`))
        {
            const service = new ConsumedFoodService();

            service.deleteConsumedFood(consumedFood.id).then(() => {
                const newNutritionDays = structuredClone(nutritionDays);

                for (let i = 0; i < newNutritionDays.length; i += 1)
                {
                    const newNutritionDay = newNutritionDays[i];
                    if (newNutritionDay.date === nutritionDay.date)
                    {
                        for (let j = 0; j < newNutritionDay.consumedFoods.length; j += 1)
                        {
                            const cf = newNutritionDay.consumedFoods[j];

                            if (cf.id === consumedFood.id)
                            {
                                newNutritionDay.totalCalories -= consumedFood.caloriesConsumed;
                                newNutritionDay.consumedFoods.splice(j, 1);
                                setNutritionDays(newNutritionDays);
                                break;
                            }
                        }
                    }
                }
            });
        }
    }

    const text = `${consumedFood.foodName}: ${consumedFood.caloriesConsumed} calories;`;

    return <li className="list-group-item" key={consumedFood.foodName}>
                {editing === true
                    ?
                    <EditConsumedFood consumedFood={consumedFood} nutritionDay={nutritionDay} nutritionDays={nutritionDays}
                                    setNutritionDays={setNutritionDays} setEditing={setEditing} />
                    :
                    <div className="d-flex align-items-center column-gap-1">
                        <span>
                            {text}
                        </span>
                        <CiEdit tabIndex="0" className="w-5 h-5" role="button" title="Edit consumed food" onClick={() => setEditing(true)} />
                        <TiDeleteOutline tabIndex="0" className="w-5 h-5" role="button" title="Delete consumed food" onClick={handleDelete} />
                    </div>
                }
            </li>;
}
