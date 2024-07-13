import ApiServiceBase from "./ApiServiceBase";

export default class RecipesService extends ApiServiceBase
{
    async getRecipes()
    {
        let url = `${this.backendOrigin}/api/Recipes`;

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

    async getRecipe(id)
    {
        let url = `${this.backendOrigin}/api/Recipes/${id}`;

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