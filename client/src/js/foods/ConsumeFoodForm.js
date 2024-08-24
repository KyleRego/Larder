import FoodsService from "../services/FoodsService";

export default function ConsumeFoodForm({food, foods, setFoods})
{
    async function handleEatFood(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);

        const foodService = new FoodsService();

        const dto = {
            foodId: food.id,
            servings: formData.get("servingsConsumed")
        };

        await foodService.postEatFood(dto).then(result => {
            const newFoods = structuredClone(foods);

            for (let i = 0; i < foods.length; i += 1)
            {
                if (foods[i] === food)
                {
                    newFoods[i] = result;
                }
            }

            setFoods(newFoods);
        })
    }

    return <form onSubmit={handleEatFood}>
                <div className="d-flex flex-column flex-sm-row justify-content-around align-items-center">
                    <div className="text-center">
                        <label htmlFor="servingsConsumed">Consume servings:</label>
                        <input type="number" step="any" name="servingsConsumed" defaultValue={0}></input>
                    </div>
                    <div>
                        <button type="submit" className="btn btn-primary btn-sm">Submit</button>
                    </div>
                </div>
            </form>
}
