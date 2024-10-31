export default function SearchBox({handleOnChange} 
    : {handleOnChange : React.ChangeEventHandler<HTMLInputElement>}) {
    return (
        <div className="d-flex flex-column align-items-start">
            <label htmlFor="search">Search:</label>
            <input id="search"
                    className="form-control-sm"
                    type="search" 
                    onChange={handleOnChange}
            />
        </div>
    );
}