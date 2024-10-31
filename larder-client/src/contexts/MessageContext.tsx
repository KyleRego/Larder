import { createContext, SetStateAction, Dispatch } from 'react';
import { Message } from '../types/Message';

interface MessageContextType {
    message: Message | null;
    setMessage: Dispatch<SetStateAction<Message | null>>;
};

export const MessageContext = createContext<MessageContextType>({
    message: null,
    setMessage: () => {}
});
