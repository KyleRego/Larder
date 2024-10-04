import { useContext } from "react";
import FoodsService from "../services/FoodsService";
import { AlertContext } from "../../AlertContext";

export default function EatFoodForm({food, setFood}) {
    const { setAlertMessage } = useContext(AlertContext);
    
    async function handleEatFood(e) {
        e.preventDefault();
        const formData = new FormData(e.target);

        const foodService = new FoodsService();

        const dto = {
            foodId: food.id,
            servings: formData.get("servingsConsumed")
        };

        await foodService.postEatFood(dto).then(result => {
            setFood(result.data);
            setAlertMessage(result.message);
        }).catch(error => {
            console.log(error);
        });
    }

    return <form onSubmit={handleEatFood}>
                <div className="card card-body d-flex flex-row justify-content-center column-gap-3 flex-wrap align-items-center">
                    <div className="d-flex flex-wrap column-gap-1 align-items-center">
                        <label htmlFor="servingsConsumed">Eat servings:</label>
                        <input type="number" step="any" name="servingsConsumed" defaultValue={0}></input>
                    </div>
                    <div>
                        <button type="submit" className="btn btn-primary btn-sm">Submit</button>
                    </div>
                </div>
            </form>;
}
