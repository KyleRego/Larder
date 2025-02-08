import { NutritionDto } from "./NutritionDto";
import { IngredientDto } from "./Ingredient";
import { QuantityDto } from "./QuantityDto";
import { ConsumedTime } from "./ConsumedTime";

export type ItemDto = {
    id: string | null,
    name: string,
    description: string | null,
    nutrition: NutritionDto | null,
    ingredient: IngredientDto | null,
    quantity: QuantityDto | null,
    consumedTime: ConsumedTime | null
}
