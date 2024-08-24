import ApiServiceBase from "./ApiServiceBase";

export default class FoodsService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.foodsBaseUrl = `${this.backendOrigin}/api/foods`;
    }

    async getFoods(sortOrder = null)
    {
        let url = this.foodsBaseUrl;

        if (sortOrder !== null)
        {
            url += `?sortOrder=${sortOrder}`;
        }

        return await this.tryGetJson(url);
    }

    async getFood(id)
    {
        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryGetJson(url);
    }

    async postFood(foodDto)
    {
        const url = `${this.foodsBaseUrl}`;

        return await this.tryPost(url, foodDto);
    }

    async postEatFood(foodServingsDto)
    {
        const id = foodServingsDto.foodId;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/EatFood/${id}`;

        return await this.tryPost(url, foodServingsDto);
    }

    async putFood(food)
    {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPut(url, food);
    }

    async patchFood(food)
    {
        const id = food.foodId;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPatch(url, food);
    }

    async deleteFood(food)
    {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryDelete(url, food);
    }
}
