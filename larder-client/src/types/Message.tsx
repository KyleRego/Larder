import { ApiResponseType } from "./ApiResponse";

export type Message = {
    text: string;
    type: ApiResponseType;
};