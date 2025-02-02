import { NutritionDto } from "../../types/NutritionDto";
import QuantitySpan from "../QuantitySpan";
import NutritionTable from "../tables/NutritionTable";

export default function NutritionCard({nutrition} : {nutrition: NutritionDto}) {
    return <div className="card"
            style={{maxWidth: "276px", maxHeight: "324px", overflow: "scroll"}}>
        <div className="card-body">
            <h3 className="card-title">Nutrition</h3>
            <div className="d-flex justify-content-center column-gap-1">
                <span>Serving size - </span> <QuantitySpan quantity={nutrition.servingSize} />
            </div>

            <NutritionTable nutrition={nutrition} />

        </div>
    </div>;
}