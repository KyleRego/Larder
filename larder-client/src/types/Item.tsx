import { FoodDto } from "./FoodDto"
import { IngredientDto } from "./Ingredient"
import { QuantityComponentDto } from "./QuantityComponentDto"

export type ItemDto = {
    id: string | null,
    name: string,
    amount: string,
    description: string | null,
    food: FoodDto | null,
    ingredient: IngredientDto | null,
    quantityComp: QuantityComponentDto | null
}
