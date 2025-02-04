import { NutritionDto } from "../../types/NutritionDto";
import QuantitySpan from "../QuantitySpan";
import NutritionTable from "../tables/NutritionTable";

export default function NutritionCard({nutrition} : {nutrition: NutritionDto}) {
    return <div className="card"
            style={{minWidth: "424px", maxHeight: "267px", overflowY: "scroll"}}>
        <div className="card-body">
            <h3 className="card-title fs-4">
                <div className="d-flex justify-content-center column-gap-1 flex-wrap border-bottom border-2 border-black pb-1">
                    <div>
                    Nutrition
                    </div>
                    <div className="d-flex justify-content-center column-gap-1 sticky">
                        (per <QuantitySpan quantity={nutrition.servingSize} /> serving size)
                    </div>
                </div>
            </h3>

            <NutritionTable nutrition={nutrition} />

        </div>
    </div>;
}