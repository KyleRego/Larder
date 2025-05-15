// src/pages/EditRecipe.tsx
import { useState, useEffect } from "react";
import { DynamicForm } from "../DynamicForm";
import { RecipeDto } from "../../types/dtos/RecipeDto";
import { useApiRequest } from "../../hooks/useApiRequest";
import Loading from "../Loading";
import { useParams } from "react-router";

export function EditRecipe() {
    const { id } = useParams<{ id: string }>();
    const [recipe, setRecipe] = useState<RecipeDto | null>(null);
    const { handleRequest } = useApiRequest();

    useEffect(() => {
        async function getRecipe() {
            const res: RecipeDto | null = await handleRequest<RecipeDto>({
                method: "get",
                url: `/api/Recipes/${id}`
            });

            if (res) {
                setRecipe(res);
            }
        }

        getRecipe();
    }, [id]);

    if (!recipe) return <Loading />;

    const handleSubmit = (updated: RecipeDto) => {
        console.log(updated);
    };

    return (
        <DynamicForm<RecipeDto>
            modelName="Recipe"
            data={recipe}
            onSubmit={handleSubmit}
        />
    );
}
