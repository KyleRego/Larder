export default function FoodForm({initialFood, handleSubmit})
{
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
            <label htmlFor="quantity">Quantity (number of servings ready to eat):</label>
            <input id="quantity" name="quantity" type="number" defaultValue={initialFood.quantity}></input>
        </div>

        <div>
            <strong>Per serving:</strong>
        </div>

        <div>
            <label htmlFor="calories">Calories:</label>
            <input id="calories" name="calories" type="number" defaultValue={initialFood.calories}></input>
        </div>

        <div>
            <label htmlFor="protein">Protein (grams):</label>
            <input id="protein" name="protein" type="number" defaultValue={initialFood.protein}></input>
        </div>

        <div>
            <button type="submit">Submit</button>
        </div>

    </form>
}