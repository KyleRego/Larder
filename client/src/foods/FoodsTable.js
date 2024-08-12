import { Link } from "react-router-dom";

import SortingTableHeader from "../components/SortingTableHeader";

import FoodsService from "../services/FoodsService";
import { useState } from "react";

import { CiEdit } from "react-icons/ci";
import { MdDone } from "react-icons/md";

import "./FoodsTable.css";

// setFoods is passed in so that when the food amount is edited in cell,
// the entire foods state can be updated, to change the amount of that one food
export default function FoodsTable({foods, setFoods, sortOrder, setSortOrder})
{
    let rows = foods.map(food => FoodRow(food, foods, setFoods));

    return <>
        <table className="foodsTable">
            <caption>Servings of ready to eat food.</caption>
            <thead>
                <tr>
                    <SortingTableHeader columnName="Name" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <SortingTableHeader columnName="Amount" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                    <SortingTableHeader columnName="Calories" sortOrder={sortOrder} setSortOrder={setSortOrder} />
                </tr>
            </thead>

            <tbody>
                {rows}
            </tbody>
        </table>
    </>
}

function FoodRow(food, foods, setFoods)
{
    return (
        <tr key={food.id}>
            <th scope="row">
                <Link to={`/foods/${food.id}`}>
                    {food.name}
                </Link>
            </th>

            <FoodAmountTableCell food={food} foods={foods} setFoods={setFoods} />

            <td>{food.calories}</td>
        </tr>
    );
}

function FoodAmountTableCell({food, foods, setFoods})
{
    const amount = food.amount;
    const [editing, setEditing] = useState(false);

    async function handleSubmit(e)
    {
        e.preventDefault();

        const formData = new FormData(e.target);
        const newAmount = formData.get("amount");

        const quantityDto = {
            id: food.id,
            amount: newAmount
        };

        const service = new FoodsService();
        await service.patchFood(quantityDto).then(() => {
            const newFoods = structuredClone(foods);
            
            for (let i = 0; i < newFoods.length; i += 1)
            {
                if (newFoods[i].id === food.id)
                {
                    newFoods[i].amount = newAmount
                }
            }

            setFoods(newFoods);
            setEditing(false);
        });
    }

    if (editing === true)
    {
        return <td className="py-0">
            <form onSubmit={handleSubmit}>
                <div className="flex column-gap-3 align-items-center m-0">
                    <label hidden htmlFor="amount"></label>
                    <input name="amount" type="number" defaultValue={amount}></input>
                    <button type="submit" title="Done">
                        <MdDone />
                    </button>

                    <button type="button" onClick={() => setEditing(false)} title="Cancel">
                        Cancel
                    </button>
                </div>
            </form>
        </td>
    }
    else
    {
        return <td className="py-0">
            <div className="m-0 flex column-gap-3 align-items-center">
                <span>{amount}</span>
                <CiEdit className="w-5 h-5 cursor-pointer" onClick={() => setEditing(true)} title="Edit" />
            </div>
        </td>
    }
}