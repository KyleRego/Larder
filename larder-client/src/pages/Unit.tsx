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
import BreadCrumbs from "../Breadcrumbs";
import ActionBar from "../ActionBar";
import UnitCard from "../components/cards/UnitCard";

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
        <div className="d-flex flex-column h-100">
            <BreadCrumbs>
                <li className="breadcrumb-item" aria-current="page">
                    <Link to={"/units"}>Units</Link>
                </li>
                <li className="breadcrumb-item active">
                    <h1 className="fs-6 d-inline">
                        {unit.name}
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="my-4 container flex-grow-1">
                <UnitCard unit={unit} />

                <div className="mt-4">
                    <h2 className="fs-3">Unit Conversions:</h2>

                    <div className="mt-4 d-flex flex-column align-items-start">
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
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <Link className="btn btn-outline-light border-black text-black"
                        to={`/units/${unit.id}/edit`} title={`Edit ${unit.name}`}>
                            {`Edit ${unit.name}`}
                    </Link>
                </div>
            </ActionBar>
        </div>
    );
}
