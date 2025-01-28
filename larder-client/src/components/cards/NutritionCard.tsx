import { NutritionDto } from "../../types/NutritionDto";
import QuantitySpan from "../QuantitySpan";
import NutritionTable from "../tables/NutritionTable";

export default function NutritionCard({nutrition} : {nutrition: NutritionDto}) {
    return <div className="card" style={{maxWidth: "360px"}}>
        <div className="card-body">
            <h3 className="card-title">Nutrition Facts</h3>
            <div className="d-flex column-gap-3 p-2">
                <span>Serving size</span> <QuantitySpan quantity={nutrition.servingSize} />
            </div>

            <NutritionTable nutrition={nutrition} />

        </div>
    </div>;
}