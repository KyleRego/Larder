export default function UnitForm({initialUnit, handleSubmit})
{
    const buttonText = (initialUnit == null) ? "Create unit" : "Update unit";

    return <form onSubmit={handleSubmit}>
        <div>
            <label htmlFor="name">Unit name:</label>
            <input id="name" name="name" type="text" defaultValue={initialUnit?.name}></input>
        </div>
        
        <div>
            <label htmlFor="type">Type:</label>
            <select id="type" name="type">
                <option value="0">Mass</option>
                <option value="1">Volume</option>
                <option value="2">Weight</option>
            </select>
        </div>

        <button className="btn btn-primary" type="submit">{buttonText}</button>
    </form>
}