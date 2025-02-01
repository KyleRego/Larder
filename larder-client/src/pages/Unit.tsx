import { useEffect, useState } from "react";
import { UnitDto } from "../types/UnitDto";
import { apiClient } from "../util/axios";
import { useParams } from "react-router";
import Loading from "../components/Loading";
import EditLink from "../components/EditLink";
import UnitConversionForm from "../components/UnitConversionForm";
import { UnitConversionDto } from "../types/UnitConversionDto";
import UnitConversionDiv from "../components/UnitConversion";
import { Link } from "react-router-dom";
import { useApiRequest } from "../hooks/useApiRequest";

export default function UnitPage() {
    const [unit, setUnit] = useState<UnitDto | null>(null);
    const [adding, setAdding] = useState<Boolean>(false)
    const { id } = useParams<{ id: string }>();
    const [refreshCounter, setRefreshCounter] = useState(0);
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        apiClient.get<UnitDto>(`/api/units/${id}`).then(res => {
            setUnit(res.data);
        }).catch(error => {
            console.error(error);
        })
    }, [refreshCounter]);

    if (unit === null) {
        return <Loading />
    }

    const unitConversions = unit.conversions.map(uc => {
        return (
            <div key={uc.id} className="mt-2">
                <UnitConversionDiv unitConversion={uc} parentRefresh={() => setRefreshCounter(refreshCounter + 1)} />
            </div>
        );
    });

    async function handleCreateConversion(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);
        const targetUnitsPerUnit = parseFloat(formData.get("targetUnitsPerUnit") as string)

        const newUnitConversion: UnitConversionDto = {
            id: null,
            unitId: unit!.id!,
            targetUnitsPerUnit: targetUnitsPerUnit,
            targetUnitId: formData.get("targetUnitId") as string
        };

        const res = await handleRequest<UnitConversionDto>({
            method: "post",
            url: "/api/UnitConversions",
            data: newUnitConversion
        });

        if (res) {
            setAdding(false);
            setRefreshCounter(refreshCounter + 1);
        }
    }

    return (
        <>
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    <li className="breadcrumb-item" aria-current="page">
                        <Link to={"/units"}>Units</Link>
                    </li>
                    <li className="breadcrumb-item active">
                        {unit.name}
                    </li>
                </ol>
            </nav>

            <div className="d-flex align-items-center column-gap-3">
                <h1>{unit.name}</h1>

                <EditLink path={`/units/${unit.id}/edit`} title="Edit unit" />
            </div>

            <div className="mt-4">
                <p>Type: {UnitDto.getType(unit)}</p>
            </div>

            <div className="mt-4">
                <h2>Unit Conversions:</h2>

                <div className="mt-4 d-flex flex-column align-items-center">
                    {unitConversions}
                </div>

                <div className="mt-2 d-flex justify-content-start">
                { adding ? (
                    <UnitConversionForm handleSubmit={handleCreateConversion}
                                        initialUnitConversion={null}
                                        handleCancel={() => setAdding(false)} />
                ) : (
                    <button onClick={() => setAdding(true)} 
                            className="btn btn-outline-primary"
                            type="button">
                        Add conversion
                    </button> 
                )}
                </div>
            </div>
        </>
    );
}
