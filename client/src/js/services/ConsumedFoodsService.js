import ApiServiceBase from "./ApiServiceBase";

export default class ConsumedFoodsService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.timelineBaseUrl = `${this.backendOrigin}/api/ConsumedFoods`;
    }

    async getConsumedFoodsIndex()
    {
        let url = this.timelineBaseUrl;

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
