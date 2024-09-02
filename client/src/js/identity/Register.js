import { useState } from "react";
import { Link } from "react-router-dom";
import IdentityService from "./IdentityService";

export default function Register()
{
    const [errors, setErrors] = useState([]);

    function handleSubmitRegister(e)
    {
        e.preventDefault();
        const formData = new FormData(e.target);
        const email = formData.get("email");
        const password = formData.get("password");

        const service = new IdentityService();

        service.postRegister(email, password).then((res) => {
            if (res.status === 400)
            {
                const tmpErrors = [];
                const keys = Object.keys(res.errors);
                for (let i = 0; i < keys.length; i += 1)
                {
                    const errorMessage = res.errors[keys[i]][0];
                    tmpErrors.push(errorMessage);
                }
                setErrors(tmpErrors);
            }
            else if (res.status === 200)
            {
                setErrors([]);
            }
            else
            {
                throw new Error(`Response status: ${res.status}`);
            }
        }).catch((error) => {
            console.error(error);
        });
    }

    const errorsInfo = <ol>
        {errors.map(er => <li>{er}</li>)}
    </ol>

    return <>
        <h1>Register:</h1>

        <div>
            {errorsInfo}
        
            <form onSubmit={handleSubmitRegister}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address:</label>
                    <input type="email" className="form-control" name="email" aria-describedby="emailHelp" />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password:</label>
                    <input type="password" className="form-control" name="password" />
                </div>

                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </div>

        <p className="text-center pt-4">
            Would you like to <Link to={"/login"}>login</Link> instead?
        </p>
    </>;
}
