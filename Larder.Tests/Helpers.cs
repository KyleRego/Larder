namespace Larder.Tests;

public static class Helpers
{
    public static T Untask<T>(Task<T> task)
    {
        return task.GetAwaiter().GetResult();
    }
}
