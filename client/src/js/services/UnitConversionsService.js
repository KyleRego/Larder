import ApiServiceBase from "./ApiServiceBase";

export default class UnitConversionsService extends ApiServiceBase {
    constructor() {
        super();
        this.unitConversionsBaseUrl = `${this.backendOrigin}/api/UnitConversions`;
    }
    
    async postUnitConversion(dto) {
        const headers = new Headers({ "Content-Type": "application/json" });

        const request = new Request(this.unitConversionsBaseUrl, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        return await response.json();
    }

    async putUnitConversion(dto) {
        if (dto.id === undefined) {
            throw new Error("id missing");
        }

        let url = `${this.unitConversionsBaseUrl}/${dto.id}`;

        const headers = new Headers({ "Content-Type": "application/json" });

        const request = new Request(url, {
            method: "PUT",
            headers: headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        return await response.json();
    }

    async deleteUnitConversion(id) {
        let url = `${this.unitConversionsBaseUrl}/${id}`;

        const headers = new Headers({ "Content-Type": "application/json"});

        const request = new Request(url, {
            method: "Delete",
            headers: headers
        });

        await fetch(request);
    }
}
