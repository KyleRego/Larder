import ApiServiceBase from "./ApiServiceBase";

export default class IngredientsService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.ingredientsBaseUrl = `${this.backendOrigin}/api/Ingredients`;
    }

    async getIngredients()
    {
        try
        {
            const response = await fetch(this.ingredientsBaseUrl);

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