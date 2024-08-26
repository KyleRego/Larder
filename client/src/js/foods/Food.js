import { useState, useEffect } from "react";
import { useParams, Link, useOutletContext } from "react-router-dom";

import FoodsService from "../services/FoodsService";
import FoodConstants from "./FoodConstants";
import FoodCard from "./FoodCard";

export default function Food()
{
    const [setToastMessage, setShowToast] = useOutletContext();

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

    const quantityRows = FoodConstants.quantityProperties.filter(foodProperty => {
        // A food does not have every property so filter down to ones it has
        return food[foodProperty];
    }).map(foodProperty => {
        return <tr key={foodProperty}>
            <th scope="row">{foodProperty}</th>
            <td>{food[foodProperty].amount} {food[foodProperty].unitName}</td>
        </tr>
    });

    function handleDeleteFood()
    {
        if (window.confirm(`Are you sure you want to delete this food (${food.name})?`))
        {
            const service = new FoodsService();

            service.deleteFood(food).then(() => {
                setToastMessage("Food was successfully deleted");
                setShowToast(true);
            });
        }
    }

    return <>
        <div className="d-flex column-gap-3 flex-wrap align-items-center">
            <h1>{food.name}</h1>
            <div>
                <button className="btn btn-danger" onClick={handleDeleteFood}>Delete food</button>
            </div>
        </div>

        <FoodCard food={food} setFood={setFood} />

        <div>
            <table className="table">
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
