import ApiServiceBase from "./ApiServiceBase";

export default class ConsumedFoodService extends ApiServiceBase {
    constructor() {
        super();
        this.consumedFoodsBaseUrl = `${this.backendOrigin}/api/ConsumedFoods`;
    }

    async postConsumedFood(dto) {
        const url = `${this.consumedFoodsBaseUrl}`;

        return await this.tryPostJson(url, dto);
    }

    async putConsumedFood(dto) {
        this.throwNoId();

        const url = `${this.consumedFoodsBaseUrl}/${dto.id}`;

        return await this.tryPut(url, dto);
    }

    async deleteConsumedFood(id)
    {
        this.throwNoId();

        const url = `${this.consumedFoodsBaseUrl}/${id}`;

        return await this.tryDelete(url);
    }
}
