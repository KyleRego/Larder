import { QuantityDto } from "./QuantityDto"

export type FoodDto = {
    servings: Number,
    servingSize: QuantityDto,
    calories: Number,
    gramsProtein: Number,
    gramsTotalFat: Number,
    gramsSaturatedFat: Number,
    gramsTransFat: Number,
    milligramsCholesterol: Number,
    milligramsSodium: Number,
    gramsTotalCarbs: Number,
    gramsDietaryFiber: Number,
    gramsTotalSugars: Number,

    totalCalories: Number,
    totalGramsProtein: Number
}
