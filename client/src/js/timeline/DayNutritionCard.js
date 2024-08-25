import { useState } from "react";
import { MdAddCircleOutline } from "react-icons/md";

import ConsumedFoodListItem from "./ConsumedFoodListItem";
import NewConsumedFood from "./NewConsumedFood";

export default function DayNutritionCard({nutritionDay, nutritionDays, setNutritionDays})
{
    const [newingConsumedFood, setNewingConsumedFood] = useState(false);

    const consumedFoodItems = nutritionDay.consumedFoods.map(f => {
        return <ConsumedFoodListItem key={f.id} consumedFood={f} nutritionDay={nutritionDay}
                                            nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />;
    });

    return <div className="card mb-4" key={nutritionDay.date}>
                <div className="card-body">
                    <h2 className="card-title mt-0">{nutritionDay.date}</h2>

                    <p className="mb-2">
                        Total calories: {nutritionDay.totalCalories}, total protein: {nutritionDay.totalProtein}
                    </p>

                    <ul className="mb-2 list-group">
                        {consumedFoodItems}

                        <li className="list-group-item">
                            {newingConsumedFood === true
                                    ?
                                <NewConsumedFood nutritionDay={nutritionDay} setNewingConsumedFood={setNewingConsumedFood}
                                            nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />
                                    :
                                <MdAddCircleOutline onClick={() => setNewingConsumedFood(true)}
                                            className="w-5 h-5" role="button" title="Add food consumed on this day" />
                            }
                        </li>
                    </ul>

                    
                </div>
            </div>;
}
