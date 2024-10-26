import { Food } from "./Food"
import { Ingredient } from "./Ingredient"

export type Item = {
    id: string | null,
    name: string,
    description: string | null,
    food: Food | null,
    ingredient: Ingredient | null
}
