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

        return await this.tryGetJson(url);
    }
}
