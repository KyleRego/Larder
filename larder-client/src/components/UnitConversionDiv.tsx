import { ReactNode, useContext, useState } from "react";
import { UnitConversion } from "../types/UnitConversion";
import { UnitsContext } from "../contexts/UnitsContext";
import { MdEdit } from "react-icons/md";
import UnitConversionForm from "./UnitConversionForm";
import { Unit } from "../types/Unit";
import Loading from "./Loading";
import { apiClient } from "../util/axios";
import { ApiResponseType } from "../types/ApiResponse";
import { MessageContext } from "../contexts/MessageContext";

export default function({unitConversion} : {unitConversion: UnitConversion}): ReactNode {
    const { units } = useContext(UnitsContext);
    const { setMessage } = useContext(MessageContext);
    const [editing, setEditing] = useState<boolean>(false);

    const unitId = unitConversion.unitId;
    const targetUnitId = unitConversion.targetUnitId;
    const targetUnitsPerUnit = unitConversion.targetUnitsPerUnit;

    let unit : Unit | undefined;
    let targetUnit : Unit | undefined;

    for (const u of units) {
        if (!unit && u.id === unitId) {
            unit = u
        } else if (!targetUnit && u.id == targetUnitId) {
            targetUnit = u
        }

        if (unit && targetUnit) {
            break;
        }
    }

    if (!unit || !targetUnit) {
        return <Loading />
    }

    async function handleUpdate(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);
        const targetUnitsPerUnit = parseFloat(formData.get("targetUnitsPerUnit") as string)

        const newUnitConversion: UnitConversion = {
            id: unitConversion.id,
            unitId: unit!.id!,
            targetUnitsPerUnit: targetUnitsPerUnit,
            targetUnitId: formData.get("targetUnitId") as string
        };

        const response = await apiClient.put(`/api/UnitConversions/${unitConversion.id}`, newUnitConversion);
        setMessage({text: response.data.message, type: ApiResponseType.Success});
        setEditing(false);
    }
    
    return (
        <>
            { editing === false ? (
                <div className="d-flex align-items-center column-gap-3">
                    <div>1 {unit.name} = {targetUnitsPerUnit} {targetUnit.name}</div>

                    <div>
                        <button onClick={() => setEditing(true)}
                                className="btn btn-sm btn-secondary"
                                type="button" title="Edit unit conversion">
                            <MdEdit className="icon-sm" />
                        </button>
                    </div>
                </div>
            ) : (
            <div>
                <UnitConversionForm unit={unit} handleSubmit={handleUpdate}
                            unitConversion={unitConversion}
                            handleCancel={() => setEditing(false)} />
            </div>
            )}
        </>
    );
}