import { RecipeDto } from "../../types/dtos/RecipeDto";
import { DynamicForm } from "../DynamicForm";

export default function NewRecipe() {
    function handleSubmit() {};

    return (
        <div style={{ maxWidth: 600, margin: "0 auto" }}>
        <h1>New Recipe</h1>
        <DynamicForm<RecipeDto>
            modelName="Recipe"
            onSubmit={handleSubmit}
        />
        </div>
    );   
}