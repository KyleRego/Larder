using Larder.Models;
using Larder.Models.SortOptions;

namespace Larder.Repository.Interface;

public interface IItemRepository : IRepositoryBase<Item, ItemSortOptions>
{

}
