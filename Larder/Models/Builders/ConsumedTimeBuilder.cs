using Larder.Models.ItemComponents;

namespace Larder.Models.Builders;

public class ConsumedTimeBuilder
{
    private DateTimeOffset _consumedAt;

    public ConsumedTimeBuilder WithTime(DateTimeOffset consumedAt)
    {
        _consumedAt = consumedAt;
        return this;
    }

    public ConsumedTime Build(Item item)
    {
        ConsumedTime consumedTime = new()
        {
            Item = item,
            ItemId = item.Id,
            ConsumedAt = _consumedAt
        };

        return consumedTime;
    }
}