using Larder.Models;

namespace Larder.Services.Interface;

public interface IQuantityMathService
{
    public Task<Quantity> Subtract(Quantity minuend, Quantity subtrahend);
}
