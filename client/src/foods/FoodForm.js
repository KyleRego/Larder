import QuantityInput from "../components/QuantityInput"
import FoodConstants from "./FoodConstants";

export default function FoodForm({initialFood, handleSubmit, units})
{
    const quantityProperties = FoodConstants.quantityProperties;

    const foodNutritionQuantitiesFormItems = quantityProperties.map(propName => {
        return <div>
            <QuantityInput initialQuantity={initialFood[propName]} units={units} name={propName} />
        </div>
    })

    return <form onSubmit={handleSubmit}>
        <div>
            <label htmlFor="name">Name:</label>
            <input id="name" name="name" type="text" defaultValue={initialFood.name}></input>
        </div>

        <div>
            <label htmlFor="description">Description:</label>
            <textarea id="description" name="description" defaultValue={initialFood.description}></textarea>
        </div>

        <div>
            <QuantityInput initialQuantity={initialFood.quantity} units={units} />
        </div>

        <h2>Nutrition:</h2>

        <div>
            <label htmlFor="calories">Calories:</label>
            <input id="calories" name="calories" type="number" defaultValue={initialFood.calories}></input>
        </div>

        {foodNutritionQuantitiesFormItems}

        <div>
            <button type="submit">Submit</button>
        </div>

    </form>
}