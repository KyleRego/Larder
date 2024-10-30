import { createContext, useState, ReactNode, SetStateAction, Dispatch } from 'react';
import { ApiResponseType } from '../types/ApiResponse';

export type Message = {
    text: string;
    type: ApiResponseType;
};

interface MessageContextType {
    message: Message | null;
    setMessage: Dispatch<SetStateAction<Message | null>>;
};

export const MessageContext = createContext<MessageContextType>({
    message: null,
    setMessage: () => {}
});

export const MessageProvider = ({ children }: { children: ReactNode}) => {
    const [message, setMessage] = useState<Message | null>(null);

    return (
        <MessageContext.Provider value={{ message, setMessage }}>
            {children}
        </MessageContext.Provider>
    );
};

