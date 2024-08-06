import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";

import FoodsService from "../services/FoodsService";

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

    return <>
        <h1>{food.name}</h1>

        <p>
            {food.description}
        </p>

        <p>
            Quantity: {food.quantity?.amount} {food.quantity?.unitName}
        </p>

        <div>
            <table>
                <thead>
                    <tr>
                        <th scope="col">
                            Calories
                        </th>
                    </tr>
                </thead>

                <tbody>
                    <tr>
                        <td>
                            {food.calories}
                        </td>
                    </tr>
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