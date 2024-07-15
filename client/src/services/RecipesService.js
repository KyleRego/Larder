import ApiServiceBase from "./ApiServiceBase";

export default class RecipesService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.recipesBaseUrl = `${this.backendOrigin}/api/Recipes`;
    }

    async getRecipes()
    {
        try
        {
            const response = await fetch(this.recipesBaseUrl);

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

    async getRecipe(id)
    {
        let url = `${this.recipesBaseUrl}/${id}`;

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

    async putRecipe(recipeDto)
    {
        let url = `${this.recipesBaseUrl}/${recipeDto.recipeId}`;

        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request(url, {
            method: "PUT",
            body: JSON.stringify(recipeDto) ,
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
}