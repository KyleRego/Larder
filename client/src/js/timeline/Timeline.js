import { useState, useEffect } from "react";

import TimelineService from "../services/TimelineService";
import DayNutritionCard from "./DayNutritionCard";

export default function Timeline()
{
    const [nutritionDays, setNutritionDays] = useState([]);

    useEffect(() => {
        const service = new TimelineService();

        service.getTimelineIndex().then(result => {
            setNutritionDays(result);
        }).catch(error => {
            console.error(error);
        });
    }, []);

    const nutritionDayCards = nutritionDays.map(nd => {
        return <DayNutritionCard key={nd.date} nutritionDay={nd}
                            nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />;
    });

    return <>
        <h1>Nutrition timeline</h1>

        {nutritionDayCards}
    </>;
}
