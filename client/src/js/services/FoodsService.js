import ApiServiceBase from "./ApiServiceBase";

export default class FoodsService extends ApiServiceBase {
    constructor() {
        super();
        this.foodsBaseUrl = `${this.backendOrigin}/api/foods`;
    }

    async getFoods(sortOrder = "", search = "") {
        let url = this.foodsBaseUrl;

        if (sortOrder !== "" && search !== "") url += `?sortOrder=${sortOrder}&search=${search}`;
        else if (sortOrder !== "") url += `?sortOrder=${sortOrder}`;
        else if (search !== "") url += `?search=${search}`;

        return await this.tryGetJson(url);
    }

    async getFood(id) {
        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryGetJsonV2(url);
    }

    async postFood(foodDto) {
        return await this.tryPostJson(this.foodsBaseUrl, foodDto);
    }

    async postEatFood(foodServingsDto) {
        if (foodServingsDto.foodId === undefined) this.throwNoId();

        const url = `${this.foodsBaseUrl}/EatFood/${foodServingsDto.foodId}`;

        return await this.tryPostJson(url, foodServingsDto);
    }

    async putFood(food) {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPut(url, food);
    }

    async patchFood(foodServingsDto) {
        if (!foodServingsDto.foodId) this.throwNoId();

        const url = `${this.foodsBaseUrl}/${foodServingsDto.foodId}`;

        return await this.tryPatch(url, foodServingsDto);
    }

    async deleteFood(food) {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryDelete(url, food);
    }
}
