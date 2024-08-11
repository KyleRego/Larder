import { useState, useEffect } from "react";
import { Link } from "react-router-dom";

import UnitsTable from "./UnitsTable";

import UnitsService from "../services/UnitsService";

import "./Units.css";

export default function Units()
{
    const [units, setUnits] = useState([]);
    const [sortOrder, setSortOrder] = useState("Name");

    useEffect(() =>
    {
        const service = new UnitsService();

        service.getUnits(sortOrder).then(result => {
            setUnits(result);
        })
    }, [sortOrder]);

    return (
        <>
            <h1>
                Units
            </h1>

            <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />

            <Link to="/units/new">New unit</Link>
        </>
    )
}
