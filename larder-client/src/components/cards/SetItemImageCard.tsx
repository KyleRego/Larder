import { useState } from "react";
import { useApiRequest } from "../../hooks/useApiRequest";
import { ItemDto } from "../../types/dtos/ItemDto";

export default function SetItemImageCard({itemId} : {itemId : string}) {
    const { handleRequest } = useApiRequest();
    const [file, setFile] = useState<File | null>(null);

    function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
        if (e.target.files?.length) {
            setFile(e.target.files[0]);
        }
    }

    async function handleUpload() {
        if (!file) return;

        const formData = new FormData();
        formData.append("imageFile", file);

        await handleRequest<ItemDto>({
            method: "post",
            url: `/api/Items/${itemId}/image`,
            data: formData
        });
    }

    return <div className="card" style={{maxWidth: "324px"}}>
        <div className="card-body">
            <h3 className="fs-4 mb-3">
                Set item image
            </h3>

            <div className="d-flex flex-column row-gap-3">
                <input type="file" accept="image/*" onChange={handleChange} />

                <button className="btn btn-sm btn-dark" onClick={handleUpload}>
                    Upload
                </button>
            </div>
        </div>

    </div>
}
