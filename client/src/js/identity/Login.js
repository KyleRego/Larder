import { useNavigate } from "react-router-dom";

import IdentityService from "../services/IdentityService";

export default function Login({})
{
    const navigate = useNavigate();

    const handleSubmitLogin = (e) => {
        e.preventDefault();
        const formData = new FormData(e.target);
        const email = formData.get("email");
        const password = formData.get("password")

        const service = new IdentityService();
        service.PostLogin(email, password).then(res => {

        }).catch(error => {
            console.error(error);
        });
    };

    return <>
        <h1>Login:</h1>
    
        <form onSubmit={handleSubmitLogin}>
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
    </>;
}