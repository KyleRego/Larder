import { useState, useEffect, useContext } from "react";

import TimelineService from "../services/TimelineService";
import DayNutritionCard from "./DayNutritionCard";
import { AlertContext } from "../../AlertContext";

export default function Timeline() {
    const { setAlertMessage } = useContext(AlertContext);
    const [nutritionDays, setNutritionDays] = useState([]);

    useEffect(() => {
        const service = new TimelineService();

        service.getTimelineIndex().then(result => {
            setNutritionDays(result);
        }).catch(error => {
            setAlertMessage(`Something went wrong: ${error.message}`);
        });
    }, [setAlertMessage]);

    console.log(nutritionDays);

    const dayNutritionCards = nutritionDays.map(nd => {
        return <DayNutritionCard key={nd.date} nutritionDay={nd}
                            nutritionDays={nutritionDays} setNutritionDays={setNutritionDays} />;
    });

    return <>
        <h1>Nutrition timeline</h1>

        {dayNutritionCards}
    </>;
}
