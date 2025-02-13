using Larder.Dtos;

namespace Larder.Services.Interface;

public interface IQuantityService
{
    public Task<QuantityDto> Subtract
                                    (QuantityDto minuend, QuantityDto subtrahend);
    public Task<QuantityDto> SubtractUpToZero
                                    (QuantityDto minuend, QuantityDto subtrahend);
    public Task<QuantityDto> Convert(QuantityDto quantity, string unitId);

    public Task<double> Divide(QuantityDto dividend, QuantityDto divisor);
}
