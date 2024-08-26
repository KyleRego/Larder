import FoodsService from "../services/FoodsService";

export default function ConsumeFoodForm({food, setFood})
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

        await foodService.postEatFood(dto).then((returnedDto) => {
            setFood(returnedDto);
        }).catch(error => {
            console.log(error);
        });
    }

    return <form onSubmit={handleEatFood}>
                <div className="d-flex column-gap-3 flex-wrap align-items-center">
                    <div className="">
                        <label htmlFor="servingsConsumed">Consume servings:</label>
                        <input type="number" step="any" name="servingsConsumed" defaultValue={0}></input>
                    </div>
                    <div>
                        <button type="submit" className="btn btn-primary btn-sm">Submit</button>
                    </div>
                </div>
            </form>;
}
