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

        return await this.tryGetJson(url);
    }
}