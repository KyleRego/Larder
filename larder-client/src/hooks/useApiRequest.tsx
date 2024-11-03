import { useContext } from "react";
import { MessageContext } from "../contexts/MessageContext";
import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { apiClient } from "../util/axios";
import { ApiResponse, ApiResponseType } from "../types/ApiResponse";

export function useApiRequest() {
    const { setMessage } = useContext(MessageContext);

    async function handleRequest<T>(config: AxiosRequestConfig)
                                                        : Promise<T | null> {
        try {
            const response: AxiosResponse<ApiResponse<T>>
                                            = await apiClient.request(config);

            setMessage({text: response.data.message, type: response.data.type});

            return response.data.data;
        } catch (error) {
            if (axios.isAxiosError(error)) {
                if (error.response) {
                    switch (error.response.status) {
                        case 422:
                            setMessage({ text: error.response.data.message,
                                            type: ApiResponseType.Warning });
                            break;
                        case 401:
                            setMessage({ text: "Unauthorized access.",
                                            type: ApiResponseType.Danger });
                            break;
                        default:
                            setMessage({ text: "An unexpected error occurred.",
                                            type: ApiResponseType.Danger });
                    }
                } else {
                    setMessage({ text: "A network error occurred; please try again.",
                                            type: ApiResponseType.Danger });
                }
            } else {
                setMessage({ text: "An unexpected error occurred.",
                                            type: ApiResponseType.Danger });
            }

            return null;
        }
    }

    return { handleRequest };
}
