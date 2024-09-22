import ConsumedFoodService from "../services/ConsumedFoodService";
import ConsumedFoodForm from "./ConsumedFoodForm";

export default function EditConsumedFood({consumedFood, setEditing, nutritionDay, nutritionDays, setNutritionDays})
{
    function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const dto = {
            id: consumedFood.id,
            foodName: formData.get("foodName"),
            servingsConsumed: parseFloat(formData.get("servingsConsumed")),
            caloriesConsumed: parseFloat(formData.get("caloriesConsumed"))
        };

        const service = new ConsumedFoodService();
        
        service.putConsumedFood(dto).then((returnedDto) => {
            const newNutritionDays = structuredClone(nutritionDays);

            for (let i = 0; i < newNutritionDays.length; i += 1)
            {
                const newNutritionDay = newNutritionDays[i];

                if (newNutritionDay.date === nutritionDay.date)
                {
                    for (let j = 0; j < newNutritionDay.consumedFoods.length; j += 1)
                    {
                        const cf = newNutritionDay.consumedFoods[j];
                    
                        if (cf.id === dto.id)
                        {
                            newNutritionDay.totalCalories -= cf.caloriesConsumed;
                            newNutritionDay.totalCalories += dto.caloriesConsumed;

                            cf.foodName = dto.foodName;
                            cf.servingsConsumed = dto.servingsConsumed;
                            cf.caloriesConsumed = dto.caloriesConsumed;

                            break;
                        }
                    }
                }
            }

            setNutritionDays(newNutritionDays);
        })

        setEditing(false);
    }

    function handleCancel()
    {
        setEditing(false);
    }

    return <ConsumedFoodForm consumedFood={consumedFood} handleSubmitFunction={handleSubmit} handleCancelFunction={handleCancel} />
}
