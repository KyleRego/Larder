import { MdModeEdit } from "react-icons/md";
import { Link } from "react-router-dom";

export default function EditLink({path, title} : {path: string, title: string}) {
    return  <Link   to={path}
                    title={title}
                    type="button" className="text-black border-black border-bottom border-2">
                        <MdModeEdit className="icon" />
            </Link>;
}
