import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import { UnitsContext } from "../../UnitsContext";
import FoodsService from "../services/FoodsService";
import FoodsTable from "./FoodsTable";
import { AlertContext } from "../../AlertContext";

export default function Foods() {
    const { setAlertMessage } = useContext(AlertContext);
    const { units } = useContext(UnitsContext)
    const [foods, setFoods] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        const service = new FoodsService();

        service.getFoods(sortOrder, searchParam).then(result => {
            setFoods(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });

    }, [sortOrder, searchParam, setAlertMessage]);

    return <>
        <div className="mb-4 mt-2 d-flex flex-wrap column-gap-1 row-gap-3 align-items-center justify-content-around">
            <h1>Your foods:</h1>

            <div className="d-flex flex-column align-items-start">
                <label htmlFor="search">Search:</label>
                <input id="search" className="form-control-sm"
                    type="search" onChange={(e) => setSearchParam(e.target.value)} />
            </div>

            <Link to="/foods/new" className="btn btn-primary" title="Add new food">New food</Link>
        </div>

        <div className="overflow-x-scroll">
            <FoodsTable foods={foods} setFoods={setFoods}
                    sortOrder={sortOrder} setSortOrder={setSortOrder} 
                    units={units} />
        </div>
    </>;
}
