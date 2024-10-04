using Larder.Data;
using Larder.Models;
using Microsoft.EntityFrameworkCore;

namespace Larder.Repository;

public enum LabResultSearchOptions
{
    AnyOrder,
    DateTime,
    DateTime_Desc
}

public interface ILabResultRepository
                    : IRepositoryBase<LabResultNtt, LabResultSearchOptions>
{

}

public class LabResultRepository(AppDbContext context)
            : RepositoryBase<LabResultNtt, LabResultSearchOptions>(context),
                                                        ILabResultRepository
{
    public override Task<LabResultNtt?> Get(string id)
    {
        throw new NotImplementedException();
    }

    public async override Task<List<LabResultNtt>> GetAllForUser(string userId, LabResultSearchOptions sortBy, string? search)
    {
        var query = _dbContext.LabResults.Where(lr => lr.UserId == userId);

        switch (sortBy)
        {
            case LabResultSearchOptions.DateTime:
                query = query.OrderBy(lr => lr.DateTime);
                break;

            case LabResultSearchOptions.DateTime_Desc:
                query = query.OrderByDescending(lr => lr.DateTime);
                break;
        }

        return await query.ToListAsync();
    }
}
