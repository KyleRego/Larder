import { apiClient } from "../util/axios"

export default function Logout() {
    function handleLogout() {
        apiClient.post("/logout")
    }
    
    return (
        <>
            <button type="button" onClick={handleLogout}>
                Logout
            </button>
        </>
    )
}
