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
        case ApiResponseType.Info:
            bgColorCss = "bg-info";
            borderCss = "border-info";
            btnCss = "btn-outline-info"
            break;
        case ApiResponseType.Warning:
            bgColorCss = "bg-warning-subtle";
            borderCss = "border-warning";
            btnCss = "btn-warning"
            break;
        case ApiResponseType.Danger:
            bgColorCss = "bg-danger";
            borderCss = "border-danger";
            btnCss = "btn-outline-danger"
            break;
    }

    return (
        <div className="container position-absolute bottom-0 start-50 translate-middle">
            <div className={`card text-center border border-0`}>
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
    );
};
