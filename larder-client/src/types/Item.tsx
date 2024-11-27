import { Food } from "./Food"
import { Ingredient } from "./Ingredient"

export type ItemDto = {
    id: string | null,
    name: string,
    amount: string,
    description: string | null,
    food: Food | null,
    ingredient: Ingredient | null
}
