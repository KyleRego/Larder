import { NutritionDto } from "../../types/NutritionDto";
import QuantitySpan from "../QuantitySpan";
import NutritionTable from "../tables/NutritionTable";

export default function NutritionCard({nutrition} : {nutrition: NutritionDto}) {
    return <div className="card"
            style={{maxWidth: "424px", maxHeight: "267px", overflowY: "scroll"}}>
        <div className="card-body">
            <h3 className="card-title fs-4">
                Nutrition per <QuantitySpan quantity={nutrition.servingSize} />
            </h3>

            <NutritionTable nutrition={nutrition} />

        </div>
    </div>;
}