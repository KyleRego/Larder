

export default function FoodForm({initialFood, handleSubmit, units})
{

    return <form onSubmit={handleSubmit}>
        <div className="d-flex flex-wrap column-gap-3 align-items-stretch">
            <div className="flex-grow-1">
                <div className="">
                    <label className="d-block" htmlFor="name">Name*:</label>
                    <input className="w-100" required id="name" name="name" type="text" defaultValue={initialFood.name}></input>
                </div>

                <div className="d-flex column-gap-1 align-items-center">
                    <label htmlFor="servings">Number of servings*:</label>
                    <input required type="number" name="servings" defaultValue={initialFood.servings}></input>
                </div>
            </div>
        

            <div className="flex-grow-1">
                <label htmlFor="description">Description:</label>
                <textarea id="description" name="description" defaultValue={initialFood.description}></textarea>
            </div>
        </div>

        <h2>Nutrition details per 1 serving:</h2>

        <p>g = grams; mg = milligrams</p>

        <div className="d-flex justify-content-between row-gap-3 flex-wrap align-items-center">
            <div className="d-flex column-gap-1 align-items-center">
                <label className="fw-bold" htmlFor="calories">Calories*:</label>
                <input required name="calories" type="number" step="any" defaultValue={initialFood.calories}></input>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label className="fw-bold" htmlFor="gramsProtein">Protein:</label>
                <input name="gramsProtein" type="number" step="any" defaultValue={initialFood.gramsProtein}></input>
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label className="fw-bold" htmlFor="gramsTotalFat">Total fat:</label>
                <input name="gramsTotalFat" type="number" step="any" defaultValue={initialFood.gramsTotalFat}></input>
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="gramsSaturatedFat">Saturated fat:</label>
                <input name="gramsSaturatedFat" type="number" step="any" defaultValue={initialFood.gramsSaturatedFat}></input>            
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="gramsTransFat">Trans fat:</label>
                <input name="gramsTransFat" type="number" step="any" defaultValue={initialFood.gramsTransFat}></input>            
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="milligramsCholesterol">Cholesterol:</label>
                <input name="milligramsCholesterol" type="number" step="any" defaultValue={initialFood.milligramsCholesterol}></input>            
                <span>mg</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="milligramsSodium">Sodium</label>
                <input name="milligramsSodium" type="number" step="any" defaultValue={initialFood.milligramsSodium}></input>
                <span>mg</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label className="fw-bold" htmlFor="gramsTotalCarbs">Total carbs:</label>
                <input name="gramsTotalCarbs" type="number" step="any" defaultValue={initialFood.gramsTotalCarbs}></input>
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="gramsDietaryFiber">Dietary fiber:</label>
                <input name="gramsDietaryFiber" type="number" step="any" defaultValue={initialFood.gramsDietaryFiber}></input>
                <span>g</span>
            </div>

            <div className="d-flex column-gap-1 align-items-center">
                <label htmlFor="gramsTotalSugars">Total sugars:</label>
                <input name="gramsTotalSugars" type="number" step="any" defaultValue={initialFood.gramsTotalSugars}></input>
                <span>g</span>
            </div>
        </div>

        <div>
            <button className="btn btn-primary" type="submit">Submit</button>
        </div>
    </form>
}
