import { useContext, useEffect, useState } from "react";
import { Unit } from "../types/Unit";
import { apiClient } from "../util/axios";
import { useParams } from "react-router";
import Loading from "../components/Loading";
import EditLink from "../components/EditLink";
import UnitConversionForm from "../components/UnitConversionForm";
import { UnitConversion } from "../types/UnitConversion";
import { ApiResponseType } from "../types/ApiResponse";
import { MessageContext } from "../contexts/MessageContext";
import UnitConversionDiv from "../components/UnitConversionDiv";

export default function UnitPage() {
    const [unit, setUnit] = useState<Unit | null>(null);
    const [adding, setAdding] = useState<Boolean>(false)
    const { id } = useParams<{ id: string }>();
    const { setMessage } = useContext(MessageContext);

    useEffect(() => {
        apiClient.get<Unit>(`/api/units/${id}`).then(res => {
            setUnit(res.data);
        }).catch(error => {
            console.error(error);
        })
    }, []);

    if (unit === null) {
        return <Loading />
    }

    const unitConversions = unit.conversions.map(uc => {
        return (
            <div className="mt-2">
                <UnitConversionDiv key={uc.id} unitConversion={uc} />
            </div>
        );
    });

    async function handleCreateConversion(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);
        const targetUnitsPerUnit = parseFloat(formData.get("targetUnitsPerUnit") as string)

        const newUnitConversion: UnitConversion = {
            id: null,
            unitId: unit!.id!,
            targetUnitsPerUnit: targetUnitsPerUnit,
            targetUnitId: formData.get("targetUnitId") as string
        };

        const response = await apiClient.post("/api/UnitConversions", newUnitConversion);
        setMessage({text: response.data.message, type: ApiResponseType.Success});
        setAdding(false);
    }

    return (
        <>
            <div className="page-flex-header">
                <h1>{unit.name}</h1>

                <EditLink path={`/units/${unit.id}/edit`} title="Edit unit" />
            </div>

            <div className="mt-4">
                <p>Type: {Unit.getType(unit)}</p>
            </div>

            <div className="mt-4">
                <h2>Unit Conversions:</h2>

                <div className="mt-4">
                    {unitConversions}
                </div>

                <div className="mt-4">
                { adding ? (
                    <UnitConversionForm unit={unit} handleSubmit={handleCreateConversion}
                                unitConversion={null}
                                handleCancel={() => setAdding(false)} />
                ) : (
                    <button onClick={() => setAdding(true)} 
                            className="btn btn-secondary"
                            type="button">
                        Add conversion
                    </button> 
                )}
                </div>
            </div>
        </>
    );
}
