export default class ApiServiceBase
{
    constructor()
    {
        this.backendOrigin = process.env.REACT_APP_WEBAPI_ORIGIN;
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
}