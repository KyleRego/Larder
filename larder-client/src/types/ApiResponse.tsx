export enum ApiResponseType {
    Success, Warning
}

export type ApiResponse<T> = {
    data: T | null,
    message: string,
    type: ApiResponseType
}
