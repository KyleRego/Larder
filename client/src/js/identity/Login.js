import { useContext } from "react";
import { useNavigate, Link } from "react-router-dom";
import IdentityService from "./IdentityService";
import { AuthedContext } from "../../AuthedContext";

export default function Login()
{
    const { setAuthed } = useContext(AuthedContext);
    const navigate = useNavigate();

    const handleSubmitLogin = (e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        const email = formData.get("email");
        const password = formData.get("password")

        const service = new IdentityService();
        service.postLogin(email, password).then(response => {
            if (response.ok) {
                setAuthed(true);
                navigate("/");
            }
        }).catch(error => {
            console.error(error);
        });
    };

    return <>
        <h1 className="mt-2 mb-4 text-center">Login:</h1>
    
        <div>
            <form onSubmit={handleSubmitLogin}>
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
