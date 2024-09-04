import QuantityInput from "../components/QuantityInput"

export default function IngredientForm({ingredient, units, handleFormSubmit})
{
    return <form onSubmit={handleFormSubmit}>
        <div className="mb-2 d-flex column-gap-1 align-items-center">
            <label htmlFor="name">Name:</label>
            <input className="flex-grow-1" type="text" name="name" defaultValue={ingredient.name}></input>
        </div>

        {/* <label htmlFor="amount">Quantity:</label>
        <input type="number" name="amount" defaultValue={ingredient.quantity.amount}></input>

        <label htmlFor="unitId">Unit:</label>
        <select name="unitId" defaultValue={ingredient.quantity.unitId}>
            <UnitSelectOptions units={units} />
        </select> */}

        <div className="mb-2">
            <QuantityInput quantity={ingredient.quantity} units={units} />
        </div>
        
        <div>
            <button className="btn btn-primary" type="submit">Submit</button>
        </div>
    </form>
}
