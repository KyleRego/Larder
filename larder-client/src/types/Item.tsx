import { FoodDto } from "./FoodDto"
import { Ingredient } from "./Ingredient"

export type ItemDto = {
    id: string | null,
    name: string,
    amount: string,
    description: string | null,
    food: FoodDto | null,
    ingredient: Ingredient | null
}
