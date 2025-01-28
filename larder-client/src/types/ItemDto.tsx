import { NutritionDto } from "./NutritionDto";
import { IngredientDto } from "./Ingredient";
import { QuantityDto } from "./QuantityDto";

export type ItemDto = {
    id: string | null,
    name: string,
    amount: number,
    description: string | null,
    nutrition: NutritionDto | null,
    ingredient: IngredientDto | null,
    quantity: QuantityDto | null
}
