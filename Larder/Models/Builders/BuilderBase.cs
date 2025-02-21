namespace Larder.Models.Builders;

public abstract class BuilderBase<TSelf>
            where TSelf : BuilderBase<TSelf>
{
    protected string _id = Guid.NewGuid().ToString();

    public TSelf WithId(string id)
    {
        _id = id;
        return (TSelf)this;
    }
}
