export default function LabeledInput({inputName, labelText, initialValue,
                                       afterInputText="", inputType="text", required=false})
{
    return <div className="d-flex flex-wrap column-gap-1 align-items-center">
                <label htmlFor={inputName}>
                    {labelText}
                </label>

                <input  className="flex-grow-1"
                        name={inputName}
                        defaultValue={initialValue}
                        required={required}
                        type={inputType}
                        step={"any"} >
                </input>

                {afterInputText !== ""
                ?
                <span>
                    {afterInputText}
                </span>
                : ""}
            </div>;
}
