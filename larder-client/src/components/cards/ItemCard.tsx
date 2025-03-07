import { ItemDto } from "../../types/dtos/ItemDto";
import QuantitySpan from "../QuantitySpan";

export default function ItemCard({item} : {item: ItemDto}) {
    return <div className="card" style={{maxWidth: "243px", wordBreak: "break-all"}}>
                <div className="card-body">
                    <h2 className="card-title fs-5">
                        {item.quantity && <QuantitySpan quantity={item.quantity} />} {item.name}
                    </h2>
                    <p className="card-text">
                        {item.description}
                    </p>
                </div>
            </div>;
}
