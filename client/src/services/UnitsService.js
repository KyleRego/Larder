import ApiServiceBase from "./ApiServiceBase";

export default class UnitsService extends ApiServiceBase
{
    async getUnits()
    {
        const url = `${this.backendOrigin}/units`;

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