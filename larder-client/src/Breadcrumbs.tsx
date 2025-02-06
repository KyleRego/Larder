import { ReactNode } from "react";

export default function BreadCrumbs({children} : {children: ReactNode}) {
    return  (
        <div className="container my-2">
            <nav aria-label="breadcrumb">
                <ol className="breadcrumb">
                    {children}
                </ol>
            </nav>
        </div>
    );
}
