import ApiServiceBase from "./ApiServiceBase";

export default class DemoService extends ApiServiceBase {
    constructor() {
        super();
        this.demosBaseUrl = `${this.backendOrigin}/api/Demos`;
    }
    
    async postCreateDemo() {
        return await this.tryPost(this.demosBaseUrl);
    }
}
