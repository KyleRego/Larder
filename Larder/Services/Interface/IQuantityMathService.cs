using Larder.Dtos;
using Larder.Models.Interface;

namespace Larder.Services.Interface;

public interface IQuantityMathService
{
    public Task<QuantityDto> Subtract
                                    (IQuantity minuend, IQuantity subtrahend);
    public Task<QuantityDto> SubtractUpToZero
                                    (IQuantity minuend, IQuantity subtrahend); 
}
