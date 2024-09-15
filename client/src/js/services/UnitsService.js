import ApiServiceBase from "./ApiServiceBase";

export default class UnitsService extends ApiServiceBase
{
    constructor() {
        super();
        this.unitsBasePath = `${this.backendOrigin}/api/Units`;
    }

    async getUnits(sortOrder) {
        let url = this.unitsBasePath;

        if (sortOrder !== null)
        {
            url += `?sortOrder=${sortOrder}`
        }

        return await this.tryGetJson(url);
    }

    async getUnit(id) {
        let url = `${this.unitsBasePath}/${id}`;

        return await this.tryGetJson(url);
    }

    async postUnit(dto) {
        let url = this.unitsBasePath;

        return await this.tryPost(url, dto); 
    }

    async putUnit(dto) {
        if (dto.id === undefined) {
            throw new Error("id missing");
        }

        let url = `${this.unitsBasePath}/${dto.id}`;

        return await this.tryPut(url, dto);  
    }

    async deleteUnit(id) {
        let url = `${this.unitsBasePath}/${id}`;

        return await this.tryDelete(url);
    }
}