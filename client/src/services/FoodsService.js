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

    async postFood(food)
    {
        const url = `${this.foodsBaseUrl}`;

        return await this.tryPostJson(url, food);
    }

    async putFood(food)
    {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPutJson(url, food);
    }

    async patchFood(food)
    {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPatchJson(url, food);
    }
}