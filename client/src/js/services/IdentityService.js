import ApiServiceBase from "./ApiServiceBase";

export default class IdentityService extends ApiServiceBase
{
    async PostRegister(email, password)
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

        const response = await fetch(request);

        return await response.json();
    }

    async PostLogin(email, password)
    {
        const url = `${this.backendOrigin}/login`;

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

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error("response was not ok");
        }
    }
}
