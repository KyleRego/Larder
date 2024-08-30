import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";

import UnitsService from "../services/UnitsService"
import UnitHelpers from "./UnitHelpers";
import UnitConversionForm from "./UnitConversionForm";

export default function Unit()
{
    let { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(result => {
            setUnit(result);
        });
    }, [id]);

    if (unit === null) return <h1>Loading...</h1>

    return <>
        <h1>{unit.name}</h1>

        <p>
            Type: {UnitHelpers.UnitTypeEnumValueToText(unit.type)}
        </p>

        <UnitConversionForm unit={unit} />

        <div>
            <Link to={`/units/${id}/edit`}>Edit unit</Link>
        </div>

        <div>
            <Link to="/units">Back to units</Link>
        </div>
    </>
}