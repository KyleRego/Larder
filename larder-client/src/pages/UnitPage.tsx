import { useEffect, useState } from "react";
import { Unit } from "../types/Unit";
import { apiClient } from "../util/axios";
import { useParams } from "react-router";
import Loading from "../components/Loading";
import EditLink from "../components/EditLink";

export default function UnitPage() {
    const [unit, setUnit] = useState<Unit | null>(null);
    const { id } = useParams<{ id: string }>();

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

    return (
        <>
            <div className="page-flex-header">
                <h1>{unit.name}</h1>

                <EditLink path={`/units/${unit.id}/edit`} title="Edit unit" />
            </div>

            <div className="mt-4">
                <h2>Type: {Unit.getType(unit)}</h2>
            </div>
        </>
    );
}