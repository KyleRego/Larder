import ApiServiceBase from "./ApiServiceBase";

export default class UnitsService extends ApiServiceBase
{
    async getUnits(sortOrder)
    {
        let url = `${this.backendOrigin}/api/Units`;

        if (sortOrder !== null)
        {
            url += `?sortOrder=${sortOrder}`
        }

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