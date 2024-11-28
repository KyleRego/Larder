namespace Larder.Services;

// Wraps IServiceProvider to be able to mock GetRequiredService<T>
// "Extension methods may not be used in setup
//                             / verification expressions." - Moq
public interface IServiceProviderWrapper
{
    public T GetRequiredService<T>() where T : notnull;
}

public class ServiceProviderWrapper(IServiceProvider serviceProvider)
                                                : IServiceProviderWrapper
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public T GetRequiredService<T>() where T : notnull
    {
        return _serviceProvider.GetRequiredService<T>();
    }
}
