import { useContext } from "react";
import { QuantityDto } from "../types/QuantityDto";
import { UnitsContext } from "../contexts/UnitsContext";

export default function QuantitySpan({quantity} : {
    quantity: QuantityDto
}) {
    if (quantity.unitId) {
        const { units } = useContext(UnitsContext);

        for (const u of units) {
            if (!quantity.unitName && u.id === quantity.unitId) {
                quantity.unitName = u.name
            }

            if (quantity.unitName) {
                break;
            }
        }
    }

    var text: string = `${quantity.amount}`;
    if (quantity.unitName) text += ` ${quantity.unitName}`;

    return <span>{text}</span>;
}
