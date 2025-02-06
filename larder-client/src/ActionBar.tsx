import { ReactNode } from "react";

export default function ActionBar({ children } : {children: ReactNode}) {
    return (
        <div className="sticky-bottom p-3 bg-secondary">
            {children}
        </div>
    );
};
