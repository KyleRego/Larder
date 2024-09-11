export default class ApiServiceBase
{
    constructor()
    {
        this.backendOrigin = process.env.REACT_APP_WEBAPI_ORIGIN;

        // TODO: Figure out why npm build does not read .env.production
        if (this.backendOrigin === undefined)
        {
            this.backendOrigin = "https://larder.lol:49152";
        }
    }

    // TODO: Remove this API and use the one above
    async tryGetJson(url)
    {
        const response = await fetch(url, { credentials: "include" });

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return await response.json();
    }

    async tryPost(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return await response.json();
    }

    async tryPut(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "PUT",
            headers: headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return await response.json();
    }

    async tryPatch(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "PATCH",
            headers: headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }
    }

    async tryDelete(url)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "DELETE",
            headers: headers,
        });

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }
    }
}