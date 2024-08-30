import LabeledInput from "../components/LabeledInput";

export default function ConsumedFoodForm({consumedFood, handleSubmitFunction, handleCancelFunction})
{
    const formElementsData = [
        // inputName, labelText, initialValue, afterInputText, inputType, required
        ["calories", "Calories:", consumedFood.calories, "", "number", true],
        ["gramsProtein", "Protein:", consumedFood.gramsProtein, "g", "number", true],
        ["gramsTotalFat", "Total fat:", consumedFood.gramsTotalFat, "g", "number", false],
        ["gramsSaturatedFat", "Saturated fat:", consumedFood.gramsSaturatedFat, "g", "number", false],
        ["gramsTransFat", "Trans fat:", consumedFood.gramsTransFat, "g", "number", false],
        ["milligramsCholesterol", "Cholesterol:", consumedFood.milligramsCholesterol, "g", "number", false],
        ["milligramsSodium", "Sodium:", consumedFood.milligramsSodium, "mg", "number", false],
        ["gramsTotalCarbs", "Total carbs:", consumedFood.gramsTotalCarbs, "mg", "number", false],
        ["gramsDietaryFiber", "Dietary fiber:", consumedFood.gramsDietaryFiber, "g", "number", false],
        ["gramsTotalSugars", "Total sugars:", consumedFood.gramsTotalSugars, "g", "number", false]
    ];

    const formInputs = formElementsData.map((data, index) => {
        return <LabeledInput key={index}
                inputName={data[0]} labelText={data[1]} initialValue={data[2]}
                afterInputText={data[3]} inputType={data[4]} required={data[5]} />;
    })

    return <form onSubmit={handleSubmitFunction}>
        <div className="d-flex column-gap-1 flex-wrap align-items-center">
            <label htmlFor="name">Name:</label>
            <input className="flex-grow-1" defaultValue={consumedFood.name} required name="name" type="text"></input>
        </div>

        <div className="d-flex column-gap-3 flex-wrap">
            {formInputs}
            
            <div className="d-flex column-gap-3">
                <button className="btn btn-primary btn-sm" type="submit" title="Update consumed food">Submit</button>
                <button onClick={handleCancelFunction} title="Cancel" className="btn btn-secondary btn-sm" type="button">Cancel</button>
            </div>
        </div>
        
    </form>;
}
