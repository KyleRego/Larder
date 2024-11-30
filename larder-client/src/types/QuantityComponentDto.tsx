import { QuantityDto } from "./QuantityDto"

export type QuantityComponentDto = {
    quantity: QuantityDto | null;
    quantityPerItem: QuantityDto | null;
}
