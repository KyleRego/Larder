import { useState } from "react";
import { NutritionDto } from "../types/NutritionDto";

export default function NutritionInput(
                        {initialNutrition, handleNutritionChange}
                                    : {initialNutrition: NutritionDto,
            handleNutritionChange: (nutrition: NutritionDto) => void }) {
    const [nutrition, setNutrition] = useState<NutritionDto>(initialNutrition);

    function handleChange<T extends keyof NutritionDto>(key: T) {
        return (e: React.ChangeEvent<HTMLInputElement>) => {
            const value = parseFloat(e.target.value) || 0;
            const newNutrition = {...nutrition, [key]: value};
            setNutrition(newNutrition);
            handleNutritionChange(newNutrition);
        };
    }

    return <div>
        <div className="d-flex align-items-start justify-content-between flex-wrap">
            <div className="d-flex flex-column row-gap-1">
                <div className="input-group">
                    <span className="input-group-text">Calories</span>
                    <input value={nutrition.calories}
                            onChange={handleChange("calories")}
                            type="number"
                            step="any"
                            className="form-control"
                            id="calories-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Protein (g)</span>
                    <input value={nutrition.gramsProtein}
                            onChange={handleChange("gramsProtein")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsProtein-input" />
                </div>
            </div>

            <div className="d-flex flex-column row-gap-1">
                <div className="input-group">
                    <span className="input-group-text">Total carbs (g)</span>
                    <input value={nutrition.gramsTotalCarbs}
                            onChange={handleChange("gramsTotalCarbs")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsTotalCarbs-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Total sugars (g)</span>
                    <input value={nutrition.gramsTotalSugars}
                            onChange={handleChange("gramsTotalSugars")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsTotalSugars-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Dietary fiber (g)</span>
                    <input value={nutrition.gramsDietaryFiber}
                            onChange={handleChange("gramsDietaryFiber")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsDietaryFiber-input" />
                </div>
            </div>

            <div className="d-flex flex-column row-gap-1">
                <div className="input-group">
                    <span className="input-group-text">Total fat (g)</span>
                    <input value={nutrition.gramsTotalFat}
                            onChange={handleChange("gramsTotalFat")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsTotalFat-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Saturated fat (g)</span>
                    <input value={nutrition.gramsSaturatedFat}
                            onChange={handleChange("gramsSaturatedFat")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsSaturatedFat-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Trans fat (g)</span>
                    <input value={nutrition.gramsTransFat}
                            onChange={handleChange("gramsTransFat")}
                            type="number"
                            step="any" className="form-control"
                            id="gramsTransFat-input" />
                </div>
            </div>
            <div className="d-flex flex-column row-gap-1">
                <div className="input-group">
                    <span className="input-group-text">Sodium (mg)</span>
                    <input value={nutrition.milligramsSodium}
                            onChange={handleChange("milligramsSodium")}
                            type="number"
                            step="any" className="form-control"
                            id="milligramsSodium-input" />
                </div>
                <div className="input-group">
                    <span className="input-group-text">Cholesterol</span>
                    <input value={nutrition.gramsTransFat}
                            onChange={handleChange("milligramsCholesterol")}
                            type="number"
                            step="any" className="form-control"
                            id="milligramsCholesterol-input" />
                </div>
            </div>
        </div>
    </div>
}
