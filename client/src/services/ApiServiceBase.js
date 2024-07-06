export default class ApiServiceBase
{
    constructor()
    {
        this.backendOrigin = process.env.REACT_APP_WEBAPI_ORIGIN;
    }
}