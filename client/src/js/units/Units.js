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

    useEffect(() => {
        const service = new UnitsService();

        service.getUnits(sortOrder).then(result => {
            setUnits(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        })
    }, [sortOrder, setAlertMessage]);

    return <>
            <div className="mt-2 mb-4 d-flex justify-content-around align-items-center">
                <h1>Your units:</h1>

                <Link className="btn btn-primary" title="New unit" to="/units/new">New unit</Link>
            </div>

            <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
        </>;
}
