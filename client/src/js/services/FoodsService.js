import ApiServiceBase from "./ApiServiceBase";

export default class FoodsService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.foodsBaseUrl = `${this.backendOrigin}/api/foods`;
        this.foodEatingBaseUrl = `${this.backendOrigin}/api/foodEating`;
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

        return await this.tryPost(url, food);
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
        const id = food.id;
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

    async postEatFood(dto)
    {
        if (!dto.id) return;

        let url = this.foodEatingBaseUrl;

        return await this.tryPost(url, dto);
    }
}