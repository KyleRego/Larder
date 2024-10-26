import { useContext } from "react";
import { AuthedContext } from "../contexts/AuthedContext";
import { apiClient } from "../util/axios";
import { Link } from "react-router-dom";

export default function Login() {
    const { setAuthed } = useContext(AuthedContext);

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
            }})
            .catch(error => console.log(error));
    };

    return <>
        <h1 className="mt-2 mb-4 text-center">Login:</h1>

        <div>
            <form onSubmit={handleLogin}>
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address:</label>
                    <input required type="email" className="form-control" name="email" aria-describedby="emailHelp" />
                </div>

                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password:</label>
                    <input required type="password" className="form-control" name="password" />
                </div>

                <button type="submit" className="btn btn-primary">Submit</button>
            </form>
        </div>

        <p className="text-center pt-4">
            Would you like to <Link to={"/register"}>register</Link> instead?
        </p>

    </>;
}