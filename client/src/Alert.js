import { useContext } from "react";
import { AlertContext } from "./AlertContext";

export default function Alert() {
    const { alertMessage, setAlertMessage } = useContext(AlertContext);

    function handleCloseAlert() {
        setAlertMessage("");
    }

    if (alertMessage === "") return;

    return <div className="container position-absolute bottom-0 start-50 translate-middle">
        <div className="card text-center shadow-sm">
            <div className="card-body">
                <div className="mb-4">
                    {alertMessage}
                </div>
                <div className="m-0">
                    <button type="button" className="btn btn-secondary" onClick={handleCloseAlert}>OK</button>
                </div>
            </div>
        </div> 
    </div>
}
