export default class ApiServiceBase
{
    constructor() {
        this.backendOrigin = process.env.REACT_APP_WEBAPI_ORIGIN;

        // TODO: Figure out why npm build does not read .env.production
        if (this.backendOrigin === undefined)
        {
            this.backendOrigin = "https://larder.lol:49152";
            this.headers = new Headers({"Content-Type": "application/json"});
        }
    }

    // TODO: Double check this is the syntax for a private function
    async #throwError(response) {
        throw new Error(`Response status: ${response.status}`);
    }

    // TODO: This should be protected
    throwNoId() {
        throw new Error("no id");
    }

    async tryGetJson(url) {
        const response = await fetch(url, { credentials: "include" });

        if (!response.ok) this.#throwError(response);

        return await response.json();
    }

    async tryPost(url, dto) {
        const request = new Request(url, {
            method: "POST",
            headers: this.headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok) this.#throwError(response);

        return await response.json();
    }

    async tryPut(url, dto) {
        const request = new Request(url, {
            method: "PUT",
            headers: this.headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok) this.#throwError(response);

        return await response.json();
    }

    async tryPatch(url, dto)
    {
        const request = new Request(url, {
            method: "PATCH",
            headers: this.headers,
            body: JSON.stringify(dto)
        });

        const response = await fetch(request);

        if (!response.ok) this.#throwError(response);
    }

    async tryDelete(url) {
        const request = new Request(url, {
            method: "DELETE",
            headers: this.headers,
        });

        const response = await fetch(request);

        if (!response.ok) this.#throwError(response);
    }
}
