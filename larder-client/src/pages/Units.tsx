import { Link } from "react-router-dom";
import UnitsTable from "../components/UnitsTable";
import { useContext, useEffect } from "react";
import { UnitsContext } from "../contexts/UnitsContext";
import { Unit } from "../types/Unit";
import { apiClient } from "../util/axios";

export default function Units() {
    const { units, setUnits } = useContext(UnitsContext);

    useEffect(() => {
        apiClient.get<Unit[]>("api/units").then(res => {
            setUnits(res.data);
        }).catch(error => {
            console.error(error);
        })
    }, [])

    return (
        <>
            <div className="page-flex-header">
                <h1>Units</h1>

                <Link className="btn btn-primary" to={"/units/new"}>New unit</Link>
            </div>

            <div className="mt-4">
                <UnitsTable units={units} />
            </div>
        
        </>
    );
}
