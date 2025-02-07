export default function SearchBox({handleOnChange} 
    : {handleOnChange : React.ChangeEventHandler<HTMLInputElement>}) {
    return (
        <div className="form-floating">
            <input id="search"
                    className="form-control form-control-sm"
                    type="search" 
                    onChange={handleOnChange}
                    placeholder=""
            />
            <label htmlFor="search" className="form-label">Search:</label>
        </div>
    );
}