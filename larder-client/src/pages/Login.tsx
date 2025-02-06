import { useContext } from "react";
import { AuthedContext } from "../contexts/AuthedContext";
import { apiClient } from "../util/axios";
import { Link, useNavigate } from "react-router-dom";
import { ApiResponseType } from "../types/ApiResponse";
import { MessageContext } from "../contexts/MessageContext";

export default function Login() {
    const { setAuthed } = useContext(AuthedContext);
    const navigate = useNavigate();
    const { setMessage } = useContext(MessageContext);

    function handleLogin(e: React.FormEvent<HTMLFormElement>) {
        e.preventDefault();
        const formData = new FormData(e.currentTarget);
        const email: string = formData.get("email") as string;
        const password: string = formData.get("password") as string;

        apiClient.post("/login",
                        { email: email, password: password},
                        { params: {useCookies: true} }).then(response => {
            if (response.status === 200) {
                setAuthed(true);
                navigate("/items");
                setMessage({text: "You are now logged in.", type: ApiResponseType.Success})
            }})
            .catch(error => console.log(error));
    };

    return <div className="container">
        <h1 className="my-4 text-center">Login:</h1>

        <div>
            <form onSubmit={handleLogin}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address:</label>
                    <input id="email" required type="email" className="form-control" name="email" aria-describedby="emailHelp" />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password:</label>
                    <input id="password" required type="password" className="form-control" name="password" />
                </div>

                <button id="submit-login" type="submit" className="btn btn-primary">Submit</button>
            </form>
        </div>

        <p className="text-center pt-4">
            Would you like to <Link to={"/register"}>register</Link> instead?
        </p>

    </div>;
}