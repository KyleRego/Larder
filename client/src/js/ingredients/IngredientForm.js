import UnitSelectOptions from "../units/UnitSelectOptions";

export default function IngredientForm({initialIngredient, units, handleFormSubmit})
{
    return <form onSubmit={handleFormSubmit}>
        <label htmlFor="name">Name:</label>
        <input type="text" name="name" defaultValue={initialIngredient.name}></input>

        <label htmlFor="quantity">Quantity:</label>
        <input type="number" name="quantity" defaultValue={initialIngredient.quantity}></input>

        <label htmlFor="unit">Unit:</label>
        <select name="unit" defaultValue={initialIngredient.unitId}>
            <UnitSelectOptions units={units} />
        </select>

        <button type="submit">Submit</button>
    </form>
}