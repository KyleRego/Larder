import { ReactNode } from "react";
import { ItemDto } from "../types/Item";

export default function ItemsTable({items} : {items: ItemDto[]}) {
    const itemRows = items.map(item => {
        return <ItemRow key={item.id} item={item} />
    })
    
    return (
        <table className="table table-striped text-break">
            <caption>
                Your items
            </caption>
            <thead>
                <tr>
                    <th scope="col">Name</th>
                    <th scope="col">Amount</th>
                    <th scope="col">Description</th>
                </tr>
            </thead>
            <tbody>
                {itemRows}
            </tbody>
        </table>
    );
}

function ItemRow({item} : {item: ItemDto}) : ReactNode {
    return (
        <tr>
            <th scope="row">{item.name}</th>
            <td>{item.amount}</td>
            <td>{item.description}</td>
        </tr>
    );
}
