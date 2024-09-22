import { useState, useEffect, useContext } from "react";
import { Link } from "react-router-dom";
import UnitsTable from "./UnitsTable";
import UnitsService from "../services/UnitsService";
import "./Units.css";
import { AlertContext } from "../../AlertContext";

export default function Units() {
    const { setAlertMessage } = useContext(AlertContext);
    const [units, setUnits] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        const service = new UnitsService();

        service.getUnits(sortOrder, searchParam).then(result => {
            setUnits(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        })
    }, [sortOrder, searchParam, setAlertMessage]);

    return <>
            <div className="mb-4 mt-2 d-flex flex-wrap column-gap-1 row-gap-3 align-items-center justify-content-around">
                <h1>Your units:</h1>

                <div className="d-flex flex-column align-items-start">
                    <label htmlFor="search">Search:</label>
                    <input id="search" className="form-control-sm"
                        type="search" onChange={(e) => setSearchParam(e.target.value)} />
                </div>

                <Link className="btn btn-primary" title="New unit" to="/units/new">New unit</Link>
            </div>

            <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
        </>;
}
