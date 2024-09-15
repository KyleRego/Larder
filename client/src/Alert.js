import { useContext } from "react";
import { AlertContext } from "./AlertContext";

export default function Alert() {
    const { alertMessage, setAlertMessage } = useContext(AlertContext);

    function handleCloseAlert() {
        setAlertMessage("");
    }

    if (alertMessage === "") return;

    return <div className="container position-absolute bottom-0 start-50 translate-middle">
        <div className="card bg-primary-subtle text-center shadow-sm">
            <div className="card-body">
                <div className="d-flex justify-content-center column-gap-3 align-items-center">
                    <div className="">
                        {alertMessage}
                    </div>
                    <div className="">
                        <button type="button" className="btn btn-primary" onClick={handleCloseAlert}>OK</button>
                    </div>
                </div>
            </div>
        </div> 
    </div>
}
