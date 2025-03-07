import { ReactNode } from "react";

export default function BreadCrumbs({children} : {children: ReactNode}) {
    return  (
        <div className="container mt-3">
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    {children}
                </ol>
            </nav>
        </div>
    );
}
