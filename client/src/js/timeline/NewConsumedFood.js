import ConsumedFoodForm from "./ConsumedFoodForm";
import ConsumedFoodService from "../services/ConsumedFoodService";

export default function NewConsumedFood({setNewingConsumedFood, nutritionDay, nutritionDays, setNutritionDays})
{
    function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);

        const dto = {
            foodName: formData.get("foodName"),
            dateConsumed: nutritionDay.date,
            caloriesConsumed: formData.get("caloriesConsumed"),
            servingsConsumed: 0
        };

        const service = new ConsumedFoodService();

        service.postConsumedFood(dto).then(() => {
            const newNutritionDays = structuredClone(nutritionDays)

            for (let i = 0; i < newNutritionDays.length; i += 1)
            {
                if (newNutritionDays[i].date === nutritionDay.date)
                {
                    newNutritionDays[i].consumedFoods.push(dto);
                    break;
                }
            }

            setNutritionDays(newNutritionDays);
        });

        setNewingConsumedFood(false);
    }

    function handleCancel()
    {
        setNewingConsumedFood(false);
    }

    const initialConsumedFood = {
        foodName: "",
        caloriesConsumed: 0
    }

    return <ConsumedFoodForm consumedFood={initialConsumedFood}
                            handleSubmitFunction={handleSubmit} handleCancelFunction={handleCancel} />
}
