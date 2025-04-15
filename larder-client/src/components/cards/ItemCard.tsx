import { ItemDto } from "../../types/dtos/ItemDto";
import QuantitySpan from "../QuantitySpan";

export default function ItemCard({item} : {item: ItemDto}) {
    return <div className="card" style={{maxWidth: "243px", wordBreak: "break-all"}}>
                <div className="card-body d-flex flex-column row-gap-1">
                    <h2 className="fs-4">
                        {item.name}
                    </h2>

                    <h3 className="card-title fs-5">
                        {item.quantity && <QuantitySpan quantity={item.quantity} />}
                    </h3>

                    {item.description &&
                    <p className="card-text">
                        {item.description}
                    </p>
                    }

                    <div className="d-flex justify-content-center">
                        <img src={`${import.meta.env.VITE_API_ORIGIN}/api/Items/${item.id}/image`}
                            onError={(e) => {
                            e.currentTarget.onerror = null;
                            e.currentTarget.src = "/default-item-image.png";
                        }}
                            alt="Item Image" />
                    </div>
                </div>
            </div>;
}
