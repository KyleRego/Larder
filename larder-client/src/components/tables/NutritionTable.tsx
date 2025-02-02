import { UINutrientData } from "../../data/UINutrientData";
import { NutritionDto } from "../../types/NutritionDto";

export default function NutritionTable({nutrition} : {nutrition: NutritionDto}) {
    const tableRows = UINutrientData.map(entry => {
        const field = entry.field;
        const label = entry.label;

        return <tr>
                <td scope="row">{label}</td>
                <td>{`${(nutrition as any)[field]} ${entry.unitName}`}</td>
            </tr>;
    })

    return <table className="table">
        <caption>
            Nutrition per serving size quantity
        </caption>
        <thead>
            <th scope="col">Nutrient</th>
            <th scope="col">Quantity</th>
        </thead>
        <tbody>
            {tableRows}
        </tbody>
    </table>;
}
