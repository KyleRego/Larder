import { QuantityDto } from "./QuantityDto"

export type FoodDto = {
    servingsPerItem: number,
    servings: number,
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
    gramsTotalSugars: number,

    totalCalories: number,
    totalGramsProtein: number
}
