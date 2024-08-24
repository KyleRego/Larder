import Toast from 'react-bootstrap/Toast';
import ToastContainer from 'react-bootstrap/ToastContainer';

export default function AppToast({message, show, setShow})
{
    const toggleShow = () => setShow(!show);

    return (
        <ToastContainer
          className="p-3"
          position={"top-center"}
          style={{ zIndex: 1 }}
        >
            <Toast show={show} onClose={toggleShow}>
                <Toast.Header>
                    <strong className="me-auto">Larder</strong>
                </Toast.Header>
                <Toast.Body>{message}</Toast.Body>
            </Toast>
        </ToastContainer>
    );
}