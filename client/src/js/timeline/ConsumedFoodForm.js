export default function ConsumedFoodForm({consumedFood, handleSubmitFunction, handleCancelFunction})
{
    return <form onSubmit={handleSubmitFunction}>
        <div className="d-flex column-gap-3 flex-wrap">
            <div>
                <label htmlFor="foodName">Food name:</label>
                <input required name="foodName" type="text" defaultValue={consumedFood.foodName}></input>
            </div>

            <div>
                <label htmlFor="servingsConsumed">Servings eaten:</label>
                <input name="servingsConsumed" type="number" step="any" defaultValue={consumedFood.servingsConsumed}></input>
            </div>
            
            <div>
                <label htmlFor="caloriesConsumed">Calories:</label>
                <input name="caloriesConsumed" type="number" step="any" defaultValue={consumedFood.caloriesConsumed}></input>
            </div>
            
            <div className="d-flex column-gap-3">
                <button className="btn btn-primary btn-sm" type="submit" title="Update consumed food">Submit</button>
                <button onClick={handleCancelFunction} title="Cancel" className="btn btn-secondary btn-sm" type="button">Cancel</button>
            </div>
        </div>
        
    </form>;
}
