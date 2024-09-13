import { createContext } from "react";

export const AlertContext = createContext({ alertMessage: "", setAlertMessage: () => {} });