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
        if (window.confirm(`Are you sure you want to delete unit "${unit.name}"`)) {
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
        <div className="card mt-4">
            <div className="card-body">
                <div className="mt-0 mb-4 d-flex flex-wrap justify-content-between align-items-center">
                    <div className="d-flex align-items-center column-gap-3">
                        <h1 className="m-0">{unit.name}</h1>

                        <Link className="btn btn-primary btn-sm" to={`/units/${id}/edit`}>Edit</Link>
                    </div>

                    <button onClick={handleDelete} type="button" className="btn btn-danger btn-sm">Delete</button>
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
            </div>
        </div>

        <div>
            <Link to="/units">Back to units</Link>
        </div>
    </>
}