import { MdModeEdit } from "react-icons/md";
import { Link } from "react-router-dom";

export default function EditLink({path, title} : {path: string, title: string}) {
    return  <Link   to={path}
                    title={title}
                    type="button" className="text-black">
                        <MdModeEdit className="icon" />
            </Link>;
}
