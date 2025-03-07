export type QuantityDto = {
    amount: number,
    unitId: string | null,
    unitName: string | null
}

export function formatQuantity(quantity: QuantityDto): string {
    if (quantity.unitName === null) {
        return quantity.amount.toString();
    }

    return `${quantity.amount} ${quantity.unitName}`;
}
