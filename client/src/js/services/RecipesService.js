import ApiServiceBase from "./ApiServiceBase";

export default class RecipesService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.recipesBaseUrl = `${this.backendOrigin}/api/Recipes`;
    }

    async getRecipes(sortOrder) {
        let url = this.recipesBaseUrl;

        if (sortOrder !== null)
        {
            url += `?sortOrder=${sortOrder}`;
        }

        const response = await fetch(url);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return await response.json();
    }

    async getRecipe(id)
    {
        let url = `${this.recipesBaseUrl}/${id}`;

        const response = await fetch(url);

        if (!response.ok)
        {
            throw new Error(`Response status: ${response.status}`);
        }

        return await response.json();
    }

    async postRecipe(recipe)
    {
        console.log("recipe being posted:");
        console.log(recipe);
        const headers = new Headers({"Content-Type": "application/json"});
        const request = new Request(this.recipesBaseUrl, {
            method: "POST",
            headers: headers,
            body: JSON.stringify(recipe)
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return response.json();
        }
        catch(error)
        {
            console.error(error);
        }
    }

    async putRecipe(recipe)
    {
        let url = `${this.recipesBaseUrl}/${recipe.id}`;

        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request(url, {
            method: "PUT",
            body: JSON.stringify(recipe) ,
            headers: headers
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }
        }
        catch (error)
        {
            console.error(error.message);
        }
    }

    async deleteRecipe(id)
    {
        let url = `${this.recipesBaseUrl}/${id}`;

        this.tryDelete(url);
    }
}
