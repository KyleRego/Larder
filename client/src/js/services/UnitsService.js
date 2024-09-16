import ApiServiceBase from "./ApiServiceBase";

export default class UnitsService extends ApiServiceBase {
    constructor() {
        super();
        this.unitsBaseUrl = `${this.backendOrigin}/api/Units`;
    }

    async getUnits(sortOrder="", search="") {
        let url = this.unitsBaseUrl;

        if (sortOrder !== "" && search !== "") url += `?sortOrder=${sortOrder}&search=${search}`;
        else if (sortOrder !== "") url += `?sortOrder=${sortOrder}`;
        else if (search !== "") url += `?search=${search}`;

        return await this.tryGetJson(url);
    }

    async getUnit(id) {
        let url = `${this.unitsBaseUrl}/${id}`;

        return await this.tryGetJson(url);
    }

    async postUnit(unitDto) {
        return await this.tryPost(this.unitsBaseUrl, unitDto); 
    }

    async putUnit(unitDto) {
        if (unitDto.id === undefined) throw new Error("id missing");

        let url = `${this.unitsBaseUrl}/${unitDto.id}`;

        return await this.tryPut(url, unitDto);  
    }

    async deleteUnit(id) {
        let url = `${this.unitsBaseUrl}/${id}`;

        return await this.tryDelete(url);
    }
}
