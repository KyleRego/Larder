export enum ApiResponseType {
    Success, Danger, Warning, Info
}

export type ApiResponse<T> = {
    data: T,
    message: string,
    type: ApiResponseType
}
