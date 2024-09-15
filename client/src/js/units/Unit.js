import { useState, useEffect } from "react";
import { Link, useParams } from "react-router-dom";

import UnitsService from "../services/UnitsService"
import UnitHelpers from "./UnitHelpers";
import UnitConversion from "./UnitConversion";
import NewUnitConversion from "./NewUnitConversion";

export default function Unit()
{
    const [newingConversion, setNewingConversion] = useState(false);
    let { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(result => {
            setUnit(result);
        });
    }, [id]);

    if (unit === null) return <h1>Loading...</h1>

    const unitConversions = unit.conversions.map(uc => {
        return <UnitConversion unit={unit} setUnit={setUnit} unitConversion={uc} key={uc.id} />
    });
    const unitTargetConversions = unit.targetConversions.map(uc => {
        return <UnitConversion unit={unit} setUnit={setUnit} unitConversion={uc} key={uc.id} />
    });

    return <>
        <h1>{unit.name}</h1>

        <p>
            Type: {UnitHelpers.UnitTypeEnumValueToText(unit.type)}
        </p>

        <h2>Conversions:</h2>

        {unitConversions}

        {unitTargetConversions}

        { newingConversion === false 
        ?
            <div>
                <button onClick={() => setNewingConversion(true)} type="button" className="btn btn-primary btn-sm">
                    New conversion
                </button>
            </div>   
        :
            <NewUnitConversion unit={unit}
                                setUnit={setUnit}
                                setNewingConversion={setNewingConversion} />
        }

        <div>
            <Link to={`/units/${id}/edit`}>Edit unit</Link>
        </div>

        <div>
            <Link to="/units">Back to units</Link>
        </div>
    </>
}