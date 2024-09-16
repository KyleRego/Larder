

export default function FoodForm({initialFood, handleSubmit}) {
    return <form onSubmit={handleSubmit}>
        <div className="mb-2 d-flex column-gap-3 align-items-center">
            <label className="d-block" htmlFor="name"><strong>Name:</strong></label>
            <input required className="w-100" id="name" name="name" type="text" defaultValue={initialFood.name}></input>
        </div>

        <div className="my-2 d-flex column-gap-3 align-items-center">
            <label htmlFor="servings"><strong>Number of servings:</strong></label>
            <input required type="number" name="servings" defaultValue={initialFood.servings}></input>
        </div>

        <div className="my-2">
            <label htmlFor="description">Description:</label>
            <textarea id="description" name="description" defaultValue={initialFood.description}></textarea>
        </div>

        <h2 className="my-4">Nutrition details per 1 serving:</h2>

        <div className="mb-4 d-flex justify-content-between row-gap-3 flex-wrap align-items-start">
            <div className="d-flex flex-column row-gap-3">
                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="calories"><strong>Calories:</strong></label>
                    <input required name="calories" type="number" step="any" defaultValue={initialFood.calories}></input>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsProtein"><strong>Protein:</strong></label>
                    <input required name="gramsProtein" type="number" step="any" defaultValue={initialFood.gramsProtein}></input>
                    <span>g</span>
                </div>
            </div>

            <div className="d-flex flex-column row-gap-3">
                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsTotalFat"><strong>Total fat:</strong></label>
                    <input required name="gramsTotalFat" type="number" step="any" defaultValue={initialFood.gramsTotalFat}></input>
                    <span>g</span>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsSaturatedFat">Saturated fat:</label>
                    <input name="gramsSaturatedFat" type="number" step="any" defaultValue={initialFood.gramsSaturatedFat}></input>            
                    <span>g</span>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsTransFat">Trans fat:</label>
                    <input name="gramsTransFat" type="number" step="any" defaultValue={initialFood.gramsTransFat}></input>            
                    <span>g</span>
                </div>
            </div>

            <div className="d-flex flex-column row-gap-3">
                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsTotalCarbs"><strong>Total carbs:</strong></label>
                    <input required name="gramsTotalCarbs" type="number" step="any" defaultValue={initialFood.gramsTotalCarbs}></input>
                    <span>g</span>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsTotalSugars">Total sugars:</label>
                    <input name="gramsTotalSugars" type="number" step="any" defaultValue={initialFood.gramsTotalSugars}></input>
                    <span>g</span>
                </div>
            </div>
            
            <div className="d-flex flex-column row-gap-3">
                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="gramsDietaryFiber">Dietary fiber:</label>
                    <input name="gramsDietaryFiber" type="number" step="any" defaultValue={initialFood.gramsDietaryFiber}></input>
                    <span>g</span>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="milligramsCholesterol">Cholesterol:</label>
                    <input name="milligramsCholesterol" type="number" step="any" defaultValue={initialFood.milligramsCholesterol}></input>            
                    <span>mg</span>
                </div>

                <div className="d-flex justify-content-end column-gap-1 align-items-center">
                    <label htmlFor="milligramsSodium">Sodium</label>
                    <input name="milligramsSodium" type="number" step="any" defaultValue={initialFood.milligramsSodium}></input>
                    <span>mg</span>
                </div>
            </div>
        </div>

        <p className="my-2 text-center">g = grams; mg = milligrams</p>

        <div>
            <button className="btn btn-primary" type="submit">Submit</button>
        </div>
    </form>
}
