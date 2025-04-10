using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Tests.Services.MockRepository;

namespace Larder.Tests.Services.ItemServiceTests;

public abstract class ItemServiceTestsBase : ServiceTestsBase
{
    protected IItemService _sut;
    private readonly IUnitRepository _unitData = new MockUnitData();
    private readonly IItemRepository _itemData;

    public ItemServiceTestsBase()
    {
        _itemData = new MockItemData(_unitData);

        _sut = new ItemService(_serviceProvider.Object, _itemData);
    }
}
