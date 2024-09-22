import { useState, useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthedContext } from "../../AuthedContext";
import IdentityService from "./IdentityService";

export default function Register()
{
    const { setAuthed } = useContext(AuthedContext);
    const navigate = useNavigate();
    const [errors, setErrors] = useState([]);

    function handleSubmitRegister(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const email = formData.get("email");
        const password = formData.get("password");

        const service = new IdentityService();

        service.postRegister(email, password).then(async result => {
            if (result.status === 400) {
                const json = await result.json();
                const tmpErrors = [];
                const keys = Object.keys(json.errors);
                for (let i = 0; i < keys.length; i += 1)
                {
                    const errorMessage = json.errors[keys[i]][0];
                    tmpErrors.push(errorMessage);
                }
                setErrors(tmpErrors);
            }
            else if (result.status === 200) {
                setAuthed(true);
                setErrors([]);
                navigate("/");
            }
            else {
                throw new Error(`Response status: ${result.status}`);
            }
        }).catch((error) => {
            console.error(error);
        });
    }

    const errorsInfo = <ol>
        {errors.map(er => <li key={er}>{er}</li>)}
    </ol>

    return <>
        <h1 className="mt-2 mb-4 text-center">Register:</h1>

        <div>
            {errorsInfo}
        
            <form onSubmit={handleSubmitRegister}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address:</label>
                    <input required autoComplete="username" id="email" type="email" className="form-control" name="email" aria-describedby="emailHelp" />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password:</label>
                    <input required autoComplete="new-password" id="password" type="password" className="form-control" name="password" />
                </div>

                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </div>

        <p className="text-center pt-4">
            Would you like to <Link to={"/login"}>login</Link> instead?
        </p>
    </>;
}
