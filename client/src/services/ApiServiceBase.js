export default class ApiServiceBase
{
    constructor()
    {
        this.backendOrigin = process.env.REACT_APP_WEBAPI_ORIGIN;

        // TODO: Figure out why npm build does not read .env.production
        if (this.backendOrigin === undefined)
        {
            this.backendOrigin = "https://kylerego.net:49152";
        }
    }

    // TODO: Can this be a protected method
    async tryGetJson(url)
    {
        try
        {
            const response = await fetch(url);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            const json = await response.json();

            return json;
        }
        catch (error)
        {
            console.error(error.message);
        }
    }

    async tryPostJson(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(dto)
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return response.json();
        }
        catch (error)
        {
            console.error(error);
        }
    }

    async tryPutJson(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "PUT",
            headers: headers,
            body: JSON.stringify(dto)
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return response.json();
        }
        catch (error)
        {
            console.error(error);
        }
    }

    async tryPatchJson(url, dto)
    {
        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "PATCH",
            headers: headers,
            body: JSON.stringify(dto)
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return response.json();
        }
        catch (error)
        {
            console.error(error);
        }
    }
}