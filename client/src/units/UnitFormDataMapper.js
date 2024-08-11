export default class UnitFormDataMapper
{
    static map(formData)
    {
        const unit = {
            name: formData.get("name"),
            type: parseInt(formData.get("type"))
        };

        return unit;
    }
}