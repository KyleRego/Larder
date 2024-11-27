export default function FoodFormControls() {
    return (
        <>
            <div className="d-flex column-gap-3">
                <div className="flex-grow-1">
                    <label>Servings per item amount:</label>
                    <input className="form-control" type="number"></input>
                </div>
                <div className="flex-grow-1">
                    <label htmlFor="servings">Servings:</label>
                    <input className="form-control" id="servings" type="number" name="servings" title="Food servings:" />
                </div>
            </div>

            <div className="mt-2">
                <label htmlFor="caloriesInput">Calories per serving:</label>
                <input className="form-control" id="caloriesInput" type="number" name="calories" title="Calories per serving:" />
            </div>
        </> 
    );
}
