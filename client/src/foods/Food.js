import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";
import FoodConstants from "./FoodConstants";

export default function Food()
{
    let { id } = useParams();
    const [food, setFood] = useState(null);

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFood(id).then(result => {
            setFood(result);
        }).catch(error => {
            console.log(error);
        });
    }, [id]);

    if (food === null) return <h1>Loading...</h1>;

    const quantityRows = FoodConstants.quantityProperties.map(propName => {
        return <tr key={propName}>
            <th scope="row">{propName}</th>
            <td>{food[propName].amount} {food[propName].unitName}</td>
        </tr>
    });

    return <>
        <h1>{food.name}</h1>

        <p>
            {food.description}
        </p>

        {(food.quantity) && 
        <p>
            Quantity: {food.quantity.amount} {food.quantity.unitName}
        </p>
        }

        <div>
            <table>
                <tbody>
                    <tr>
                        <th scope="row">Calories</th>
                        <td>{food.calories}</td>
                    </tr>
                    {quantityRows}
                </tbody>
            </table>
        </div>

        <div>
            <Link to={`/foods/${id}/edit`}>Edit food</Link>
        </div>

        <div>
            <Link to="/foods">Back to foods</Link>
        </div>
    </>
}