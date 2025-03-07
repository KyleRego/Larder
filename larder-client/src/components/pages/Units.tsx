import { Link } from "react-router-dom";
import UnitsTable from "../tables/UnitsTable";
import { useContext, useEffect, useState } from "react";
import { UnitsContext } from "../../contexts/UnitsContext";
import { UnitDto } from "../../types/dtos/UnitDto";
import { apiClient } from "../../util/axios";
import { UnitSortOptions } from "../../types/UnitSortOptions";
import SearchBox from "../SearchBox";
import ActionBar from "../layout/ActionBar";
import BreadCrumbs from "../layout/Breadcrumbs";

export default function Units() {
    const { units, setUnits } = useContext(UnitsContext);
    const [sortOrder, setSortOrder] = useState(UnitSortOptions.Name);
    const [searchParam, setSearchParam] = useState("");

    useEffect(() => {
        async function getUnits() {
            const response = await apiClient.get<UnitDto[]>("api/units", 
                { params: {sortOrder: sortOrder, search: searchParam} })

            setUnits(response.data);
        }

        getUnits();
    }, [sortOrder, searchParam])

    function handleSearchChange(e: React.ChangeEvent<HTMLInputElement>) {
        setSearchParam(e.currentTarget.value);
    }

    return (
        <div className="h-100 d-flex flex-column">
            <BreadCrumbs>
                <li className="breadcrumb-item active">
                    <h1 className="fs-6 d-inline">
                        Units
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="container my-2 d-flex">
                <SearchBox handleOnChange={handleSearchChange} /> 
            </div>

            <div className="container flex-grow-1">
                <UnitsTable units={units} sortOrder={sortOrder} setSortOrder={setSortOrder} />
            </div>

            <ActionBar>
                <div className="d-flex justify-content-center">
                    <Link className="btn btn-secondary border-black" to={"/units/new"}>New unit</Link>
                </div>
            </ActionBar>
        
        </div>
    );
}
