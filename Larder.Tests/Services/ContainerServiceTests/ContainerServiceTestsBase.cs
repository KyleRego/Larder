using Larder.Repository.Interface;
using Larder.Services.Interface;
using Larder.Services.Impl;
using Larder.Tests.Services.MockRepository;

namespace Larder.Tests.Services.ContainerServiceTests;

public abstract class ContainerServiceTestsBase : ServiceTestsBase
{
    protected IContainerService _sut;
    protected readonly IItemRepository _itemData;
    private readonly IUnitRepository _unitData;

    public ContainerServiceTestsBase()
    {
        _unitData = new MockUnitData();
        _itemData = new MockItemData(_unitData);

        _sut = new ContainerService(_serviceProvider.Object, _itemData);
    }
}