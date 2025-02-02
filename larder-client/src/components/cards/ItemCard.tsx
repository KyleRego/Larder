import { ItemDto } from "../../types/ItemDto";
import EditLink from "../EditLink";
import QuantitySpan from "../QuantitySpan";

export default function ItemCard({item} : {item: ItemDto}) {
    return <div className="card" style={{maxWidth: "424px", wordBreak: "break-all"}}>
                <div className="card-body">
                    <div className="d-flex column-gap-1 align-items-start">
                    <h1 className="fs-3">
                        <div className="d-flex flex-column column-gap-1 row-gap-3 flex-wrap">
                            <span> {item.name}</span>
                                <span className="fs-4"> Quantity: <QuantitySpan quantity={item.quantity!} /> </span>
                                    <EditLink   path={`/items/${item.id}/edit`}
                                                title={`Edit ${item.name}`} />
                        </div>
                    </h1>
                    
                    </div>
                </div>
            </div>;
}