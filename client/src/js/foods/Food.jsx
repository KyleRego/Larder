import { useState, useEffect, useContext } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";

import EatFoodForm from "./EatFoodForm";

import FoodsService from "../services/FoodsService";
import { AlertContext } from "../../AlertContext";

export default function Food() {
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    let { id } = useParams();
    const [food, setFood] = useState(null);

    useEffect(() =>
    {
        const service = new FoodsService();

        service.getFood(id).then(result => {
            setFood(result.data);
        }).catch(error => {
            console.log(error);
        });
    }, [id]);

    if (food === null) return <h1>Loading...</h1>;

    function handleDeleteFood()
    {
        if (window.confirm(`Are you sure you want to delete this food (${food.name})?`))
        {
            const service = new FoodsService();

            service.deleteFood(food).then(() => {
                setAlertMessage(`Food "${food.name}" was deleted.`);
                navigate("/foods");
            });
        }
    }

    return <>
        <div className="mt-2 mb-4 d-flex justify-content-around align-items-center">
            <h1 className="m-0">{food.name}</h1>

            <div className="d-flex align-items-center column-gap-3">
                <Link role="button" title="Edit food" to={`/foods/${id}/edit`} className="btn btn-primary">Edit</Link>

                <button type="button" title="Delete food" className="btn btn-danger" onClick={handleDeleteFood}>Delete</button>
            </div>
        </div>

        <div className="mb-4">
            <p className="mb-2">{food.description}</p>
            <p className="mb-2">Servings: {food.servings}</p>
            <div className="">
                <EatFoodForm food={food} setFood={setFood} />
            </div>
        </div>

        <div>
            <h2>
                Nutrition per serving:
            </h2>
            <table className="table">
                <tbody>
                    <tr>
                        <th scope="row">Calories</th>
                        <td><span>{food.calories}</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Protein</th>
                        <td><span>{food.gramsProtein} g</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Saturated Fat</th>
                        <td><span>{food.gramsSaturatedFat} g</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Trans Fat</th>
                        <td><span>{food.gramsTransFat} g</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Cholesterol</th>
                        <td><span>{food.milligramsCholesterol} mg</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Sodium</th>
                        <td><span>{food.milligramsCholesterol} mg</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Total Carbs</th>
                        <td><span>{food.gramsTotalCarbs} g</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Dietary Fiber</th>
                        <td><span>{food.gramsDietaryFiber} g</span></td>
                    </tr>
                    <tr>
                        <th scope="row">Total Sugars</th>
                        <td><span>{food.gramsTotalSugars} g</span></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div>
            <Link to="/foods">Back to foods</Link>
        </div>
    </>
}
