import { useContext } from "react";
import { MessageContext } from "../contexts/MessageContext";

export default function MessageDisplay() {
    const { message, setMessage } = useContext(MessageContext)

    if (!message) return null;

    const handleClose = () => setMessage(null);

    return (
        <div className="container position-absolute bottom-0 start-50 translate-middle">
            <div className="card bg-primary-subtle text-center shadow-sm">
                <div className="card-body">
                    <div className="d-flex justify-content-center column-gap-3 align-items-center">
                        <div className="">
                            {message.text}
                        </div>
                        <div className="">
                            <button type="button" className="btn btn-outline-primary" onClick={handleClose}>OK</button>
                        </div>
                    </div>
                </div>
            </div> 
        </div>
    );
};
