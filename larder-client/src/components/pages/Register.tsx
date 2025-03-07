import { useContext, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { AuthedContext } from "../../contexts/AuthedContext";
import { apiClient } from "../../util/axios";
import { MessageContext } from "../../contexts/MessageContext";
import { ApiResponseType } from "../../types/ApiResponse";

export default function Register() {
    const { setAuthed } = useContext(AuthedContext);
    const [errors, setErrors] = useState<string[]>([]);
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    function handleRegister(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const email: string = formData.get("email") as string;
        const password: string = formData.get("password") as string;

        apiClient.post("/register", { email: email, password: password})
            .then(_ => {
                setAuthed(true);
                navigate("/");
                setMessage({text: "Account registered successfully.", type: ApiResponseType.Success});
            }).catch(error => {
            if (error.response?.status === 400) {
                const json = error.response.data;
                const tmpErrors: string[] = [];

                const keys = Object.keys(json.errors);
                for (let i = 0; i < keys.length; i += 1) {
                    const errorMessage = json.errors[keys[i]][0];
                    tmpErrors.push(errorMessage);
                }
                setErrors(tmpErrors);
            } else {
                console.error("Unexpected error occurred");
        }});
    }

    const errorsInfo = <ol>
        {errors.map(er => <li key={er}>{er}</li>)}
    </ol>;

    return <div className="container">
        <h1 className="my-4 text-center">Register:</h1>

        <div>
            {errorsInfo}
        
            <form onSubmit={handleRegister}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address:</label>
                    <input required autoComplete="username" id="email" type="email" className="form-control" name="email" aria-describedby="emailHelp" />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password:</label>
                    <input required autoComplete="new-password" id="password" type="password" className="form-control" name="password" />
                </div>

                <div>
                    <button id="submit-register" type="submit" className="btn btn-primary">Submit</button>
                </div>     
            </form>
        </div>

        <p className="text-center pt-4">
            Would you like to <Link to={"/login"}>login</Link> instead?
        </p>
    </div>;
}
