import { Dispatch, SetStateAction, useState } from "react";
import ItemsTable from "../tables/ItemsTable";
import { Link } from "react-router-dom";
import SearchBox from "../SearchBox";
import FoodsTable from "../tables/FoodsTable";
import ActionBar from "../layout/ActionBar";
import BreadCrumbs from "../layout/Breadcrumbs";

enum TableVersions {
    AllItems = "All Items",
    Foods = "Foods",
    Ingredients = "Ingredients"
}

export default function Items() {
    const [currentTable, setCurrentTable] = useState<TableVersions>(TableVersions.AllItems);
    const [searchParam, setSearchParam] = useState("");

    function renderCurrentTable() {
        switch (currentTable) {
            case TableVersions.AllItems:
                return <ItemsTable searchParam={searchParam} />
            case TableVersions.Foods:
                return <FoodsTable searchParam={searchParam} />
            case TableVersions.Ingredients:
                return <div>Ingredients table placeholder</div>
        }
    }

    function handleSearchChange(e: React.ChangeEvent<HTMLInputElement>) {
        setSearchParam(e.currentTarget.value);
    }

    return (
        <div className="h-100 d-flex flex-column">
            <BreadCrumbs>
                <li className="breadcrumb-item active">
                    <h1 className="fs-6 d-inline">
                        Items
                    </h1>
                </li>
            </BreadCrumbs>

            <div className="container my-2 d-flex align-items-center column-gap-5 flex-wrap row-gap-1">
                <TableVersionDropdown currentVariant={currentTable} setCurrentTable={setCurrentTable} />

                <SearchBox handleOnChange={handleSearchChange} />
            </div>

            <div className="flex-grow-1 container">
                <div className="table-responsive  px-0">
                    {renderCurrentTable()}
                </div>
            </div>
            
            <ActionBar>
                <div className="d-flex justify-content-center">
                    <Link className="btn btn-outline-light" to={"/items/new"}>
                        New item
                    </Link>
                </div>
            </ActionBar>
        </div>
    );
}

function TableVersionDropdown({currentVariant, setCurrentTable}
                        : { currentVariant: TableVersions,
                            setCurrentTable: Dispatch<SetStateAction<TableVersions>> }) {
    const [expanded, setExpanded] = useState(false);

    const dropdownOptions = Object.values(TableVersions).filter((mem) => mem != currentVariant).map((mem, indx) => {
        return <li key={indx} onClick={() => {
            setCurrentTable(mem);
            setExpanded(false);
        }}>
            <a className="dropdown-item" href="#">{mem}</a>
        </li>;
    });

    return <div className="dropdown">
            <button className="btn btn-light dropdown-toggle border-black"
                    type="button" aria-expanded="false"
                onClick={() => setExpanded(!expanded)}    >
                {currentVariant}
            </button>
            <ul className={`dropdown-menu ${expanded === true ? "d-block" : "d-none"}`}>
                {dropdownOptions}
            </ul>
        </div>;
}
