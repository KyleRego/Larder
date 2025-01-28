import { QuantityDto } from "./QuantityDto"

export type NutritionDto = {
    servingSize: QuantityDto,
    calories: number,
    gramsProtein: number,
    gramsTotalFat: number,
    gramsSaturatedFat: number,
    gramsTransFat: number,
    milligramsCholesterol: number,
    milligramsSodium: number,
    gramsTotalCarbs: number,
    gramsDietaryFiber: number,
    gramsTotalSugars: number
}
