import { useContext } from "react";
import { MessageContext } from "../contexts/MessageContext";
import { ApiResponseType } from "../types/ApiResponse";

export default function MessageDisplay() {
    const { message, setMessage } = useContext(MessageContext)

    if (!message) return null;

    const handleClose = () => setMessage(null);

    let bgColorCss: string = "";
    let borderCss: string = "";
    let btnCss: string = "";

    switch(message.type) {
        case ApiResponseType.Success:
            bgColorCss = "bg-success-subtle";
            borderCss = "border-success";
            btnCss = "btn-outline-success"
            break;
        case ApiResponseType.Warning:
            bgColorCss = "bg-warning-subtle";
            borderCss = "border-warning";
            btnCss = "btn-warning"
            break;
    }

    return (
        <div className="container position-fixed bottom-0 start-50 translate-middle" style={{zIndex: 9999, bottom: 0}}>
            <div className="d-flex justify-content-center">
                <div className="card text-center border border-0 w-75">
                    <div className={`card-body ${bgColorCss} border ${borderCss} rounded`}>
                        <div className="d-flex justify-content-center column-gap-3 align-items-center card-text">
                            <div id="message-text">
                                {message.text}
                            </div>
                            <div>
                                <button type="button" className={`btn ${btnCss}`} onClick={handleClose}>OK</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>  
        </div>
    );
};
