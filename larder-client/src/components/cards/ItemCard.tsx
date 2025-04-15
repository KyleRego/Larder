import { ItemDto } from "../../types/dtos/ItemDto";
import QuantitySpan from "../QuantitySpan";

export default function ItemCard({ item }: { item: ItemDto }) {
    const imageUrl = `${import.meta.env.VITE_API_ORIGIN}/api/Items/${item.id}/image`;

    return (
        <div
            style={{
                position: "relative",
                width: "128px",
                height: "128px",
                overflow: "hidden",
                borderRadius: "8px",
                boxShadow: "0 0 8px rgba(0, 0, 0, 0.1)",
            }}
        >
            <img
                src={imageUrl}
                onError={(e) => {
                    e.currentTarget.onerror = null;
                    e.currentTarget.src = "/default-item-image.png";
                }}
                alt="Item"
                style={{
                    width: "100%",
                    height: "100%",
                    objectFit: "cover",
                    display: "block",
                }}
            />
            <div
                style={{
                    position: "absolute",
                    top: 0,
                    left: 0,
                    right: 0,
                    bottom: 0,
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "flex-end",
                    padding: "0.5rem",
                    background: "linear-gradient(to top, rgba(0,0,0,0.6), rgba(0,0,0,0))",
                    color: "white",
                }}
            >
                <div
                    style={{
                        fontWeight: "bold",
                        fontSize: "1.1rem",
                        textShadow: "0 0 4px rgba(0, 0, 0, 0.8)",
                        wordBreak: "break-word",
                    }}
                >
                    {item.name}
                </div>

                {item.quantity && (
                    <div
                        style={{
                            fontSize: "0.9rem",
                            marginTop: "0.25rem",
                            textShadow: "0 0 4px rgba(0, 0, 0, 0.8)",
                        }}
                    >
                        Qty: <QuantitySpan quantity={item.quantity} />
                    </div>
                )}

                {item.description && (
                    <div
                        style={{
                            fontSize: "0.85rem",
                            marginTop: "0.25rem",
                            fontStyle: "italic",
                            textShadow: "0 0 4px rgba(0, 0, 0, 0.8)",
                        }}
                    >
                        {item.description}
                    </div>
                )}
            </div>
        </div>
    );
}
