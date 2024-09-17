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

        return await this.tryGetJson(url);
    }

    async postFood(foodDto) {
        return await this.tryPost(this.foodsBaseUrl, foodDto);
    }

    async postEatFood(foodServingsDto) {
        if (foodServingsDto.foodId === undefined) this.throwNoId();

        const url = `${this.foodsBaseUrl}/EatFood/${foodServingsDto.id}`;

        return await this.tryPost(url, foodServingsDto);
    }

    async putFood(food) {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryPut(url, food);
    }

    async patchFood(food) {
        if (food.id === null) this.throwNoId();

        const url = `${this.foodsBaseUrl}/${food.id}`;

        return await this.tryPatch(url, food);
    }

    async deleteFood(food) {
        const id = food.id;
        if (id === undefined) throw new Error("food id missing");

        const url = `${this.foodsBaseUrl}/${id}`;

        return await this.tryDelete(url, food);
    }
}
