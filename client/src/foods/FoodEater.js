import FoodsService from "../services/FoodsService";

export default function FoodEater({food})
{
    async function handleEatFood(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);

        const foodService = new FoodsService();

        const dto = {
            foodId: food.id,
            servingsConsumed: formData.get("servingsConsumed")
        };

        await foodService.postEatFood(dto);
    }

    return <form onSubmit={handleEatFood}>
                <div className="d-flex justify-content-start align-items-center">
                    <div className="me-4">
                        <label htmlFor="servingsConsumed">Consume servings:</label>
                        <input type="number" step="any" name="servingsConsumed"></input>
                    </div>
                    <div>
                        <button type="submit" className="btn btn-primary btn-sm">Submit</button>
                    </div>
                </div>
            </form>
}
