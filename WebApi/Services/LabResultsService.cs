using Larder.Dtos;
using Larder.Models;
using Larder.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Larder.Services;

public interface ILabResultsService
{
    public Task<List<LabResult>> GetLabResults();
}

public class LabResultsService( ILabResultRepository repository,
                                IHttpContextAccessor httpConAcsr,
                                IAuthorizationService authService)
        : ApplicationServiceBase(httpConAcsr, authService), ILabResultsService
{
    private readonly ILabResultRepository _repository = repository;

    public async Task<List<LabResult>> GetLabResults()
    {
        List<LabResultNtt> labs = await _repository.GetAllForUser(CurrentUserId(),
                                                LabResultSearchOptions.DateTime);

        return labs.Select(LabResult.FromEntity).ToList();
    }
}
