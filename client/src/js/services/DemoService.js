import ApiServiceBase from "./ApiServiceBase";

export default class DemoService extends ApiServiceBase {
    constructor() {
        this.demosBaseUrl = `${this.backendOrigin}/api/Demos`;
    }
    
    async postCreateDemo() {
        return await this.tryPost(this.demosBaseUrl, {});
    }
}
