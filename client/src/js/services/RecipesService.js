import ApiServiceBase from "./ApiServiceBase";

export default class RecipesService extends ApiServiceBase {
    constructor() {
        super();
        this.recipesBaseUrl = `${this.backendOrigin}/api/Recipes`;
    }

    async getRecipes(sortOrder="", search="") {
        let url = this.recipesBaseUrl;

        if (sortOrder !== "" && search !== "") url += `?sortOrder=${sortOrder}&search=${search}`;
        else if (sortOrder !== "") url += `?sortOrder=${sortOrder}`;
        else if (search !== "") url += `?search=${search}`;

        return await this.tryGetJson(url);
    }

    async getRecipe(id) {
        const url = `${this.recipesBaseUrl}/${id}`;

        return await this.tryGetJson(url);
    }

    async postRecipe(recipeDto) {
        return await this.tryPost(this.recipesBaseUrl, recipeDto);
    }

    async putRecipe(recipeDto) {
        if (recipeDto.id === undefined) throw new Error("id missing");

        const url = `${this.recipesBaseUrl}/${recipeDto.id}`;

        return await this.tryPut(url, recipeDto);  
    }

    async deleteRecipe(id) {
        const url = `${this.recipesBaseUrl}/${id}`;

        this.tryDelete(url);
    }
}
