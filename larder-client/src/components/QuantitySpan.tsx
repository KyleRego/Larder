import { useContext } from "react";
import { QuantityDto } from "../types/dtos/QuantityDto";
import { UnitsContext } from "../contexts/UnitsContext";

export default function QuantitySpan({quantity} : {
    quantity: QuantityDto
}) {
    if (quantity.unitId) {
        const { units } = useContext(UnitsContext);

        for (const u of units) {
            if (quantity.unitName) {
                break;
            }

            if (u.id === quantity.unitId) {
                quantity.unitName = u.name
            }
        }
    }

    let text: string = `${quantity.amount}`;
    if (quantity.unitName) text += ` ${quantity.unitName}`;

    return <span>{text}</span>;
}
