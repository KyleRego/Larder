using Larder.Dtos;
using Larder.Models.ItemComponents;

namespace Larder.Models.Builders;

public class NutritionBuilder
{
    private Quantity _servingSize = Quantity.One();
    private double _calories;
    private double _gramsProtein;
    private double _gramsTotalFat;
    private double _gramsSaturatedFat;
    private double _gramsTransFat;
    private double _milligramsCholesterol;
    private double _milligramsSodium;
    private double _gramsTotalCarbs;
    private double _gramsDietaryFiber;
    private double _gramsTotalSugars;

    public NutritionBuilder WithServingSize(Quantity servingSize)
    {
        _servingSize = servingSize;
        return this;
    }

    public NutritionBuilder WithServingSize(QuantityDto servingSize)
    {
        _servingSize = new()
        {
            Amount = servingSize.Amount,
            UnitId = servingSize.UnitId
        };
        return this;
    }

    public NutritionBuilder WithServingSize(double amount, Unit? unit = null)
    {
        _servingSize = new()
        {
            Amount = amount,
            UnitId = unit?.Id
        };
        return this;
    }

    public NutritionBuilder WithCalories(double calories)
    {
        _calories += calories;
        return this;
    }

    public NutritionBuilder WithProtein(double gramsProtein)
    {
        _gramsProtein += gramsProtein;
        return this;
    }

    public NutritionBuilder WithTotalFat(double grams)
    {
        _gramsTotalFat += grams;
        return this;
    }

    public NutritionBuilder WithSaturatedFat(double grams)
    {
        _gramsSaturatedFat += grams;
        return this;
    }

    public NutritionBuilder WithTransFat(double grams)
    {
        _gramsTransFat += grams;
        return this;
    }

    public NutritionBuilder WithCholesterol(double mg)
    {
        _milligramsCholesterol += mg;
        return this;
    }

    public NutritionBuilder WithSodium(double mg)
    {
        _milligramsSodium += mg;
        return this;
    }

    public NutritionBuilder WithTotalCarbs(double grams)
    {
        _gramsTotalCarbs += grams;
        return this;
    }

    public NutritionBuilder WithDietaryFiber(double grams)
    {
        _gramsDietaryFiber += grams;
        return this;
    }

    public NutritionBuilder WithTotalSugars(double grams)
    {
        _gramsTotalSugars += grams;
        return this;
    }

    public Nutrition Build(Item item)
    {
        return new Nutrition
        {
            Item = item,
            ItemId = item.Id,
            ServingSize = _servingSize,
            Calories = _calories,
            GramsProtein = _gramsProtein,
            GramsTotalFat = _gramsTotalFat,
            GramsSaturatedFat = _gramsSaturatedFat,
            GramsTransFat = _gramsTransFat,
            MilligramsCholesterol = _milligramsCholesterol,
            MilligramsSodium = _milligramsSodium,
            GramsTotalCarbs = _gramsTotalCarbs,
            GramsDietaryFiber = _gramsDietaryFiber,
            GramsTotalSugars = _gramsTotalSugars
        };
    }

    public NutritionDto BuildDto()
    {
        return new NutritionDto
        {
            ServingSize = (QuantityDto)_servingSize,
            Calories = _calories,
            GramsProtein = _gramsProtein,
            GramsTotalFat = _gramsTotalFat,
            GramsSaturatedFat = _gramsSaturatedFat,
            GramsTransFat = _gramsTransFat,
            MilligramsCholesterol = _milligramsCholesterol,
            MilligramsSodium = _milligramsSodium,
            GramsTotalCarbs = _gramsTotalCarbs,
            GramsDietaryFiber = _gramsDietaryFiber,
            GramsTotalSugars = _gramsTotalSugars
        };
    }
}
