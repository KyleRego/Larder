import { useEffect, useState } from "react";
import { Unit } from "../types/Unit";
import { apiClient } from "../util/axios";
import { useParams } from "react-router";
import { MdModeEdit } from "react-icons/md";
import Loading from "../components/Loading";
import { Link } from "react-router-dom";

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

                <Link to={`/units/${unit.id}/edit`} title="Edit unit"
                        type="button" className="btn btn-sm btn-outline-primary">
                    <MdModeEdit className="icon" />
                </Link>
            </div>

            <div className="mt-4">
                <h2>Type: {Unit.getType(unit)}</h2>
            </div>
        </>
    );
}