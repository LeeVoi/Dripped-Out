using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;

namespace service.Services;

public class SizeService
{

    private readonly ICrud<SizeType> _sizeTypeRepository;
    private readonly ISizeMapper _sizeMapper;

    public SizeService(CRUDFactory crudFactory, ISizeMapper sizeMapper)
    {
        _sizeTypeRepository = crudFactory.GetRepository<SizeType>(RepoType.SizeTypeRepo);
        _sizeMapper = sizeMapper;
    }
    
    public SizeType GetSizeById(int sizeId)
    {
        return _sizeTypeRepository.Read(sizeId);
    }

    public void CreatSize(SizeType sizeType)
    {
        _sizeTypeRepository.Create(sizeType);
    }

    public void DeleteSize(int sizeId)
    {
        _sizeTypeRepository.Delete(sizeId);
    }
    
    // This method is from the SizeMapperRepository and it adds selected sizes to the selected product
    public void AddSizeToProduct(int productId, List<int> sizeIds)
    {
        _sizeMapper.AddSizesToProduct(productId, sizeIds);
    }

    // This method is from the SizeMapperRepository and it removes selected sizes from the selected product
    public void RemoveSizeFromProduct(int productId, List<int> sizeIds)
    {
        _sizeMapper.RemoveSizesFromProduct(productId, sizeIds);
    }

    public List<SizeType> GetSizesByProductId(int productId)
    {
        return _sizeMapper.GetAllSizesByProductId(productId);
    }

    public List<SizeType> GetAllSizes()
    {
        return _sizeMapper.GetAllSizeTypes();
    }
    
}