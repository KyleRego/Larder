import { FoodDto } from "./FoodDto";
import { IngredientDto } from "./Ingredient";
import { QuantityDto } from "./QuantityDto";

export type ItemDto = {
    id: string | null,
    name: string,
    amount: number,
    description: string | null,
    food: FoodDto | null,
    ingredient: IngredientDto | null,
    quantity: QuantityDto | null
}
