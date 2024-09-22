import ApiServiceBase from "../services/ApiServiceBase";

export default class IdentityService extends ApiServiceBase
{
    async postRegister(email, password)
    {
        const url = `${this.backendOrigin}/register`;

        const dto = {
            email: email,
            password: password
        };

        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(dto)
        });

        return await fetch(request);
    }

    async postLogin(email, password)
    {
        const url = `${this.backendOrigin}/login?useCookies=true`;

        const dto = {
            email: email,
            password: password
        };

        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "POST",
            // without credentials: "include", the browser
            // ignores the Set-Cookie response header
            credentials: "include",
            headers: headers,
            body: JSON.stringify(dto)
        });

        return await fetch(request);
    }

    async postLogout() {
        const url = `${this.backendOrigin}/logout`;
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "POST",
            credentials: "include",
            headers: headers,
        });

        return await fetch(request);
    }
}
