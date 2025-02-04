import { ItemDto } from "../../types/ItemDto";
import QuantitySpan from "../QuantitySpan";

export default function ItemCard({item} : {item: ItemDto}) {
    return <div className="card" style={{maxWidth: "243px", wordBreak: "break-all"}}>
                <div className="card-body">
                    <div className="text-center">
                        <h2 className="fs-3 text-center border-bottom pb-1 border-2 border-black">
                            {item.name}
                        </h2>
                        { item.quantity && 
                        <div className="fs-4" title="Quantity">
                            <QuantitySpan quantity={item.quantity} />
                        </div>
                        }
                    </div>
                </div>
            </div>;
}