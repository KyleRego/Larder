import { useState } from "react";
import { MdAddCircleOutline } from "react-icons/md";

import ConsumedFoodListItem from "./ConsumedFoodListItem";
import NewConsumedFood from "./NewConsumedFood";

export default function DayNutritionCard({nutritionDay, nutritionDays, setNutritionDays})
{
    const [newingConsumedFood, setNewingConsumedFood] = useState(false);

    const consumedFoodListItems = nutritionDay.consumedFoods.map(f => {
        return <ConsumedFoodListItem key={f.id} consumedFood={f} nutritionDay={nutritionDay}
                                            nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />;
    });

    return <div className="card mb-4" key={nutritionDay.date}>
                <div className="card-body">
                    <h2 className="card-title mt-0">{nutritionDay.date}</h2>

                    <p className="mb-2">
                        Total calories: {nutritionDay.totalCalories}, total protein: {nutritionDay.totalProtein}
                    </p>

                    <ul className="mb-2 list-group list-group-flush">
                        {consumedFoodListItems}

                        {newingConsumedFood === true
                                ?
                                <li className="list-group-item">
                            <NewConsumedFood nutritionDay={nutritionDay} setNewingConsumedFood={setNewingConsumedFood}
                                        nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />
                                </li>
                                :
                                <li className="list-group-item hover-highlight d-flex justify-content-center align-items-center"
                                        onClick={() => setNewingConsumedFood(true)}
                                        title="Add food consumed on this day"
                                        role="button">
                                    <MdAddCircleOutline className="w-5 h-5" />
                                </li>
                        }

                        
                    </ul>

                    
                </div>
            </div>;
}
