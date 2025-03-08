import { useEffect, useState } from "react";
import { UnitDto } from "../../types/dtos/UnitDto";
import { useParams } from "react-router";
import Loading from "../Loading";
import { Link } from "react-router-dom";
import { useApiRequest } from "../../hooks/useApiRequest";
import BreadCrumbs from "../layout/Breadcrumbs";
import ActionBar from "../layout/ActionBar";
import UnitCard from "../cards/UnitCard";

export default function UnitPage() {
    const [unit, setUnit] = useState<UnitDto | null>(null);
    const { id } = useParams<{ id: string }>();
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        async function getUnit() {
            const res = await handleRequest<UnitDto>({
                method: "get",
                url: `/api/units/${id}`
            });

            if (res) {
                setUnit(res);
            }
        }

        getUnit();
    }, [id]);

    if (unit === null) {
        return <Loading />
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

            <div className="flex-grow-1 container my-4">
                <UnitCard unit={unit} />
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
