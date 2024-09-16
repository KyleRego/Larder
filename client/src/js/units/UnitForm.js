import UnitHelpers from "./UnitHelpers";

export default function UnitForm({initialUnit, handleSubmit}) {
    const buttonText = (initialUnit == null) ? "Create unit" : "Update unit";

    return <form onSubmit={handleSubmit}>
        <div className="d-flex column-gap-3 flex-wrap row-gap-1 align-items-center">
            <div className="d-flex flex-wrap column-gap-1 align-items-center">
                <label htmlFor="name">Unit name:</label>
                <input id="name" name="name" type="text" defaultValue={initialUnit?.name}></input>
            </div>

            <div className="d-flex flex-wrap column-gap-1 align-items-center">
                <label htmlFor="type">Type:</label>
                <select id="type" name="type">
                    <option value="0">
                        {UnitHelpers.UnitTypeEnumValueToText(0)}
                    </option>
                    <option value="1">
                        {UnitHelpers.UnitTypeEnumValueToText(1)}
                    </option>
                    <option value="2">
                        {UnitHelpers.UnitTypeEnumValueToText(2)}
                    </option>
                </select>
            </div>

            <button className="btn btn-primary" type="submit">{buttonText}</button>
        </div>
    </form>;
}