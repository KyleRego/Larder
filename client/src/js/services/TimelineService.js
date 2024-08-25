import ApiServiceBase from "./ApiServiceBase";

export default class TimelineService extends ApiServiceBase
{
    constructor()
    {
        super();
        this.timelineBaseUrl = `${this.backendOrigin}/api/Timeline`;
    }

    async getTimelineIndex()
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
