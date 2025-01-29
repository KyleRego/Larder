interface UINutrient {
    field: string,
    label: string,
    unitName: string,
    emphasized: boolean
}

export const UINutrientData: UINutrient[] = [
        {field: "calories", label: "Calories", unitName: "", emphasized: true},
        {field: "gramsProtein", label: "Protein", unitName: "g", emphasized: true},
        {field: "gramsTotalFat", label: "Total fat", unitName: "g", emphasized: true},
        {field: "gramsSaturatedFat", label: "Saturated fat", unitName: "g", emphasized: false},
        {field: "gramsTransFat", label: "Trans fat", unitName: "g", emphasized: false},
        {field: "gramsTotalCarbs", label: "Total carbs", unitName: "g", emphasized: true},
        {field: "gramsTotalSugars", label: "Total sugars", unitName: "g", emphasized: false},
        {field: "gramsDietaryFiber", label: "Dietary fiber", unitName: "g", emphasized: false},
        {field: "milligramsSodium", label: "Sodium", unitName: "mg", emphasized: true},
        {field: "milligramsCholesterol", label: "Cholesterol", unitName: "mg", emphasized: true},
    ]