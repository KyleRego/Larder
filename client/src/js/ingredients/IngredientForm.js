import { useContext } from "react";
import QuantityInput from "../components/QuantityInput";
import { UnitsContext } from "../../UnitsContext";

export default function IngredientForm({ingredient, handleFormSubmit}) {
    const { units } = useContext(UnitsContext);

    console.log(units);

    return <form onSubmit={handleFormSubmit}>
        <div className="mb-2 d-flex column-gap-1 align-items-center">
            <label htmlFor="name">Name:</label>
            <input className="flex-grow-1" type="text" name="name" defaultValue={ingredient.name}></input>
        </div>

        <div className="mb-2">
            <QuantityInput quantity={ingredient.quantity} units={units} />
        </div>

        <div>
            <button className="btn btn-primary" type="submit">Submit</button>
        </div>
    </form>;
}
