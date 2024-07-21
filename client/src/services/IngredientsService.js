import ApiServiceBase from "./ApiServiceBase";

export default class IngredientsService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.ingredientsBaseUrl = `${this.backendOrigin}/api/Ingredients`;
    }

    async getIngredients(sortOrder)
    {
        let url = this.ingredientsBaseUrl;

        if (sortOrder !== null)
        {
            url += `?sortOrder=${sortOrder}`
        }

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

    async getIngredient(id)
    {
        let url = `${this.ingredientsBaseUrl}/${id}`;

        try
        {
            const response = await fetch(url);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return await response.json();
        }
        catch (error)
        {
            console.error(error.message);
        }
    }

    async postIngredient(ingredient)
    {
        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request(this.ingredientsBaseUrl, {
            method: "POST",
            body: JSON.stringify(ingredient) ,
            headers: headers
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
        catch (error)
        {
            console.error(error);
        }
    }

    async putIngredient(ingredient)
    {
        const headers = new Headers({"Content-Type": "application/json"});
    
        const request = new Request(`${this.ingredientsBaseUrl}/${ingredient.id}`, {
            method: "PUT",
            headers: headers,
            body: JSON.stringify(ingredient)
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
        catch (error)
        {
            console.error(error);
        }
    }

    async patchQuantity(ingredient)
    {
        let url = `${this.ingredientsBaseUrl}/${ingredient.id}`;

        const headers = new Headers();
        headers.append("Content-Type", "application/json");

        const request = new Request(url, {
            method: "PATCH",
            body: JSON.stringify(ingredient) ,
            headers: headers
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
        catch (error)
        {
            console.error(error);
        }
    }

    async deleteIngredient(id)
    {
        const url = `${this.ingredientsBaseUrl}/${id}`;

        const headers = new Headers({"Content-Type": "application/json"});

        const request = new Request(url, {
            method: "DELETE",
            headers: headers
        });

        try
        {
            const response = await fetch(request);

            if (!response.ok)
            {
                throw new Error(`Response status: ${response.status}`);
            }

            return;
        }
        catch (error)
        {
            console.error(error);
        }
    }
}