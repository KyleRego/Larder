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
}