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

    async getUnit(id)
    {
        let url = `${this.backendOrigin}/api/Units/${id}`;

        return await this.tryGetJson(url);
    }

    async postUnit(dto)
    {
        let url = `${this.backendOrigin}/api/Units`;

        return await this.tryPost(url, dto); 
    }

    async putUnit(dto)
    {
        let url = `${this.backendOrigin}/api/Units/${dto.id}`;

        return await this.tryPut(url, dto);  
    }
}