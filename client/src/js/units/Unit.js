import { useState, useEffect, useContext } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";

import UnitsService from "../services/UnitsService"
import UnitHelpers from "./UnitHelpers";
import UnitConversion from "./UnitConversion";
import TargetUnitConversion from "./TargetUnitConversion";
import NewUnitConversion from "./NewUnitConversion";
import { AlertContext } from "../../AlertContext";

export default function Unit()
{
    const navigate = useNavigate();
    const { setAlertMessage } = useContext(AlertContext);
    const [newingConversion, setNewingConversion] = useState(false);
    let { id } = useParams();
    const [unit, setUnit] = useState(null);

    useEffect(() => {
        const service = new UnitsService();

        service.getUnit(id).then(result => {
            setUnit(result);
        });
    }, [id]);

    function handleDelete() {
        if (window.confirm(`Are you sure you want to delete unit "${unit.name}"?`)) {
            const service = new UnitsService();

            service.deleteUnit(unit.id).then(() => {
                setAlertMessage(`Unit "${unit.name}" was deleted.`);
                navigate("/units");
            })
        }
    }

    if (unit === null) return <h1>Loading...</h1>

    const unitConversions = unit.conversions.map(uc => {
        return <UnitConversion unit={unit} setUnit={setUnit} unitConversion={uc} key={uc.id} />
    });
    const unitTargetConversions = unit.targetConversions.map(uc => {
        return <TargetUnitConversion targetUnit={unit} unitConversion={uc} key={uc.id} />
    });

    return <>
        <div className="mb-4 d-flex column-gap-5 flex-wrap row-gap-1 align-items-center">
            <h1 className="m-0">Unit: {unit.name}</h1>

            <div className="d-flex align-items-center column-gap-3">
                <Link className="btn btn-primary" title="Edit unit" to={`/units/${id}/edit`}>Edit</Link>

                <button onClick={handleDelete} title="Delete unit" type="button" className="btn btn-danger">Delete</button>
            </div>
        </div>

        <p>
            Type: {UnitHelpers.UnitTypeEnumValueToText(unit.type)}
        </p>

        <h2>Conversions:</h2>

        <div className="mt-0 mb-4">
        {unitConversions}

        {unitTargetConversions}

            <div className="mt-4">
            { newingConversion === false 
            ?
                <button onClick={() => setNewingConversion(true)} type="button" className="btn btn-primary btn-sm">
                    New conversion
                </button>
            :
                <NewUnitConversion unit={unit}
                                    setUnit={setUnit}
                                    setNewingConversion={setNewingConversion} />
            }
            </div>  
        </div>

        <div>
            <Link to="/units">Back to units</Link>
        </div>
    </>
}