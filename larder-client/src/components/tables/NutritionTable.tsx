import { NutritionDto } from "../../types/NutritionDto";
import QuantitySpan from "../QuantitySpan";

export default function NutritionTable({nutrition} : {nutrition: NutritionDto}) {
    return <table className="table">
        <caption>
            Nutrition per <QuantitySpan quantity={nutrition.servingSize} />
        </caption>
        <thead>
        <tr>
            <th scope="col" colSpan={2}>Macronutrients</th>
            <th scope="col" colSpan={2}>Micronutrients</th>
        </tr>
    </thead>
        <tbody>
            <tr>
                <td>Calories</td>
                <td>{nutrition.calories}</td>
                <td>Sodium</td>
                <td>{nutrition.milligramsSodium}</td>
            </tr>
            <tr>
                <td>Protein</td>
                <td>{nutrition.gramsProtein} g</td>
                <td>Cholesterol</td>
                <td>{nutrition.milligramsCholesterol} mg</td>
            </tr>
            <tr>
                <td>Total carbs</td>
                <td>{nutrition.gramsTotalCarbs} g</td>
            </tr>
            <tr>
                <td>Total fat</td>
                <td>{nutrition.gramsTotalFat} g</td>
            </tr>
            <tr>
                <td>Total sugars</td>
                <td>{nutrition.gramsTotalSugars} g</td>
            </tr>
            <tr>
                <td>Dietary fiber</td>
                <td>{nutrition.gramsDietaryFiber} g</td>
            </tr>
            <tr>
                <td>Saturated fat</td>
                <td>{nutrition.gramsSaturatedFat} g</td>
            </tr>
            <tr>
                <td>Trans fat</td>
                <td>{nutrition.gramsTransFat} g</td>
            </tr>
        </tbody>
    </table>;
}
