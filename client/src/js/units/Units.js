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
            <h1>
                Units
            </h1>

            <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />

            <Link to="/units/new">New unit</Link>
        </>;
}
