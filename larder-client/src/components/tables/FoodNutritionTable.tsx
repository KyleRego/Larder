import { foodNutritionData } from "../../data/foodNutritionData";
import { FoodDto } from "../../types/FoodDto";

export default function FoodNutritionTable({food} : {food: FoodDto}) {
    const tableRows = foodNutritionData.map(entry => {
        const field = entry.field;
        const label = entry.label;

        return <tr>
                <td scope="row">{label}</td>
                <td>{`${(food as any)[field]} ${entry.unitName}`}</td>
            </tr>;
    })

    return <table className="table">
        <caption>
            Nutrition per serving
        </caption>
        <thead>
            <th scope="col">Nutrient</th>
            <th scope="col">Amount</th>
        </thead>
        <tbody>
            {tableRows}
        </tbody>
    </table>;
}
