export default function UnitSelectOptions({units})
{
    const noUnitOption = <option value="">No unit</option>

    return (
        <>
            {noUnitOption}

            {units.map(u => {
                return <option key={u.id} value={u.id}>
                    {u.name}
                </option>
            })}
        </>    
    )
    
}