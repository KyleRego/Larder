// src/components/DynamicForm.tsx
import { useState, useEffect, ChangeEvent, FormEvent } from "react";
import { apiClient } from "../util/axios";

export interface FieldMeta {
    name: string;
    dataType: string;
    isRequired: boolean;
    enumValues?: string[];
}

export interface DynamicFormProps<T extends Record<string, any>> {
    modelName: string;
    data?: T;
    onSubmit: (formData: T) => void;
    onChange?: (formData: T) => void;
}

export function DynamicForm<T extends Record<string, any>>({
    modelName,
    data,
    onSubmit,
    onChange,
}: DynamicFormProps<T>) {
    const [meta, setMeta] = useState<FieldMeta[]>([]);
    const [formData, setFormData] = useState<T>({} as T);

    // Fetch metadata + initialize formData
    useEffect(() => {
        const resource = modelName.toLowerCase() + "s";
        async function loadMeta() {
            const res = await apiClient.get(`/api/${resource}/meta`);
            const fields: FieldMeta[] = res.data;
            setMeta(fields);

            const init = {} as any;
            fields.forEach(f => {
                init[f.name] = (data as any)?.[f.name] ?? "";
            });
            setFormData(init);
        }
        loadMeta();
    }, [modelName, data]);

    const handleChange = (
        e: ChangeEvent<HTMLInputElement | HTMLSelectElement>
    ) => {
        const { name, value, type, checked } = e.target as HTMLInputElement;
        const newValue = type === "checkbox" ? checked : value;
        setFormData(prev => {
            const updated = { ...prev, [name]: newValue };
            onChange?.(updated);
            return updated;
        });
    };

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault();
        onSubmit(formData);
    };

    if (!meta.length) return <div>Loading form…</div>;

    return (
        <form onSubmit={handleSubmit}>
            {meta.map(field => (
                <div key={field.name} style={{ marginBottom: 12 }}>
                    <label style={{ display: "block" }}>
                        {field.name}
                        {field.isRequired && " *"}
                    </label>

                    {field.dataType.startsWith("Enum:") && field.enumValues ? (
                        <select
                            name={field.name}
                            value={formData[field.name] ?? ""}
                            required={field.isRequired}
                            onChange={handleChange}
                        >
                            <option value="">— select —</option>
                            {field.enumValues.map(opt => (
                                <option key={opt} value={opt}>
                                    {opt}
                                </option>
                            ))}
                        </select>
                    ) : field.dataType === "Boolean" ? (
                        <input
                            type="checkbox"
                            name={field.name}
                            checked={!!formData[field.name]}
                            onChange={handleChange}
                        />
                    ) : (
                        <input
                            type={
                                field.dataType === "Int32" || field.dataType === "Double"
                                    ? "number"
                                    : "text"
                            }
                            name={field.name}
                            value={formData[field.name] ?? ""}
                            required={field.isRequired}
                            onChange={handleChange}
                        />
                    )}
                </div>
            ))}

            <button className="btn btn-primary" type="submit">Save</button>
        </form>
    );
}
