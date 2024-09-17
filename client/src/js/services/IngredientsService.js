import ApiServiceBase from "./ApiServiceBase";

export default class IngredientsService extends ApiServiceBase {
    constructor() {
        super();
        this.ingredientsBaseUrl = `${this.backendOrigin}/api/Ingredients`;
    }

    async getIngredients(sortOrder = "", search = "") {
        let url = this.ingredientsBaseUrl;

        if (sortOrder !== "" && search !== "") url += `?sortOrder=${sortOrder}&search=${search}`;
        else if (sortOrder !== "") url += `?sortOrder=${sortOrder}`;
        else if (search !== "") url += `?search=${search}`;

        return await this.tryGetJson(url);
    }

    async getIngredient(id) {
        const url = `${this.ingredientsBaseUrl}/${id}`;

        return await this.tryGetJson(url);
    }

    async postIngredient(ingDto) {
        return await this.tryPostJson(this.ingredientsBaseUrl, ingDto);
    }

    async putIngredient(ingDto) {
        if (ingDto.id === undefined) throw new Error("id missing");

        let url = `${this.ingredientsBaseUrl}/${ingDto.id}`;

        return await this.tryPut(url, ingDto);  
    }

    // TODO: Refactor this to use the super class patch method
    async patchQuantity(ingredient) {
        let url = `${this.ingredientsBaseUrl}/${ingredient.id}`;

        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request(url, {
            method: "PATCH",
            body: JSON.stringify(ingredient) ,
            headers: headers
        });

        const response = await fetch(request);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return response.json();
    }

    async deleteIngredient(id) {
        const url = `${this.ingredientsBaseUrl}/${id}`;

        this.tryDelete(url);
    }
}
