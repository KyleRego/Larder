import { MdModeEdit } from "react-icons/md";
import { Link } from "react-router-dom";

export default function EditLink({path, title} : {path: string, title: string}) {
    return  <Link   to={path}
                    title={title}
                    type="button" className="text-black"
                    style={{minWidth: "4.5rem"}}>
                        <div className="d-flex column-gap-1 btn btn-sm btn-light">
                            <MdModeEdit className="icon-sm" />
                            <span>Edit</span>
                        </div>
                        
            </Link>;
}
